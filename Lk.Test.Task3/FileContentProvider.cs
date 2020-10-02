namespace Lk.Test.Task3
{
    using System.IO;

    public class FileContentProvider
    {
        public virtual string GetFileContent(string fileName)
        {
            using (var fileStream = File.OpenRead(fileName))
            {
                return new StreamReader(fileStream).ReadToEnd();
            }
        }
    }
}
