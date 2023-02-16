using GESIIMO.Data;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GESIIMO.Services
{
    public class ChatHub : Hub
    {
        private static readonly Dictionary<string, string> userlookup = new Dictionary<string, string>();

        public async Task SendMessage(string username, string parametro, string message,string grupo, bool ExceptUser)
        {
            Console.WriteLine("send");
            string us= "";
            List<string> user = new List<string>();
            foreach (KeyValuePair<string, string> item in userlookup)
            {
                us = us +"key:"+item.Key+"|user:"+ item.Value+",";
                if(item.Key != Context.ConnectionId) user.Add(item.Key);
            }
            if (parametro == "listar")
            {
                await Clients.Client(Context.ConnectionId).SendAsync(Messages.RECEIVE, username, parametro, us);
            }
            else if (ExceptUser)
            {
                await Clients.AllExcept(Context.ConnectionId).SendAsync(Messages.RECEIVE, username, parametro, message);
            }
            else
            {
                await Clients.Group(grupo).SendAsync(Messages.RECEIVE, username, parametro, message);
            }
            
        }


        public async Task Register(string username,string _group)
        {
            var currentId = Context.ConnectionId;
            if (userlookup.ContainsValue(username))
            {
                string key = userlookup.FirstOrDefault(x => x.Value == username).Key;
                await Groups.RemoveFromGroupAsync(key, _group);
                userlookup.Remove(key);
                await Clients.AllExcept(currentId).SendAsync(Messages.RECEIVE, username, "InicioSesion", username);
            }
            else
            {
                await Clients.Client(currentId).SendAsync(Messages.RECEIVE, username, "nuevo", "");
            }
            userlookup.Add(currentId, username);
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine("Conected");
            return base.OnConnectedAsync();
        }

        public async Task AddToGroup(string grupo)
        {
            string id = Context.ConnectionId;
            await Groups.AddToGroupAsync(id, grupo);
        }

        public override async Task OnDisconnectedAsync(Exception e)
        {
            string parametro = "";
            Console.WriteLine($"Disconnect {e?.Message} {Context.ConnectionId}");
            string id = Context.ConnectionId;
            if(!userlookup.TryGetValue(id, out string username))
            {
                username = "[unknown]";
            }
            userlookup.Remove(id);
            //await Clients.AllExcept(Context.ConnectionId).SendAsync(Messages.RECEIVE,username, parametro, $"{username} has left the chat");
            await base.OnDisconnectedAsync(e);
        }

    }
}
