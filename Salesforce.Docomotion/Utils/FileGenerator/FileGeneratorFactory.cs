using System;
using Microsoft.Extensions.Logging;

namespace Salesforce.Docomotion.Utils.FileGenerator
{
    public class FileGeneratorFactory : IFileGeneratorFactory
    {
        private readonly ILogger<FileGeneratorFactory> _logger;

        public FileGeneratorFactory(ILogger<FileGeneratorFactory> logger)
        {
            _logger = logger;
        }

        public IFileGenerator GetFileGenerator(Format format)
        {
            return format switch
            {
                Format.Json => new JsonFileGenerator(_logger),
                Format.Xml => new XmlFileGenerator(_logger),
                _ => throw new NotSupportedException()
            };
        }
    }
}
