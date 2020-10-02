namespace Lk.Test.Task3
{
    using Lk.Test.Task3.ContentProcessors;

    public class ContentProcessorFactory
    {
        public virtual IContentProcessor CreateContentProcessor(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return new TextContentProcessor();
            }

            if (content.IndexOf("<html") != -1)
            {
                return new HtmlContentProcessor();
            }

            return new TextContentProcessor();
        }
    }
}
