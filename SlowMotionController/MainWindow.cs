using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SlowMotionController.Amcp;
using SlowMotionController.Caspar;
using SlowMotionController.Properties;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace SlowMotionController
{
    public partial class MainWindow : Form
    {
        AmcpClient client;
        Server server;
        Channel currentPGMChannel;

        private LinkedList<BufferCue> highlightCues;
        private Thread highlightsThread;
        private Boolean shutdownHiglights = false;

        private ulong InPointTemp = 0;
        private ulong OutPointTemp = 0;

        private double currentSpeed = 1;

        private uint SystemFramerate = 50;

        private ulong NoInPointDiff = 50;

        CueList cues;

        TagRenderer TagItemRenderer;
        List<string> Tags = new List<string>();

        public MainWindow()
        {
            InitializeComponent();

            cues = new CueList();
            highlightCues = new CueList();

            this.SystemFramerate = (uint)SlowMotionController.Properties.Settings.Default.SystemFramerate;

            this.NoInPointDiff = 5 * SystemFramerate;

            ServerAddressComboBox.Text = SlowMotionController.Properties.Settings.Default.DefaultServer;
            DefaultReplayDuration.Value = SlowMotionController.Properties.Settings.Default.DefaultReplayDuration;

            this.TagItemRenderer = new TagRenderer();

            foreach (string TagAndColor in SlowMotionController.Properties.Settings.Default.Tags)
            {
                string[] separate = TagAndColor.Split(',');
                Tags.Add(separate[0]);
                if (separate.Length > 1)
                {
                    this.TagItemRenderer.TagBrushes.Add(separate[0], new SolidBrush(Color.FromName(separate[1])));
                }
            }

            CommentsColumn.Renderer = TagItemRenderer;
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor; // Connecting can take some time

            IPAddress[] addresses = Dns.GetHostAddresses(ServerAddressComboBox.Text);
            IPAddress selectedAdress = null;

            foreach (IPAddress address in addresses)
            {
                if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    selectedAdress = address;
                }
            }

            if (selectedAdress != null)
            {
                try
                {
                    client = new AmcpClient(new IPEndPoint(selectedAdress, 5250));
                    AmcpRequest request = new AmcpRequest(client, "VERSION", 0, 0);
                    AmcpResponse response = request.GetResponse();
                    StatusLabel.Text = "Connected to: " + client.ServerEndPoint.ToString() + ", server version: " + response.Content;

                    string[] ingestDevices = Properties.Settings.Default.IngestDecklinkDevices.Split(',');

                    server = new Server(client, ingestDevices, true);

                    StatusUpdate.Enabled = true;
                    currentPGMChannel = server.Channels[0];

                    PlaybackName.Text = "IN" + server.Channels[0].Id;
                    (new AmcpRequest(client, "PLAY", 5, 100, "MULTIVIEW\\\\CM" + 1)).GetResponse();
                }
                catch (SocketException se)
                {
                    MessageBox.Show(se.Message, "Network error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            this.Cursor = this.DefaultCursor;
        }

        private String FormatTime(ulong Frames, ulong FrameRate)
        {
            ulong SecondsTotal = (Frames / FrameRate);
            ulong MinutesTotal = SecondsTotal / 60;
            ulong Hours = MinutesTotal / 60;

            ulong Minutes = MinutesTotal - Hours * 60;
            ulong Seconds = SecondsTotal - MinutesTotal * 60;
            ulong ActualFrame = Frames % FrameRate;

            return String.Format("{0:00}:{1:00}:{2:00}.{3:00}", Hours, Minutes, Seconds, ActualFrame);
        }

        private void StatusUpdate_Tick(object sender, EventArgs e)
        {
            CurrentTime.Text = FormatTime(server.RecordingHead, SystemFramerate);
            PlaybackTime.Text = FormatTime((ulong)((long)server.Channels[3].Producer.PlaybackHead - currentPGMChannel.Consumer.Offset), SystemFramerate);
        }

        private void SetInPointTemp()
        {
            InPointTemp = server.Channels[3].Producer.PlaybackHead;
            InPointLabel.Text = FormatTime(InPointTemp, SystemFramerate);
            OutPointTemp = 0;
            OutPointLabel.Text = FormatTime(OutPointTemp, SystemFramerate);
        }

        private void SetOutPointTemp()
        {
            OutPointTemp = server.Channels[3].Producer.PlaybackHead;
            if (OutPointTemp < NoInPointDiff)
                InPointTemp = 1;
            if (InPointTemp == 0)
                InPointLabel.Text = FormatTime(OutPointTemp - NoInPointDiff, SystemFramerate);
            OutPointLabel.Text = FormatTime(OutPointTemp, SystemFramerate);

            BufferCue cue = new BufferCue((InPointTemp > 0 ? InPointTemp : OutPointTemp - NoInPointDiff), OutPointTemp, currentPGMChannel);

            CueListView.AddObject(cue);

            // AddCueToList(cue);

            ClearPointTemp();
        }

        /* private void AddCueToList(BufferCue cue)
        {
            ListViewItem item = CueListView.Items.Add(CreateListViewItem(cue));

            CueListView.SelectedItems.Clear();
            CueListView.SelectedIndices.Add(item.Index);
            CueListView.Focus();
            CueListView.SelectedItems[0].EnsureVisible();
        }

        private ListViewItem CreateListViewItem(BufferCue cue)
        {
            ListViewItem item = new ListViewItem(cue.Id.ToString("000"));
            item.Tag = cue;
            item.SubItems.Add(FormatTime(cue.InFrame, SystemFramerate));
            item.SubItems.Add(FormatTime(cue.OutFrame - cue.InFrame, SystemFramerate));
            //item.SubItems.Add(cue.Comments);
            String tags = "";
            foreach (String tag in cue.Tags)
            {
                tags += tag + ", ";
            }
            item.SubItems.Add(tags);

            item.SubItems.Add(cue.Channel.Id.ToString());

            return item;
        } */

        private void ClearPointTemp()
        {
            new Thread(new ThreadStart(() => {
                ulong InPointBuf = InPointTemp;
                ulong OutPointBuf = OutPointTemp;
                Thread.Sleep(350);
                if ((InPointBuf == InPointTemp) && (OutPointBuf == OutPointTemp))
                {
                    InPointLabel.BeginInvoke((Action)(() =>
                    {
                        InPointTemp = 0;
                        InPointLabel.Text = FormatTime(InPointTemp, SystemFramerate);
                        OutPointTemp = 0;
                        OutPointLabel.Text = FormatTime(OutPointTemp, SystemFramerate);
                    }));
                }
            })).Start();
        }

        private void MarkInButton_Click(object sender, EventArgs e)
        {
            SetInPointTemp();
        }

        private void MarkOutButton_Click(object sender, EventArgs e)
        {
            SetOutPointTemp();
        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if (!ServerAddressComboBox.Focused)
            {
                if (e.Modifiers == Keys.None)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.Up:
                        case Keys.Down:
                            CueListView.Focus();
                            break;
                        case Keys.I:
                            SetInPointTemp();
                            break;
                        case Keys.O:
                            SetOutPointTemp();
                            break;
                        case Keys.D1:
                            SwitchToCamera1();
                            break;
                        case Keys.D2:
                            SwitchToCamera2();
                            break;
                        case Keys.D3:
                            SwitchToCamera3();
                            break;
                        case Keys.Z:
                            Tag(Tags[0]);
                            break;
                        case Keys.X:
                            Tag(Tags[1]);
                            break;
                        case Keys.C:
                            Tag(Tags[2]);
                            break;
                        case Keys.V:
                            Tag(Tags[3]);
                            break;
                        case Keys.B:
                            Tag(Tags[4]);
                            break;
                        case Keys.N:
                            Tag(Tags[5]);
                            break;
                        case Keys.M:
                            Tag(Tags[6]);
                            break;
                        case Keys.Enter:
                            if ((InPointTemp > 0) && (OutPointTemp == 0))
                                SetOutPointTemp();
                            CueOneSelected();
                            break;
                        case Keys.J:
                            SetShuttleSpeed(-1);
                            break;
                        case Keys.K:
                        case Keys.Q:
                            SetSpeed(0);
                            SpeedBar.Value = 0;
                            ClearShuttleSpeed();
                            break;
                        case Keys.W:
                            SetSpeed(0.25);
                            SpeedBar.Value = 25;
                            ClearShuttleSpeed();
                            break;
                        case Keys.E:
                            SetSpeed(0.33);
                            SpeedBar.Value = 33;
                            ClearShuttleSpeed();
                            break;
                        case Keys.R:
                            SetSpeed(0.5);
                            SpeedBar.Value = 50;
                            ClearShuttleSpeed();
                            break;
                        case Keys.T:
                            SetSpeed(0.75);
                            SpeedBar.Value = 75;
                            ClearShuttleSpeed();
                            break;
                        case Keys.L:
                        case Keys.Y:
                            SetSpeed(1);
                            SpeedBar.Value = 100;
                            ClearShuttleSpeed();
                            break;
                        case Keys.Escape:
                            CueListView.Focus();
                            break;
                        case Keys.Delete:
                            DeleteSelected();
                            break;
                        case Keys.S:
                            if (SpeedBar.Value + 10 <= SpeedBar.Maximum)
                                SpeedBar.Value += 10;
                            else
                                SpeedBar.Value = SpeedBar.Maximum;
                            SetSpeed((double)SpeedBar.Value / (double)SpeedBar.Maximum);
                            ClearShuttleSpeed();
                            break;
                        case Keys.A:
                            if (SpeedBar.Value - 10 >= 0)
                                SpeedBar.Value -= 10;
                            else
                                SpeedBar.Value = 0;
                            SetSpeed((double)SpeedBar.Value / (double)SpeedBar.Maximum);
                            ClearShuttleSpeed();
                            break;
                        case Keys.Add:
                        case Keys.Insert:
                            CloneSelected();
                            break;
                        case Keys.OemPeriod:
                            GoLive();
                            break;
                        case Keys.OemSemicolon:
                            GoBack1();
                            break;
                        case Keys.OemQuotes:
                            GoForward1();
                            break;
                    }
                }
                else if (e.Modifiers == Keys.Control)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.D1:
                            ModifyCueCamera(0);
                            break;
                        case Keys.D2:
                            ModifyCueCamera(1);
                            break;
                        case Keys.D3:
                            ModifyCueCamera(2);
                            break;
                        case Keys.Enter:
                            if ((InPointTemp > 0) && (OutPointTemp == 0))
                                SetOutPointTemp();
                            PlayOneSelected();
                            break;
                    }
                }
                else if (e.Modifiers == Keys.Shift)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.Enter:
                            if ((InPointTemp > 0) && (OutPointTemp == 0))
                                SetOutPointTemp();
                            PlayOneSelected(0.5);
                            break;
                    }
                }
                else if (e.Modifiers == (Keys.Control | Keys.Shift))
                {
                    switch (e.KeyCode)
                    {
                        case Keys.G:
                            StopShuttle();
                            break;
                        case Keys.H:
                            ShuttlePlus05();
                            break;
                        case Keys.J:
                            ShuttlePlus10();
                            break;
                        case Keys.K:
                            ShuttlePlus20();
                            break;
                        case Keys.L:
                            ShuttlePlus40();
                            break;
                        case Keys.P:
                            ShuttlePlus100();
                            break;
                        case Keys.O:
                            ShuttlePlus400();
                            break;
                        case Keys.F:
                            ShuttleMinus05();
                            break;
                        case Keys.D:
                            ShuttleMinus10();
                            break;
                        case Keys.S:
                            ShuttleMinus20();
                            break;
                        case Keys.A:
                            ShuttleMinus40();
                            break;
                        case Keys.Q:
                            ShuttleMinus100();
                            break;
                        case Keys.W:
                            ShuttleMinus400();
                            break;


                        case Keys.Y:
                            GoForward1Frame();
                            break;
                        case Keys.T:
                            GoBack1Frame();
                            break;
                    }
                }

                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void ModifyCueCamera(int Camera)
        {
            if (CueListView.SelectedObjects.Count > 0)
            {
                BufferCue cue = CueListView.SelectedObjects[0] as BufferCue;
                cue.Channel = server.Channels[Camera];
                CueListView.RefreshObject(cue);
            }
        }

        private void CueOneSelected()
        {
            if (CueListView.SelectedItems.Count > 0)
            {
                BufferCue cue = CueListView.SelectedObjects[0] as BufferCue;
                (new AmcpRequest(client, "PLAY", server.Channels[3], cue.Channel.Consumer.FileName, "SEEK", ((long)cue.InFrame + cue.Channel.Consumer.Offset).ToString(), "SPEED", "0", "AUDIO", "1")).GetResponse();
                SpeedBar.Value = 0;
                currentPGMChannel = cue.Channel;
                UpdateCameraButtons();
                PlaybackName.Text = "IN" + cue.Channel.Id.ToString() + " (" + cue.Id.ToString("000") + ")";
            }
        }

        private void PlayOneSelected(double speed = -1)
        {
            if (CueListView.SelectedItems.Count > 0)
            {
                BufferCue cue = CueListView.SelectedObjects[0] as BufferCue;
                if (speed == -1)
                {
                    (new AmcpRequest(client, "PLAY", server.Channels[3], cue.Channel.Consumer.FileName, "SEEK", ((long)cue.InFrame + cue.Channel.Consumer.Offset).ToString(), "SPEED", ((double)SpeedBar.Value / (double)SpeedBar.Maximum).ToString().Replace(',', '.'), "AUDIO", "1")).GetResponse();
                }
                else
                {
                    (new AmcpRequest(client, "PLAY", server.Channels[3], cue.Channel.Consumer.FileName, "SEEK", ((long)cue.InFrame + cue.Channel.Consumer.Offset).ToString(), "SPEED", (speed).ToString().Replace(',', '.'), "AUDIO", "1")).GetResponse();
                    SpeedBar.Value = (int)(speed * SpeedBar.Maximum);
                }
                currentPGMChannel = cue.Channel;
                UpdateCameraButtons();
                PlaybackName.Text = "IN" + cue.Channel.Id.ToString() + " (" + cue.Id.ToString("000") + ")";
            }
        }

        private void PlayHighlightsPlaylist()
        {
            try
            {
                if (highlightCues.Count > 0)
                {
                    BufferCue cue = highlightCues.First.Value;
                    highlightCues.RemoveFirst();
                    (new AmcpRequest(client, "PLAY", server.Channels[3], cue.Channel.Consumer.FileName, "SEEK", ((long)cue.InFrame + cue.Channel.Consumer.Offset).ToString(), "SPEED", currentSpeed.ToString().Replace(',', '.'), "LENGTH", (cue.OutFrame - cue.InFrame).ToString())).GetResponse();

                    while (highlightCues.Count > 0)
                    {
                        BufferCue nextCue = highlightCues.First.Value;
                        highlightCues.RemoveFirst();
                        (new AmcpRequest(client, "LOADBG", server.Channels[3], nextCue.Channel.Consumer.FileName, "AUTO", "SEEK", ((long)nextCue.InFrame + nextCue.Channel.Consumer.Offset).ToString(), "SPEED", currentSpeed.ToString().Replace(',', '.'), "LENGTH", (nextCue.OutFrame - nextCue.InFrame).ToString(), "MIX", "10")).GetResponse();
                        Console.WriteLine("Scheduling " + nextCue.Channel.Consumer.FileName + " @ " + nextCue.InFrame);
                        while (server.Channels[3].Producer.virtualPlaybackHead < server.Channels[3].Producer.virtualTotalFrames - (3 * SystemFramerate))
                        {
                            Thread.Sleep(1000);
                        }
                    }
                }
            }
            catch (ThreadAbortException tae)
            {
                Console.WriteLine("Aborting highlights playback.");
            }
        }

        private void StopHighlightsPlaylist()
        {
            if (highlightsThread != null)
            {
                shutdownHiglights = true;
                highlightsThread.Abort();
                highlightsThread = null;
            }
        }

        private void PlayAllSelected()
        {
            highlightCues = new LinkedList<BufferCue>();
            foreach (BufferCue item in CueListView.SelectedObjects)
            {
                highlightCues.AddLast(item);
            }
            highlightsThread = new Thread(new ThreadStart(PlayHighlightsPlaylist));
            highlightsThread.Start();
        }

        private void Tag(string tag)
        {
            if (CueListView.SelectedObjects.Count > 0)
            {
                foreach (BufferCue cue in CueListView.SelectedObjects)
                {
                    if (cue.Tags.Contains(tag))
                    {
                        cue.Tags.Remove(tag);
                    }
                    else
                    {
                        cue.Tags.AddLast(tag);
                    }
                    CueListView.RefreshObject(cue);
                }
            }
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (server != null)
            {
                server.Disconnect();
            }
        }

        private void GoBack1Frame()
        {
            if (SystemFramerate > 30) // TODO: There is a bug in the MAV system that causes the system to fail 1 frame backwards seeks. This is a workaround for this bug.
            {
                (new AmcpRequest(client, "CALL", server.Channels[3], "SEEK", "-2")).GetResponse();
            }
            else
            {
                (new AmcpRequest(client, "CALL", server.Channels[3], "SEEK", "-1")).GetResponse();
            }
        }

        private void GoForward1Frame()
        {
            (new AmcpRequest(client, "CALL", server.Channels[3], "SEEK", "+1")).GetResponse();
        }

        private void GoBack2()
        {
            (new AmcpRequest(client, "CALL", server.Channels[3], "SEEK", "-" + (2 * SystemFramerate))).GetResponse();
        }

        private void GoBack1()
        {
            (new AmcpRequest(client, "CALL", server.Channels[3], "SEEK", "-" + (1 * SystemFramerate))).GetResponse();
        }

        private void GoForward1()
        {
            (new AmcpRequest(client, "CALL", server.Channels[3], "SEEK", "+" + (1 * SystemFramerate).ToString())).GetResponse();
        }

        private void GoForward2()
        {
            (new AmcpRequest(client, "CALL", server.Channels[3], "SEEK", "+" + (2 * SystemFramerate).ToString())).GetResponse();
        }

        private void GoToFrame(ulong Frame)
        {
            (new AmcpRequest(client, "CALL", server.Channels[3], "SEEK", (Frame).ToString())).GetResponse();
        }

        private void GoLive()
        {
            SpeedBar.Value = SpeedBar.Maximum;
            (new AmcpRequest(client, "CALL", server.Channels[3], "SEEK", "|10")).GetResponse();
            (new AmcpRequest(client, "CALL", server.Channels[3], "SPEED", "1.0")).GetResponse();
        }

        private void SetSpeed(double Speed)
        {
            currentSpeed = Speed;
            (new AmcpRequest(client, "CALL", server.Channels[3], "SPEED", Speed.ToString().Replace(',', '.'))).GetResponse();
        }

        private void Minus2Button_Click(object sender, EventArgs e)
        {
            GoBack2();
        }

        private void LiveButton_Click(object sender, EventArgs e)
        {
            PlaybackName.Text = "IN" + currentPGMChannel.Id.ToString();
            GoLive();
        }

        private void Minus1Button_Click(object sender, EventArgs e)
        {
            GoBack1();
        }

        private void Plus1Button_Click(object sender, EventArgs e)
        {
            GoForward1();
        }

        private void Plus2Button_Click(object sender, EventArgs e)
        {
            GoForward2();
        }

        private void SpeedBar_Scroll(object sender, EventArgs e)
        {
            SetSpeed((double)SpeedBar.Value / (double)SpeedBar.Maximum);
            ClearShuttleSpeed();
        }

        private void Speed25_Click(object sender, EventArgs e)
        {
            SetSpeed(0.25);
            ClearShuttleSpeed();
            SpeedBar.Value = 25;
        }

        private void Speed33_Click(object sender, EventArgs e)
        {
            SetSpeed(0.33);
            ClearShuttleSpeed();
            SpeedBar.Value = 33;
        }

        private void Speed50_Click(object sender, EventArgs e)
        {
            SetSpeed(0.50);
            ClearShuttleSpeed();
            SpeedBar.Value = 50;
        }

        private void Speed75_Click(object sender, EventArgs e)
        {
            SetSpeed(0.75);
            ClearShuttleSpeed();
            SpeedBar.Value = 75;
        }

        private void Speed100_Click(object sender, EventArgs e)
        {
            SetSpeed(1);
            ClearShuttleSpeed();
            SpeedBar.Value = 100;
        }

        private void SwitchRecordChannel(int Camera)
        {
            if (currentPGMChannel != server.Channels[Camera])
            {
                currentPGMChannel = server.Channels[Camera];
                UpdateCameraButtons();
                (new AmcpRequest(client, "PLAY", 5, 100, "MULTIVIEW\\\\CM" + (Camera + 1))).GetResponse();
                ulong CurrentFrame = server.Channels[3].Producer.PlaybackHead;
                (new AmcpRequest(client, "PLAY", server.Channels[3], server.Channels[Camera].Consumer.FileName, "SEEK", ((long)CurrentFrame + server.Channels[Camera].Consumer.Offset).ToString(), "SPEED", ((double)SpeedBar.Value / (double)SpeedBar.Maximum).ToString().Replace(',', '.'), "AUDIO", "1")).GetResponse();
                PlaybackName.Text = "IN" + server.Channels[Camera].Id.ToString();
            }
        }

        private void SwitchToCamera1()
        {
            SwitchRecordChannel(0);
        }

        private void SwitchToCamera2()
        {
            SwitchRecordChannel(1);
        }

        private void SwitchToCamera3()
        {
            SwitchRecordChannel(2);
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            StatusUpdate.Stop();

            SlowMotionController.Properties.Settings.Default.DefaultReplayDuration = (int)DefaultReplayDuration.Value;
            SlowMotionController.Properties.Settings.Default.Save();

            this.Dispose();
        }

        private void Camera1Button_Click(object sender, EventArgs e)
        {
            if (Camera1Button.Checked)
                SwitchToCamera1();
        }

        private void Camera2Button_Click(object sender, EventArgs e)
        {
            if (Camera2Button.Checked)
                SwitchToCamera2();
        }

        private void Camera3Button_Click(object sender, EventArgs e)
        {
            if (Camera3Button.Checked)
                SwitchToCamera3();
        }

        private void UpdateCameraButtons()
        {
            if (currentPGMChannel.Id == 1)
            {
                Camera1Button.Checked = true;
            }
            else if (currentPGMChannel.Id == 2)
            {
                Camera2Button.Checked = true;
            }
            else if (currentPGMChannel.Id == 3)
            {
                Camera3Button.Checked = true;
            }
        }

        private void PlaySingleSelectedButton_Click(object sender, EventArgs e)
        {
            CueOneSelected();
        }

        private void CueListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DeleteSelected()
        {
            List<BufferCue> toDelete = new List<BufferCue>();
            foreach (BufferCue item in CueListView.SelectedObjects)
            {
                toDelete.Add(item);
            }
            foreach (BufferCue item in toDelete)
            {
                CueListView.RemoveObject(item);
            }
        }

        private void CloneSelected()
        {
            foreach (BufferCue item in CueListView.SelectedObjects)
            {
                BufferCue newCue = new BufferCue(item);
                //CueListView.AddObject(newCue);
                CueListView.AddObject(newCue);
                // AddCueToList(newCue);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DeleteSelected();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CloneSelected();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            SetSpeed(0);
            SpeedBar.Value = 0;
        }

        private void PlayAllSelectedButton_Click(object sender, EventArgs e)
        {
            PlaybackName.Text = "HIGHLIGHTS";
            PlayAllSelected();
        }

        private String TagsToString(LinkedList<String> Tags)
        {
            String result = "";
            foreach (String item in Tags)
            {
                result += (result == "" ? "" : ",") + item;
            }
            return result;
        }

        private void SaveCues(String FileName)
        {
            FileStream fs = new FileStream(FileName, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.ASCII);
            sw.WriteLine("mplayer EDL file, version 2");
            foreach (Channel ch in server.Channels) {
                if (ch.Consumer != null)
                {
                    sw.WriteLine("< ch" + ch.Id + " " + ch.Consumer.FileName);
                }
            }
            foreach (BufferCue cue in cues)
            {
                sw.WriteLine(String.Format("ch{0} {1:0}-{2:0} # {3:0000}, Channel {4:00}; {5}; {6}", cue.Channel.Id, cue.InFrame, cue.OutFrame, cue.Id, cue.Channel.Id, cue.FileName, TagsToString(cue.Tags)));
            }
            sw.Close();
        }

        private void ImportCues(String FileName)
        {
            char[] separators = new char[] { ' ', '\t' };
            FileStream fs = new FileStream(FileName, FileMode.Open);
            StreamReader sr = new StreamReader(fs, Encoding.ASCII);
            Dictionary<string, string> sources = new Dictionary<string, string>();
            string header = sr.ReadLine();
            if (header.StartsWith("mplayer EDL")) // valid EDL file
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (line.StartsWith("<")) // Source video defines
                    {
                        string[] elements = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        sources.Add(elements[1], elements[2]);
                    }
                    else // Cue points
                    {
                        string[] elements = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        string[] timecodes = elements[1].Split('-');
                        BufferCue cue = new BufferCue(UInt64.Parse(timecodes[0]), UInt64.Parse(timecodes[1]), sources[elements[0]]);
                        CueListView.AddObject(cue);
                    }
                }
            }
        }

        private void SaveToFile_Click(object sender, EventArgs e)
        {
            if (SaveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SaveCues(SaveFileDialog.FileName);
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            SystemStatusLabel.Text = "System framerate: " + this.SystemFramerate + " fps";
            CueListView.SetObjects(cues);
        }

        private void replayDuration_ValueChanged(object sender, EventArgs e)
        {
            NoInPointDiff = (ulong)(DefaultReplayDuration.Value * SystemFramerate);
        }

        private void CurrentTime_Click(object sender, EventArgs e)
        {

        }

        private void ShuttleTrackBar_MouseUp(object sender, MouseEventArgs e)
        {
            StopShuttle();
        }

        private void DisplayShuttleSpeed(double Speed)
        {
            ShuttleSpeedLabel.Text = String.Format("{0:0.0}", Speed);
        }

        private void ClearShuttleSpeed()
        {
            ShuttleTrackBar.Value = 0;
            ShuttleSpeedLabel.Text = "";
        }

        private void StopShuttle()
        {
            SetSpeed(0);
            ClearShuttleSpeed();
        }

        private void KeyboardShuttle(double Speed)
        {
            int Sign = Speed > 0 ? 1 : -1;
            ShuttleTrackBar.Value = Sign * (int)(Math.Sqrt(Math.Abs(Speed)) * 3);
            SpeedBar.Value = 0;
            SetShuttleSpeed(Speed);
        }

        private void ShuttlePlus05()
        {
            KeyboardShuttle(0.5);
        }

        private void ShuttlePlus10()
        {
            KeyboardShuttle(1.0);
        }

        private void ShuttlePlus20()
        {
            KeyboardShuttle(2.0);
        }

        private void ShuttlePlus40()
        {
            KeyboardShuttle(4.0);
        }

        private void ShuttlePlus100()
        {
            KeyboardShuttle(10.0);
        }

        private void ShuttlePlus400()
        {
            KeyboardShuttle(40.0);
        }

        private void ShuttleMinus05()
        {
            KeyboardShuttle(-0.5);
        }

        private void ShuttleMinus10()
        {
            KeyboardShuttle(-1.0);
        }

        private void ShuttleMinus20()
        {
            KeyboardShuttle(-2.0);
        }

        private void ShuttleMinus40()
        {
            KeyboardShuttle(-4.0);
        }

        private void ShuttleMinus100()
        {
            KeyboardShuttle(-10.0);
        }

        private void ShuttleMinus400()
        {
            KeyboardShuttle(-40.0);
        }

        private void SetShuttleSpeed(double Speed)
        {
            SpeedBar.Value = 0;
            SetSpeed(Speed);
            DisplayShuttleSpeed(Speed);
        }

        private void ShuttleTrackBar_Scroll(object sender, EventArgs e)
        {
            SpeedBar.Value = 0;
            double Sign = ShuttleTrackBar.Value > 0 ? 1 : -1;
            SetShuttleSpeed(Sign * Math.Round(Math.Pow(ShuttleTrackBar.Value / 3.0d, 2d), 2));
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CueListView_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void CueListView_FormatCell(object sender, BrightIdeasSoftware.FormatCellEventArgs e)
        {
            if (e.CellValue != null)
            {
                if (e.ColumnIndex == 1 || e.ColumnIndex == 2)
                {
                    e.SubItem.Text = FormatTime((ulong)e.CellValue, SystemFramerate);
                }
                else if (e.ColumnIndex == 3)
                {
                    String tags = "";
                    foreach (String ctag in ((LinkedList<string>)e.CellValue))
                    {
                        tags += (tags != "" ? ", " : "") + ctag;
                    }
                    e.SubItem.Text = tags;
                }
            }
        }

        private void ImportFromFile_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ImportCues(OpenFileDialog.FileName);
            }
        }

    }
}
