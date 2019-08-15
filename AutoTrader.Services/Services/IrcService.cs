using AutoTrader.Interfaces.Interfaces;
using AutoTrader.Models.Entities;
using AutoTrader.Models.Enums;
using AutoTrader.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Services.Services
{
    public class IrcService : IIrcService
    {
        public IrcCommand ProcessIncommingMessage(string text)
        {
            try
            {
                var args = text.Split();

                if (args.Length < 2)
                    throw new ArgumentException($"Not enough arguments for irc command {text}");

                var result = Enum.TryParse(args[0], out IrcCommandType commandType);

                if (!result)
                    throw new UnknownIrcCommandException(args[0]);

                switch (commandType)
                {
                    // PreDb channel bot release_name section_name
                    case IrcCommandType.PreDb:
                    case IrcCommandType.Pre:
                    case IrcCommandType.New:
                        if (args.Length != 5)
                            throw new ArgumentException($"Invalid arguments number for PreDb command: {text}");

                        return new IrcCommand
                        {
                            CommandType = commandType,
                            Channel = args[1],
                            Bot = args[2],
                            ReleaseName = args[3],
                            SectionName = args[4]
                        };

                    default:
                        throw new NotImplementedException($"irc command not implemented: {commandType}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                return null;
            }
        }
    }
}