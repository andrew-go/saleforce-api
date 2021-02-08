using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Salesforce.Docomotion.Utils.FileGenerator
{
    public class JsonFileGenerator : IFileGenerator
    {
        private const string FileFormat = ".json";
        private const string ContentType = "application/json";

        private readonly ILogger<FileGeneratorFactory> _logger;

        public JsonFileGenerator(ILogger<FileGeneratorFactory> logger)
        {
            _logger = logger;
        }

        public File GenerateFromObjects<T>(IEnumerable<T> objects)
        {
            var fileName = $"{typeof(T).Name + "s"}{FileFormat}";
            _logger.LogDebug($"Starting generating {fileName} file");

            Stopwatch sw = new Stopwatch();
            sw.Start();
            string json = JsonConvert.SerializeObject(objects, Formatting.Indented);
            sw.Stop();
            _logger.LogDebug($"{fileName} file has been generated in {sw.Elapsed}");

            var data = System.Text.Encoding.UTF8.GetBytes(json);

            return new File(fileName, data, ContentType);
        }
    }
}
