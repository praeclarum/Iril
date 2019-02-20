using System;
namespace Repil
{
    public class Message
    {
        readonly Exception exception;

        public MessageType Type { get; }
        public string Text { get; }
        public Exception Exception => exception;

        public Message (string message, Exception exception)
        {
            Type = MessageType.Error;
            Text = message;
            this.exception = exception;
        }

        public override string ToString ()
        {
            return $"{Type}: {Text}";
        }
    }

    public enum MessageType
    {
        Error
    }
}
