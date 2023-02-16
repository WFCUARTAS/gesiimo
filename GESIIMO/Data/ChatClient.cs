using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GESIIMO.Data
{
    public class ChatClient 
    {
        public const string HUBURL = "/ChatHub";
        private readonly NavigationManager _navigationManager;
        public HubConnection _hubConnection;
        private readonly string _username;
        private readonly string _grupo;
        private bool _started = false;

        public ChatClient(string username,string grupo, NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
            _username = username;
            _grupo = grupo;
        }

        public async Task StartAsync()
        {
            if (! _started)
            {
                _hubConnection = new HubConnectionBuilder()
                    .WithUrl(_navigationManager.ToAbsoluteUri(HUBURL))
                    .Build();

                _hubConnection.On<string, string, string>(Messages.RECEIVE, (user, parametro, message) =>
                 {
                     HandleReceiveMessage(user, parametro, message);
                 });

                await _hubConnection.StartAsync();
                _started = true;

                await _hubConnection.InvokeAsync("AddToGroup", _grupo);
                await _hubConnection.SendAsync(Messages.REGISTER, _username,_grupo);
                
            }
        }

        public void HandleReceiveMessage(string username, string parametro, string message)
        {
            MessageReceived?.Invoke(this, new MessageReceivedEventArgs(username, parametro, message));
        }

        public event MessageReceivedEventHandler MessageReceived;
        public delegate void MessageReceivedEventHandler(object sender, MessageReceivedEventArgs e);

        public async Task SendAsync(string parametro, string message)
        {
            if (!_started)
            {
                throw new InvalidOperationException("cliente not started");
            }
            await _hubConnection.SendAsync(Messages.SEND, _username, parametro, message, _grupo,false);
        }

        public async Task SendExceptUserAsync(string parametro, string message)
        {
            if (!_started)
            {
                throw new InvalidOperationException("cliente not started");
            }
            await _hubConnection.SendAsync(Messages.SEND, _username, parametro, message, _grupo, true);
        }

        public async Task StopAsync()
        {
            if (_started)
            {
                await _hubConnection.StopAsync();
                await _hubConnection.DisposeAsync();
                _hubConnection = null;
                _started = false;  
            }
        }

        public async ValueTask DisposeAsync()
        {
            await StopAsync();
        }

    }

    public class MessageReceivedEventArgs : EventArgs
    {
        public string Username { get; set; }

        public string Parametro { get; set; }
        public string Message { get; set; }
        public MessageReceivedEventArgs(string username, string parametro, string message)
        {
            Username = username;
            Parametro = parametro;
            Message = message;
        }
    }



}
