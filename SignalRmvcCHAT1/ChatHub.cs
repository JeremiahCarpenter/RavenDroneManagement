using System;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SignalRmvcCHAT1
{
    public class ChatHub : Hub
    {
        //This class  adds a  set of script files and assemly references that support Signal R to the Project Raven
        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message);
        }
    }
}