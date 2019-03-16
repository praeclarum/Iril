using System;
namespace Iril
{
    public class Document
    {
        public readonly string Path;
        public readonly string Text;

        public Document (string path, string text)
        {
            Path = path;
            Text = text;
        }
    }
}
