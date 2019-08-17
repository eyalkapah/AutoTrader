using AutoTrader.Models.Entities;
using AutoTrader.Models.Enums;
using AutoTrader.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Services.Helpers
{
    public class CommandBuilder
    {
        public static TradeCommand Build(string text)
        {
            var args = text.Split();

            if (args.Length < 2)
                throw new ArgumentException($"Not enough arguments for irc command {text}");

            var result = Enum.TryParse(args[0], out TradeCommandType commandType);

            if (!result)
                throw new UnknownIrcCommandException(args[0]);

            switch (commandType)
            {
                // PreDb channel bot release_name section_name
                case TradeCommandType.PreDb:
                case TradeCommandType.Pre:
                case TradeCommandType.New:
                    if (args.Length != 5)
                        throw new ArgumentException($"Invalid arguments number for PreDb command: {text}");

                    return new TradeCommand
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
    }
}