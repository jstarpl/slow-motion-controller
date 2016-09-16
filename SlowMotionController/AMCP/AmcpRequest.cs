using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlowMotionController.Amcp
{
    public class AmcpRequest
    {
        private AmcpClient client;

        private String command = "";
        private uint channel = 0;
        private uint layer = 0;
        private String[] parameters;

        public AmcpRequest(AmcpClient Client, String Command, uint Channel, uint Layer, params String[] Parameters)
        {
            this.client = Client;
            this.command = Command;
            this.channel = Channel;
            this.layer = Layer;
            this.parameters = Parameters;
        }

        public AmcpRequest(AmcpClient Client, String Command)
            : this(Client, Command, 0, 0)
        {

        }

        public AmcpRequest(AmcpClient Client, String Command, Caspar.Channel Channel, uint Layer, params String[] Parameters)
            : this(Client, Command, Channel.Id, Layer, Parameters)
        {
            
        }

        public AmcpRequest(AmcpClient Client, String Command, uint Channel, params String[] Parameters)
            : this(Client, Command, Channel, 0, Parameters)
        {

        }

        public AmcpRequest(AmcpClient Client, String Command, Caspar.Channel Channel, params String[] Parameters)
            : this(Client, Command, Channel.Id, 0, Parameters)
        {

        }

        public AmcpResponse GetResponse()
        {
            AmcpResponse result;
            lock (this.client)
            {
                this.client.SendRequest(this);
                result = this.client.GetResponse();
            }
            return result;
        }

        public void NoResponseRequest()
        {
            lock (this.client)
            {
                this.client.SendRequest(this);
            }
            return;
        }

        public override String ToString()
        {
            String result = command + (channel > 0 ? " " + channel : "") + (layer > 0 ? "-" + layer : "");
            foreach (String parameter in parameters)
            {
                result += " " + parameter;
            }
            return result;
        }
    }
}
