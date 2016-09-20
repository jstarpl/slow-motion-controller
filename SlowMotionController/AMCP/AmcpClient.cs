using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace SlowMotionController.Amcp
{
    public class AmcpClient
    {
        private TcpClient client;
        private TextWriter streamWriter;
        private TextReader streamReader;

        public AmcpClient(IPEndPoint Server)
        {
            client = new TcpClient();
            try
            {
                client.Connect(Server);
                streamWriter = new StreamWriter(client.GetStream());
                streamReader = new StreamReader(client.GetStream());
            }
            catch (SocketException se)
            {
                client = null;
                throw se;
            }
        }

        public EndPoint ServerEndPoint
        {
            get { return client.Client.RemoteEndPoint; }
        }

        public void Disconnect()
        {
            if (client != null)
                client.Close();
        }

        ~AmcpClient()
        {
            this.Disconnect();
        }

        public AmcpResponse GetResponse()
        {
            try
            {
                AmcpResponse response;

                String firstLine = streamReader.ReadLine();
                int resultCode = int.Parse(firstLine.Substring(0, 3));
                String status = firstLine.Substring(4);
                response = new AmcpResponse(resultCode, status);
                String content = "";
                if ((resultCode == 101) || ((resultCode == 201 && (status != "INFO OK"))))
                {
                    content = streamReader.ReadLine();
                    response.Content = content;
                }
                else if ((resultCode == 200) || ((resultCode == 201) && (status == "INFO OK")))
                {
                    String line = "";
                    do
                    {
                        line = streamReader.ReadLine();
                        if (line != "")
                            content += line + "\r\n";
                    }
                    while (line != "");
                    response.Content = content;
                }

                return response;
            }
            catch (Exception e)
            {
                return new AmcpResponse(500, "CONNECTION ERROR");
            }
        }

        public void SendRequest(AmcpRequest Request)
        {
            SendCommand(Request.ToString());
        }

        private void SendCommand(String Command)
        {
            try
            {
                streamWriter.WriteLine(Command);
                streamWriter.Flush();
            }
            catch (IOException ioe)
            {
                Console.Error.WriteLine(ioe.ToString());
            }
        }
    }
}
