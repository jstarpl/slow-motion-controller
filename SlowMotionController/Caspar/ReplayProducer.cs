using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlowMotionController.Caspar
{
    public class ReplayProducer
    {
        private int offset;
        internal Channel channel;

        internal ulong playbackHead = 0;

        public ReplayProducer(Channel Channel)
        {
            this.channel = Channel;
        }

        public ReplayProducer()
        {
            
        }

        public Channel Channel
        {
            get { return channel; }
            set { channel = value; }
        }

        public ulong PlaybackHead
        {
            get { return playbackHead; }
        }
    }
}
