using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlowMotionController.Caspar
{
    public class Channel
    {
        private ReplayConsumer consumer = null;
        private ReplayProducer producer = null;

        public ReplayConsumer Consumer
        {
            get { return consumer; }
            set { consumer = value; consumer.channel = this; }
        }

        public ReplayProducer Producer
        {
            get { return producer; }
            set { producer = value; producer.Channel = this; }
        }

        internal Boolean hasNext = false;

        public Boolean HasNext
        {
            get
            {
                return hasNext;
            }
        }

        private uint id;

        public uint Id
        {
            get { return id; }
        }

        public Channel(uint Id)
        {
            this.id = Id;
        }
    }
}
