using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Nemo_v2_Data.Entities;
using Newtonsoft.Json;

namespace Nemo_v2_Api.Hubs
{
    public class TransferHub : Hub
    {
        private static List<string> usersss = new List<string>();

        public override Task OnConnectedAsync()
        {
            try
            {
                var user = JsonConvert.SerializeObject(new User());
                Clients.Caller.SendAsync("client", user);
                var a = Context.User;
                usersss.Add(Context.ConnectionId);
            }
            catch (Exception e)
            {
            }

            return base.OnConnectedAsync();
        }
    }
}