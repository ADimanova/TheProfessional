namespace SignalRChat
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

            var chatUserId = this.Context.User.Identity.GetUserId();
            string groupName; 

            var group = this.data.ChatGroups.All()
                .FirstOrDefault(c => (c.FirstUserId == chatUserId || c.SecondUserId == chatUserId) &&
                (c.FirstUserId == toId || c.SecondUserId == toId));

            if (group != null)
            {
                groupName = group.Name;
            }
            else
            {
                var newGroup = new ChatGroup
                {
                    Name = string.Format("{0}/{1}", chatUserId, toId),
                    FirstUserId = chatUserId,
                    SecondUserId = toId
                };

                try
                {
                    this.data.ChatGroups.Add(newGroup);
                    this.data.SaveChanges();
                }
                catch
                {
                    throw new Exception("Something went wrong saving the new group");
                }

                groupName = newGroup.Name;
            }

            this.Groups.Add(Context.ConnectionId, groupName);
            Clients.Group(groupName).setSetting(DateTime.Now, group.FirstUserId);

            var messages = this.data.Messages.All()
                .Where(m => m.IsRead == false)
                .Where(m => m.FromUserId == toId)
                .ToList();

            var other = this.data.Users.GetById(toId);

            for (int i = 0; i < messages.Count; i++)
            {
                Clients.Group(groupName).addNewMessageToPage(other.FirstName, messages[i].Content, other.Id);

                messages[i].IsRead = true;
            }

            this.data.SaveChanges();
        }

        public void Send(string toId, string message)
        {
            var chatUserId = this.Context.User.Identity.GetUserId();
            var chatUser = this.data.Users.GetById(chatUserId);

            var group = this.data.ChatGroups.All()
                .FirstOrDefault(c => (c.FirstUserId == chatUserId || c.SecondUserId == chatUserId) &&
                (c.FirstUserId == toId || c.SecondUserId == toId));

            if (group == null)
            {
                new ArgumentNullException("This chat group is not registeres");
            }

            var groupName = group.Name;

            var newMessage = new Message
            {
                FromUserId = chatUserId,
                ToUserId = toId,
                Content = message,
                TimeSent = DateTime.Now
            };

            this.data.Messages.Add(newMessage);
            this.data.SaveChanges();

            Clients.Group(groupName).addNewMessageToPage(chatUser.FirstName, message, chatUserId);
        }
    }
}