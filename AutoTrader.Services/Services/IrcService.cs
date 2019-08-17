using AutoTrader.Interfaces.Interfaces;
using AutoTrader.Models.Entities;
using AutoTrader.Models.Enums;
using AutoTrader.Models.Exceptions;
using AutoTrader.Services.Helpers;
using NamedPipeWrapper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Services.Services
{
    public class IrcService : IIrcService
    {
        private readonly ISet<string> _clients = new HashSet<string>();

        private readonly IRaceService _raceService;
        private readonly NamedPipeServer<string> _server = new NamedPipeServer<string>("mircpipe");
        private ConcurrentBag<string> _messages;

        public IrcService(IRaceService raceService)
        {
            _messages = new ConcurrentBag<string>();
            _raceService = raceService;
        }

        public void Connect()
        {
            try
            {
                _server.ClientConnected += OnClientConnected;
                _server.ClientDisconnected += OnClientDisconnected;
                _server.ClientMessage += OnMessageRecieved;
                _server.Start();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Disconnect()
        {
            try
            {
                _server.Stop();
                _server.ClientConnected -= OnClientConnected;
                _server.ClientDisconnected -= OnClientDisconnected;
                _server.ClientMessage -= OnMessageRecieved;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void OnClientConnected(NamedPipeConnection<string, string> connection)
        {
            _clients.Add(connection.Name);
            //AddLine("<b>" + connection.Name + "</b> connected!");
            //UpdateClientList();
            connection.PushMessage("Welcome!  You are now connected to the server.");
        }

        private void OnClientDisconnected(NamedPipeConnection<string, string> connection)
        {
            _clients.Remove(connection.Name);
            //AddLine("<b>" + connection.Name + "</b> disconnected!");
            //UpdateClientList();
        }

        private void OnMessageRecieved(NamedPipeConnection<string, string> connection, string message)
        {
            Task.Run(async () =>
            {
                _messages.Add(message);

                await ProcessIncommingMessageAsync(message);
            });
        }

        private async Task ProcessIncommingMessageAsync(string text)
        {
            try
            {
                var command = CommandBuilder.Build(text);

                await _raceService.RaceAsync(command);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}