using CleanArchitecture.WebUI.Utilities;
using System.Security.AccessControl;
using static CleanArchitecture.WebUI.Utilities.Constants;

namespace CleanArchitecture.WebUI.Models.DTOs
{
    public class RequestDTO
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }
        public ContentType ContentType { get; set; } = ContentType.Json;
    }
}
