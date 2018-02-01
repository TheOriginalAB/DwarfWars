using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DwarfWars.Library;

namespace DwarfWars.Server
{
    class Log
    {
        public List<string> log;

        public Log()
        {
            log = new List<string>();
        }

        public void AddToLog(ICommand command)
        {
            switch (command.CommandType)
            {
                case CommandType.Connect:
                    ConnectCommand<ServerPlayer> connectCommand = (ConnectCommand<ServerPlayer>)command;

                    break;
                case CommandType.Goodbye:
                    break;
                case CommandType.Disconnect:
                    break;
                case CommandType.Movement:
                    break;
                case CommandType.Location:
                    break;
                case CommandType.Interact:
                    break;
                default:
                    break;
            }
        }
    }
}
