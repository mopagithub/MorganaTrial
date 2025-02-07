using System.Net;
using UmbracoBridge.Models.Requests;
using UmbracoBridge.Models.Retrieves;

namespace UmbracoBridge.Services
{
    public interface IHttpClientService
    {
        Task<T?> GetRequestAsync<T>(string url);
        Task<PostResponse?> SendRequestAsync<T>(string url, DocumentTypeRequest documentTypeRequest);
        Task<HttpStatusCode> DeleteRequestAsync<T>(string url);
    }
}
