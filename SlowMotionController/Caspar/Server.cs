using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SlowMotionController.Amcp;
using System.Xml;
using Rug.Osc;

namespace SlowMotionController.Caspar
{
    public class Server
    {
        private AmcpClient client;
        private OscListener oscListener;
        private Thread updateThread;

        private List<Channel> channels;

        private ulong currentFrameInput = 0;

        private volatile bool shutDown = false;

        public List<Channel> Channels
        {
            get { return channels; }
        }

        public ulong RecordingHead
        {
            get { return currentFrameInput; }
        }

        private String CurrentTimeDate()
        {
            return DateTime.UtcNow.ToString("yyyyMMddHHmmss");
        }

        public Server(AmcpClient Client, Boolean ChannelGrid = false)
        {
            this.RecordingHeadPositionUpdated += new OscMessageEvent(Server_RecordingHeadPositionUpdated);
            this.PlayHeadPositionUpdated += new OscMessageEvent(Server_PlayHeadPositionUpdated);
            this.VirtualPlayHeadPositionUpdated += new OscMessageEvent(Server_VirtualPlayHeadPositionUpdated);

            client = Client;

            channels = new List<Channel>();

            channels.Add(new Channel(1));
            channels.Add(new Channel(2));
            channels.Add(new Channel(3));

            string Channel1FileName = CurrentTimeDate() + "-IN1";
            string Channel2FileName = CurrentTimeDate() + "-IN2";
            string Channel3FileName = CurrentTimeDate() + "-IN3";

            (new AmcpRequest(client, "PLAY", channels[0], "DECKLINK", "1")).GetResponse();
            (new AmcpRequest(client, "PLAY", channels[1], "DECKLINK", "2")).GetResponse();
            (new AmcpRequest(client, "PLAY", channels[2], "DECKLINK", "3")).GetResponse();

            (new AmcpRequest(client, "ADD", channels[0], "REPLAY", Channel1FileName)).GetResponse();
            channels[0].Consumer = new ReplayConsumer(Channel1FileName);
            
            (new AmcpRequest(client, "ADD", channels[1], "REPLAY", Channel2FileName)).GetResponse();
            channels[1].Consumer = new ReplayConsumer(Channel2FileName);
            
            (new AmcpRequest(client, "ADD", channels[2], "REPLAY", Channel3FileName)).GetResponse();
            channels[2].Consumer = new ReplayConsumer(Channel3FileName);

            channels.Add(new Channel(4));

            Thread.Sleep(250);
            (new AmcpRequest(client, "PLAY", channels[3], Channel1FileName)).GetResponse();

            channels[3].Producer = new ReplayProducer();

            try
            {
                oscListener = new OscListener(6250);
                oscListener.Attach("/channel/1/output/file/frame", RecordingHeadPositionUpdated);
                oscListener.Attach("/channel/2/output/file/frame", RecordingHeadPositionUpdated);
                oscListener.Attach("/channel/3/output/file/frame", RecordingHeadPositionUpdated);


                oscListener.Attach("/channel/4/stage/layer/0/file/frame", PlayHeadPositionUpdated);
                oscListener.Attach("/channel/4/stage/layer/0/file/vframe", VirtualPlayHeadPositionUpdated);
                oscListener.Connect();
            }
            catch (Exception ioe)
            {
                Console.Error.WriteLine(ioe.ToString());
            }


            if (ChannelGrid)
            {
                (new AmcpRequest(client, "CHANNEL_GRID", 0)).NoResponseRequest();
                Thread.Sleep(1000);
                (new AmcpRequest(client, "MIXER", 5, "GRID", "2", "2")).GetResponse();
            }
        }

        void Server_VirtualPlayHeadPositionUpdated(OscMessage message)
        {
            string[] addressParts = message.Address.Split('/');
            int channelNumber = int.Parse(addressParts[2]);
            Channel channel = channels[channelNumber - 1]; // CasparCG channelIds are offset by 1
            if (channel.Producer != null)
            {
                channel.Producer.virtualPlaybackHead = (ulong)((long)message[0]);
                channel.Producer.virtualTotalFrames = (ulong)((long)message[1]);
            }
        }

