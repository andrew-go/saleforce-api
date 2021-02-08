using System;
using System.Threading.Tasks;
using Refit;

namespace Salesforce.Docomotion.Api
{
    public interface ISalesForceApi
    {
        [Get("/data/v50.0/query")]
        [QueryUriFormat(UriFormat.Unescaped)]
        Task<QueryResult<T>> GetByQueryAsync<T>(string q);

        [Get("/data/v50.0/sobjects/{**objectName}/{id}")]
        Task<T> GetById<T>(string objectName, string id);

        [Patch("/data/v50.0/sobjects/{**objectName}/{id}")]
        Task UpdateById<T>(string objectName, string id, T body);
    }
}
