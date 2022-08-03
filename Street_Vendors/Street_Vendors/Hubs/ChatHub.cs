using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Street_Vendors.Hubs
{
    public class ChatHub : Hub
    {
        public override System.Threading.Tasks.Task OnConnected()
        {
            Clients.All.user(Context.User.Identity.Name);
            return base.OnConnected();
        }

        public void send(string message)
        {
            Clients.Caller.message("You~You: " + message);
            Clients.Others.message("others~" + Context.User.Identity.Name + ": " + message);
        }

    }
}