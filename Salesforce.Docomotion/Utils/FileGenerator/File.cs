namespace Salesforce.Docomotion.Utils.FileGenerator
{
    public class File
    {
        public string Name { get; set; }

        public byte[] Data { get; set; }

        public string ContentType { get; set; }

        public File(string name, byte[] data, string contentType)
        {
            Name = name;
            Data = data;
            ContentType = contentType;
        }
    }
}
