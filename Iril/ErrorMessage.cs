namespace Iril
{
    public class ErrorMessage
    {
        public string FilePath;
        public string Message;
        public string SurroundingFileContents;

        public override string ToString () =>
            string.IsNullOrEmpty (SurroundingFileContents) ?
                $"{FilePath}: {Message}" :
                $"{FilePath}: {Message}\n{SurroundingFileContents}";
    }
}