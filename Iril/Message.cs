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

        public Message (MessageType type, string message)
        {
            Type = type;
            Text = message ?? throw new ArgumentNullException (nameof (message));
        }

        public override string ToString ()
        {
            var m = $"{Type}: {FilePath}: {Text}";
            if (!string.IsNullOrEmpty(Surrounding)) {
                m += "\n" + Surrounding;
            }
#if DEBUG
            if (Exception != null) {
                m += "\n" + Exception;
            }
#endif
            return m;
        }
    }

    public enum MessageType
    {
        Error,
        Warning
    }
}
