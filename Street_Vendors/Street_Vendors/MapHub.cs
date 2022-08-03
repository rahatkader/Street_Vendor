using Microsoft.AspNet.SignalR;
using Street_Vendors.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;


namespace Street_Vendors
{
    public class MapHub : Hub
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        public override System.Threading.Tasks.Task OnConnected()
        {
            Clients.All.user(Context.User.Identity.Name);
            return base.OnConnected();
        }
        public void send(string location)
        {
            
            var sellers = _context.Users.ToList();
            string cid;
            foreach (var seller in sellers)
            {
                if (Context.User.Identity.Name.Equals(seller.UserName))
                {
                    cid = "," + seller.Id;
                    Clients.Caller.message("own," + location + cid);
                    Clients.Others.message(Context.User.Identity.Name + "," + location + cid);
                }
            }     
        }
    }
}
