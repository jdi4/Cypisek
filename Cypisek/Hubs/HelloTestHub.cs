﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Cors;

namespace SignalRTest
{
    [EnableCors("AllowSpecificOrigin")]
    public class HelloTestHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message);
        }

        public void SendImage(string name, string imgurl)
        {
            Clients.All.setImage(name, imgurl);
        }
    }
}