        void Server_PlayHeadPositionUpdated(OscMessage message)
        {
            string[] addressParts = message.Address.Split('/');
            int channelNumber = int.Parse(addressParts[2]);
            Channel channel = channels[channelNumber - 1]; // CasparCG channelIds are offset by 1
            if (channel.Producer != null)
            {
                channel.Producer.playbackHead = (ulong)((long)message[0]);
                channel.Producer.totalFrames = (ulong)((long)message[1]);
            }
        }

        void Server_RecordingHeadPositionUpdated(OscMessage message)
        {
            string[] addressParts = message.Address.Split('/');
            int channelNumber = int.Parse(addressParts[2]);
            Channel channel = channels[channelNumber - 1]; // CasparCG channelIds are offset by 1
            if (channel.Consumer != null)
            {
                channel.Consumer.recordingHead = (ulong)((long)message[0]);
            }
            currentFrameInput = Math.Max(currentFrameInput, channel.Consumer.recordingHead);
        }

        private event OscMessageEvent PlayHeadPositionUpdated;
        private event OscMessageEvent VirtualPlayHeadPositionUpdated;
        private event OscMessageEvent RecordingHeadPositionUpdated;

        public void Disconnect()
        {
            /* shutDown = true;
            updateThread.Join(); */

            foreach (Channel ch in channels)
            {
                if (ch.Consumer != null)
                {
                    (new AmcpRequest(client, "REMOVE", ch, "REPLAY")).GetResponse();
                }
            }

            client.Disconnect();
            oscListener.Close();
        }

        private void UpdateStatus()
        {
            while (!this.shutDown)
            {
                Channel latestChannel = channels[0];
                foreach (Channel channel in channels)
                {
                    AmcpResponse channelInfo = (new AmcpRequest(client, "INFO", channel)).GetResponse();
                    XmlDocument status = new XmlDocument();
                    status.LoadXml(channelInfo.Content);
                    XmlNodeList consumerNodes = status.GetElementsByTagName("consumer");
                    if (consumerNodes.Count > 0) {
                        XmlNode consumerNode = consumerNodes[0];
                        foreach (XmlNode node in consumerNode.ChildNodes)
                        {
                            if (node.LocalName == "recording-head")
                            {
                                if (channel.Consumer != null)
                                {
                                    channel.Consumer.recordingHead = ulong.Parse(node.InnerText);
                                    if (latestChannel.Consumer.recordingHead > channel.Consumer.recordingHead)
                                        latestChannel = channel;
                                }
                            }
                        }
                    }
                    XmlNodeList foregroundProducerNodes = status.GetElementsByTagName("foreground");
                    if (foregroundProducerNodes.Count > 0)
                    {
                        foreach (XmlNode producerNode in foregroundProducerNodes[0].ChildNodes)
                        {
                            foreach (XmlNode node in producerNode.ChildNodes)
                            {
                                if (node.LocalName == "play-head")
                                {
                                    if (channel.Producer != null)
                                    {
                                        try
                                        {
                                            channel.Producer.playbackHead = ulong.Parse(node.InnerText);
                                        }
                                        catch (ArgumentOutOfRangeException aoore)
                                        {

                                        }
                                    }
                                }
                            }
                        }
                    }
                    XmlNodeList backgroundProducerNodes = status.GetElementsByTagName("background");
                    if (backgroundProducerNodes.Count > 0)
                    {
                        foreach (XmlNode producerNode in backgroundProducerNodes[0].ChildNodes)
                        {
                            foreach (XmlNode node in producerNode.ChildNodes)
                            {
                                if (node.LocalName == "type")
                                {
                                    if (node.InnerText == "empty-producer")
                                    {
                                        channel.hasNext = false;
                                    }
                                    else
                                    {
                                        channel.hasNext = true;
                                    }
                                }
                            }
                        }
                    }
                }

                foreach (Channel channel in channels)
                {
                    if (channel.Consumer != null)
                    {
                        channel.Consumer.Offset = (int)channel.Consumer.recordingHead - (int)latestChannel.Consumer.recordingHead;
                    }
                }

                if (latestChannel.Consumer != null)
                    currentFrameInput = latestChannel.Consumer.recordingHead;
                else
                    currentFrameInput = 0;

                Thread.Sleep(100);
            }
        }
    }
}
