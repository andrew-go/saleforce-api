using System.Collections.Generic;

namespace Salesforce.Docomotion.Utils.FileGenerator
{
    public interface IFileGenerator
    {
        File GenerateFromObjects<T>(IEnumerable<T> objects);
    }
}
