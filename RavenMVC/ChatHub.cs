﻿using System;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace RavenMVC
{
    public class ChatHub : Hub
    {
        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message);
        }
    }
}