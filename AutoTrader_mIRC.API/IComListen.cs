using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace mIrcComDll
{
    [Guid("CAA4976A-45C3-4BC5-BC0B-E474F4C3C83F"), InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IComListen
    {
        [DispId(3)]
        void Connect();

        [DispId(4)]
        string Disconnect();

        [DispId(2)]
        void Publish(string text);

        [DispId(1)]
        string Version();
    }
}