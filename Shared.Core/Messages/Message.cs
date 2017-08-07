using Shared.I18n.Utils;

namespace Shared.Core.Messages
{
    /// <summary>
    /// Class for supports the message displaying to user.
    /// </summary>
    public class Message
    {
        public string Text { get; set; }
        public MessageType MessageType { get; set; }

        private Message(string resourceKey, MessageType messageType)
        {
            this.MessageType = messageType;
            this.Text = ResourceUtils.GetString(resourceKey);
        }

        public static Message CreateErrorMessage(string resourceKey)
        {
            return new Message(resourceKey, MessageType.ERROR);
        }

        public static Message CreateSuccessMessage(string resourceKey)
        {
            return new Message(resourceKey, MessageType.SUCCESS);
        }
    }
}
