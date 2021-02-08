using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using Microsoft.Extensions.Logging;

namespace Salesforce.Docomotion.Utils.FileGenerator
{
    public class XmlFileGenerator : IFileGenerator
    {
        private const string FileFormat = ".xml";
        private const string ContentType = "application/xml";

        private readonly ILogger<FileGeneratorFactory> _logger;

        public XmlFileGenerator(ILogger<FileGeneratorFactory> logger)
        {
            _logger = logger;
        }

        public File GenerateFromObjects<T>(IEnumerable<T> objects)
        {
            var fileName = $"{typeof(T).Name}s{FileFormat}";
            _logger.LogDebug($"Starting generating {fileName} file");

            Stopwatch sw = new Stopwatch();
            sw.Start();
            var document = new XDocument(ConvertObjectToXElement<T>(objects));
            sw.Stop();
            _logger.LogDebug($"{fileName} file has been generated in {sw.Elapsed}");

            var data = Encoding.UTF8.GetBytes(document.ToString());

            return new File(fileName, data, ContentType);
        }

        private static XElement ConvertObjectToXElement<T>(object o)
        {
            if (o is IEnumerable list)
            {
                return ConvertListToXElement<T>(list);
            }

            var rootElement = new XElement(o.GetType().Name);
            var propertyInfos = o.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var propertyInfo in propertyInfos)
            {
                var value = propertyInfo.GetValue(o);
                if (value == null)
                {
                    continue;
                }

                if (propertyInfo.PropertyType.Assembly == o.GetType().Assembly)
                {
                    rootElement.Add(ConvertObjectToXElement<T>(value));
                    continue;
                }

                rootElement.Add(new XElement(propertyInfo.Name, value));
            }

            return rootElement;
        }

        private static XElement ConvertListToXElement<T>(IEnumerable list)
        {
            var rootElement = new XElement($"{typeof(T).Name}s");

            foreach (var o in list)
            {
                if (o == null)
                {
                    continue;
                }

                rootElement.Add(ConvertObjectToXElement<T>(o));
            }

            return rootElement;
        }
    }
}
