using AutoTrader.Models.Entities;

namespace AutoTrader.Interfaces.Interfaces
{
    public interface IIrcService
    {
        IrcCommand ProcessIncommingMessage(string text);
    }
}