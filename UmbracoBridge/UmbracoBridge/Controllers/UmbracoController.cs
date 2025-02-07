using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UmbracoBridge.Models.Requests;
using UmbracoBridge.Models.Retrieves;
using UmbracoBridge.Services;

namespace UmbracoBridge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UmbracoController : ControllerBase
    {
        private readonly IHttpClientService _httpClientService;
        public UmbracoController(HttpClient httpClient, IConfiguration configuration, IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        [HttpGet("Healthcheck")]
        public async Task<IActionResult> GetUmbracoHealthCheckGroup()
        {
            string url = "/umbraco/management/api/v1/health-check-group";

            var apiUserResponse = await _httpClientService.GetRequestAsync<HealthCheckResponse>(url);

            if (apiUserResponse == null)
            {
                return BadRequest("Error, check please");
            }
            return Ok(apiUserResponse);
        }



        [HttpPost("DocumentType")]
        public async Task<IActionResult> InsertDocumentType([FromBody] DocumentTypeRequest documentTypeRequest)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
                        
            string url = "/umbraco/management/api/v1/document-type";

            PostResponse? apiUserResponse = await _httpClientService.SendRequestAsync<DocumentTypeRequest>(url, documentTypeRequest);

            if (apiUserResponse == null)
            {
                return BadRequest("Error, check please");
            }

            return Ok(apiUserResponse);
        }


        [HttpDelete("DocumentType/{id}")]
        public async Task<IActionResult> DeleteDocumentType(Guid id)
        {
            string url = $"/umbraco/management/api/v1/document-type/{id}";
            var apiUserResponse = await _httpClientService.DeleteRequestAsync<object>(url);
            if (apiUserResponse.ToString() != "OK")
            {
                return BadRequest("Error, check please");
            }
            return Ok("Deleted");
        }
    }
}
