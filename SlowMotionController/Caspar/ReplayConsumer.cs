using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlowMotionController.Caspar
{
    public class ReplayConsumer
    {
        private long offset;
        internal Channel channel;

        internal ulong recordingHead = 0;

        internal String fileName = "";

        public ReplayConsumer(Channel Channel, String FileName)
        {
            this.channel = Channel;
            this.fileName = FileName;
        }

        public ReplayConsumer(String FileName)
        {
            this.fileName = FileName;
        }

        public Channel Channel
        {
            get { return channel; }
            set { channel = value; }
        }

        public long Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        public ulong RecordingHead
        {
            get { return recordingHead; }
        }

        public String FileName
        {
            get { return fileName; }
        }
    }
}
