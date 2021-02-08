using System.Collections.Generic;

namespace Salesforce.Docomotion.Api
{
    public class QueryResult<T>
    {
        public int TotalSize { get; set; }

        public List<T> Records { get; set; } = new List<T>();
    }
}
