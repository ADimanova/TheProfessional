using System;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Professional.Data;
using Professional.Models;
using Microsoft.AspNet.Identity;

namespace SignalRChat
{
    public class ChatHub : Hub
    {
        private IApplicationData data;

        public ChatHub()
        {
            this.data = new ApplicationData(new ApplicationDbContext());
            this.data.Context.DbContext.Configuration.ProxyCreationEnabled = false;
        }

        public void StartConversation(string toId)
        {
            var chatUser = this.Context.User.Identity.GetUserId();
            var groupName = string.Format("{0}/{1}", chatUser, toId);

            var messages = this.data.Messages.All()
                .Where(m => m.IsRead == false)
                .Where(m => m.FromUserId == toId)
                //.Select(m => m.Content)
                .ToList();

            foreach (var message in messages)
            {
                //Clients.Group(groupName).addNewMessageToPage("Other", message);
                Clients.All.addNewMessageToPage("Other", message.Content);
                Clients.All.log(message);
            }
            //for (int i = 0; i < messages.Count; i++)
            //{
            //    Clients.Group(groupName).addNewMessageToPage("Other", messages[i]);
            //    messages[i].IsRead = true;
            //}
            //this.data.SaveChanges();

            this.Groups.Add(Context.ConnectionId, groupName);
        }
        public void Send(string toId, string message)
        {
            var chatUser = this.Context.User.Identity.GetUserId();

            var groupName = string.Format("{0}/{1}", chatUser, toId);

            var newMessage = new Message
            {
                FromUserId = chatUser,
                ToUserId = toId,
                Content = message,
                TimeSent = DateTime.Now.ToUniversalTime()
            };

            this.data.Messages.Add(newMessage);
            this.data.SaveChanges();

            Clients.Group(groupName).addNewMessageToPage("Me", message);
        }
    }
}