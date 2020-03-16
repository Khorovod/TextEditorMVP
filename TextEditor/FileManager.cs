using System.IO;
using System.Text;


namespace TextEditor.BL
{
    public interface IFileManager
    {
        string GetContent(string filePath);
        string GetContent(string filePath, Encoding encoding);
        void SaveContent(string filePath, string content);
        void SaveContent(string filePath, string content, Encoding encoding);
        bool IsExist(string filePath);
        int GetSymbolsCount(string content);
    }
    public class FileManager : IFileManager
    {
        private readonly Encoding _defaultEncoding = Encoding.GetEncoding(1251);
        //открывать текстовый файл
        public string GetContent(string filePath, Encoding encoding)
        {
            return File.ReadAllText(filePath, encoding);
        }

        public string GetContent(string filePath)
        {
            return GetContent(filePath, _defaultEncoding);
        }
        //схоронить файл
        public void SaveContent(string filePath, string content, Encoding encoding)
        {
            File.WriteAllText(filePath, content, encoding);
        }

        public void SaveContent(string filePath, string content)
        {
            SaveContent(filePath, content, _defaultEncoding);
        }
        //есть ли файл
        public bool IsExist(string filePath)
        {
            return File.Exists(filePath);
        }

        public int GetSymbolsCount(string content)
        {
            return content.Length;
        }
    }
}
