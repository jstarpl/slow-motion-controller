using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlowMotionController.Amcp
{
    public class AmcpResponse
    {
        private int statusCode;
        private String statusDescription;
        private String content = "";

        internal AmcpResponse(int StatusCode, String StatusDescription)
        {
            this.statusCode = StatusCode;
            this.statusDescription = StatusDescription;
        }

        public int StatusCode
        {
            get { return statusCode; }
            internal set { statusCode = value; }
        }

        public String StatusDescription
        {
            get { return statusDescription; }
            internal set { statusDescription = value; }
        }

        public String Content
        {
            get { return content; }
            internal set { content = value; }
        }
    }
}
