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

        private ulong InPointTemp = 0;
        private ulong OutPointTemp = 0;

        private double currentSpeed = 1;

        private uint SystemFramerate = 50;

        private ulong NoInPointDiff = 50;

        CueList cues;

        public MainWindow()
        {
            InitializeComponent();

            cues = new CueList();
            highlightCues = new CueList();

            this.SystemFramerate = (uint)SlowMotionController.Properties.Settings.Default.SystemFramerate;

            this.NoInPointDiff = 5 * SystemFramerate;

            ServerAddressComboBox.Text = SlowMotionController.Properties.Settings.Default.DefaultServer;
            DefaultReplayDuration.Value = SlowMotionController.Properties.Settings.Default.DefaultReplayDuration;
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
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

                    server = new Server(client, true);

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
            cues.Add(cue);

            AddCueToList(cue);

            ClearPointTemp();
        }

        private void AddCueToList(BufferCue cue)
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
        }

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
                            Tag("Home");
                            break;
                        case Keys.X:
                            Tag("Away");
                            break;
                        case Keys.C:
                            Tag("Goal");
                            break;
                        case Keys.V:
                            Tag("NoGoal");
                            break;
                        case Keys.B:
                            Tag("User1");
                            break;
                        case Keys.N:
                            Tag("User2");
                            break;
                        case Keys.M:
                            Tag("User3");
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
            if (CueListView.SelectedItems.Count > 0)
            {
                BufferCue cue = CueListView.SelectedItems[0].Tag as BufferCue;
                cue.Channel = server.Channels[Camera];
                CueListView.SelectedItems[0].SubItems[4].Text = cue.Channel.Id.ToString();
            }
        }

        private void CueOneSelected()
        {
            if (CueListView.SelectedItems.Count > 0)
            {
                BufferCue cue = CueListView.SelectedItems[0].Tag as BufferCue;
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
                BufferCue cue = CueListView.SelectedItems[0].Tag as BufferCue;
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
            /* if (highlightCues.Count > 0)
            {
                BufferCue cue = highlightCues.First.Value;
                highlightCues.RemoveFirst();
                (new AmcpRequest(client, "PLAY", server.Channels[2], cue.Channel.Consumer.FileName, "SEEK", (cue.InFrame + cue.Channel.Consumer.Offset).ToString(), "SPEED", currentSpeed.ToString().Replace(',', '.'), "LENGTH", (cue.OutFrame - cue.InFrame).ToString())).GetResponse();

                while (highlightCues.Count > 0)
                {
                    BufferCue nextCue = highlightCues.First.Value;
                    highlightCues.RemoveFirst();
                    (new AmcpRequest(client, "LOADBG", server.Channels[2], nextCue.Channel.Consumer.FileName, "AUTO", "SEEK", (nextCue.InFrame + nextCue.Channel.Consumer.Offset).ToString(), "SPEED", currentSpeed.ToString().Replace(',', '.'), "LENGTH", (nextCue.OutFrame - nextCue.InFrame).ToString())).GetResponse();
                    while (server.Channels[2].HasNext)
                    {
                        Thread.Sleep(1000);
                    }
                }
            } */
        }

        private void StopHighlightsPlaylist()
        {
            if (highlightsThread != null)
            {
                highlightsThread.Abort();
                highlightsThread = null;
            }
        }

        private void PlayAllSelected()
        {
            highlightCues = new LinkedList<BufferCue>();
            foreach (ListViewItem item in CueListView.SelectedItems)
            {
                BufferCue cue = item.Tag as BufferCue;
                highlightCues.AddLast(cue);
            }
            highlightsThread = new Thread(new ThreadStart(PlayHighlightsPlaylist));
            highlightsThread.Start();
        }

        private void Tag(string tag)
        {
            if (CueListView.SelectedItems.Count > 0)
            {
                foreach (ListViewItem item in CueListView.SelectedItems)
                {
                    BufferCue cue = item.Tag as BufferCue;
                    if (cue.Tags.Contains(tag))
                    {
                        cue.Tags.Remove(tag);
                    }
                    else
                    {
                        cue.Tags.AddLast(tag);
                    }

                    String tags = "";
                    foreach (String ctag in cue.Tags)
                    {
                        tags += (tags != "" ? ", " : "") + ctag;
                    }
                    item.SubItems[3].Text = tags;
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
            (new AmcpRequest(client, "CALL", server.Channels[3], "SEEK", "-1")).GetResponse();
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
            List<ListViewItem> toDelete = new List<ListViewItem>();
            foreach (ListViewItem item in CueListView.SelectedItems)
            {
                toDelete.Add(item);
                BufferCue cue = item.Tag as BufferCue;
                cues.Remove(cue);
            }
            foreach (ListViewItem item in toDelete)
            {
                CueListView.Items.Remove(item);
            }
        }

        private void CloneSelected()
        {
            foreach (ListViewItem item in CueListView.SelectedItems)
            {
                BufferCue cue = item.Tag as BufferCue;
                //cues.Remove(cue);
                BufferCue newCue = new BufferCue(cue);
                cues.Add(newCue);
                AddCueToList(newCue);
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
            // PlaybackName.Text = "HIGHLIGHTS";
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
                sw.WriteLine(String.Format("# {0:0000}, Channel {1:00}\t{2}\t{3}", cue.Id, cue.Channel.Id, cue.FileName, TagsToString(cue.Tags)));
                sw.WriteLine(String.Format("ch{0} {1:0}-{2:0}", cue.Channel.Id, cue.InFrame, cue.OutFrame));
            }
            sw.Close();
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

    }
}
