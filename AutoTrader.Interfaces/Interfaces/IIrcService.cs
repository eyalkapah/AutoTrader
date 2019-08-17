using AutoTrader.Models.Entities;
using System.Threading.Tasks;

namespace AutoTrader.Interfaces.Interfaces
{
    public interface IIrcService
    {
        void Connect();

        void Disconnect();
    }
}