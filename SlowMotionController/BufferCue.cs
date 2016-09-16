using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SlowMotionController.Caspar;

namespace SlowMotionController
{
    class BufferCue
    {
        private uint id;

        private ulong inFrame;
        private ulong outFrame;

        private String fileName;

        private String comments;

        private LinkedList<String> tags;

        private Channel channel;

        public BufferCue(ulong InFrame, ulong OutFrame, String FileName)
        {
            this.inFrame = InFrame;
            this.outFrame = OutFrame;
            this.fileName = FileName;
            this.tags = new LinkedList<string>();
        }

        public BufferCue(BufferCue source)
        {
            this.inFrame = source.InFrame;
            this.outFrame = source.OutFrame;
            this.fileName = source.FileName;
            this.tags = new LinkedList<string>();
            foreach (string tag in source.Tags)
            {
                this.tags.AddLast(tag);
            }
            this.channel = source.Channel;
        }

        public BufferCue(ulong InFrame, ulong OutFrame, Channel Channel)
            : this(InFrame, OutFrame, Channel.Consumer.FileName)
        {
            this.channel = Channel;
        }

        public uint Id
        {
            get { return id; }
            set { id = value; }
        }

        public ulong InFrame
        {
            get { return inFrame; }
            set { inFrame = value; }
        }

        public ulong OutFrame
        {
            get { return outFrame; }
            set { outFrame = value; }
        }

        public String FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        public String Comments
        {
            get { return comments; }
            set { comments = value; }
        }

        public LinkedList<String> Tags
        {
            get { return tags; }
        }

        public Channel Channel
        {
            get { return channel; }
            set { channel = value; fileName = channel.Consumer.FileName; }
        }
    }
}
