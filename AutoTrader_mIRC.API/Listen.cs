using NamedPipeWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace mIrcComDll
{
    [Guid("AD53A3E8-E51A-49C7-944E-E72A2064F938"), ClassInterface(ClassInterfaceType.None), ComSourceInterfaces(typeof(IListenerEvents))]
    public class Listen : IComListen
    {
        private readonly NamedPipeClient<string> _client = new NamedPipeClient<string>("mircpipe");

        public void Connect()
        {
            _client.ServerMessage += OnServerMessage;
            _client.Disconnected += OnDisconnected;
            _client.Start();
        }

        public string Disconnect()
        {
            _client.Stop();
            _client.ServerMessage -= OnServerMessage;
            _client.Disconnected -= OnDisconnected;

            return "Client disconnected";
        }

        public void Publish(string text)
        {
            _client.PushMessage(text);
        }

        public string Version()
        {
            return "1.001";
        }

        private void OnDisconnected(NamedPipeConnection<string, string> connection)
        {
        }

        private void OnServerMessage(NamedPipeConnection<string, string> connection, string message)
        {
        }
    }
}