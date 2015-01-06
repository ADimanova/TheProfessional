﻿namespace SignalRChat
{
    using System;
    using System.Linq;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.SignalR;
    using Professional.Data;
    using Professional.Models;

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
            if (toId == null)
            {
                throw new ArgumentNullException("The other participant in the chat is not set");
            }

            var chatUser = this.Context.User.Identity.GetUserId();
            var groupName = string.Format("{0}/{1}", chatUser, toId);
            this.Groups.Add(Context.ConnectionId, groupName);

            var messages = this.data.Messages.All()
                .Where(m => m.IsRead == false)
                .Where(m => m.FromUserId == toId)
                .ToList();

            var other = this.data.Users.GetById(toId);

            for (int i = 0; i < messages.Count; i++)
            {
                Clients.Group(groupName).addNewMessageToPage(other.FirstName, messages[i].Content);

                messages[i].IsRead = true;
            }

            this.data.SaveChanges();
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