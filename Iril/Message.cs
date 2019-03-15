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
            Text = message ?? throw new ArgumentNullException (nameof (message));
            this.exception = exception;
        }

        public override string ToString ()
        {
#if DEBUG
            return $"{Type}: {Text}\n{Exception}";
#else
            return $"{Type}: {Text}";
#endif
        }
    }

    public enum MessageType
    {
        Error
    }
}
