using System;
using System.Threading.Tasks;

using core.Models;

namespace core.Interfaces
{
    public interface IRestApiOperations
    {
       public Task<RestApiResponse> ExecuteGet(RestApiRequest request);
       public Task<RestApiResponse> ExecutePost(RestApiRequest request);
    }
}
