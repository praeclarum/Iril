using System;
namespace Iril
{
    public class Message
    {
        readonly Exception exception;

        public string FilePath { get; set; }
        public MessageType Type { get; }
        public string Text { get; }
        public string Surrounding { get; set; }
        public Exception Exception => exception;

        public Message (string message, Exception exception = null)
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
