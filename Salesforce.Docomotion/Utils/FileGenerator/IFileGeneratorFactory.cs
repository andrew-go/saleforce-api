namespace Salesforce.Docomotion.Utils.FileGenerator
{
    public interface IFileGeneratorFactory
    {
        IFileGenerator GetFileGenerator(Format format);
    }
}
