using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlowMotionController
{
    class CueList : LinkedList<BufferCue>
    {
        public CueList()
            : base()
        {

        }

        public void Add(BufferCue value)
        {
            if (this.Count > 0)
                value.Id = this.Last.Value.Id + 1;
            else
                value.Id = 1;

            this.AddLast(value);
        }
    }
}
