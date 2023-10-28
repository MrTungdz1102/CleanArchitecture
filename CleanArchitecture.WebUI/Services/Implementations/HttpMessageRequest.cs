using CleanArchitecture.WebUI.Models.DTOs;
using CleanArchitecture.WebUI.Services.Interfaces;
using Newtonsoft.Json;
using System.Text;
using static CleanArchitecture.WebUI.Utilities.Constants;

namespace CleanArchitecture.WebUI.Services.Implementations
{
    public class HttpMessageRequest : IHttpMessageRequest
    {
        public HttpRequestMessage Build(RequestDTO requestDTO)
        {
            HttpRequestMessage message = new HttpRequestMessage();
            if (requestDTO.ContentType == ContentType.MultipartFormData)
            {
                message.Headers.Add("Accept", "*/*");
            }
            else
            {
                message.Headers.Add("Accept", "application/json");
            }
            message.RequestUri = new Uri(requestDTO.Url);
            if (requestDTO.ContentType == ContentType.MultipartFormData)
            {
                var content = new MultipartFormDataContent();
                foreach (var item in requestDTO.Data.GetType().GetProperties())
                {
                    var value = item.GetValue(requestDTO.Data);
                    // chi upload file neu file co kieu iformfile
                    if (value is FormFile)
                    {
                        var file = (FormFile)value;
                        if (file is not null)
                        {
                            content.Add(new StreamContent(file.OpenReadStream()), item.Name, file.FileName);
                        }
                    }
                    else
                    {
                        content.Add(new StringContent(value == null ? "" : value.ToString()), item.Name);
                    }
                }
                message.Content = content;
            }
            else if (requestDTO.Data != null)
            {
                message.Content = new StringContent(JsonConvert.SerializeObject(requestDTO.Data), Encoding.UTF8, "application/json");
            }

            HttpResponseMessage? responseMessage = null;
            switch (requestDTO.ApiType)
            {
                case ApiType.POST:
                    message.Method = HttpMethod.Post;
                    break;
                case ApiType.DELETE:
                    message.Method = HttpMethod.Delete;
                    break;
                case ApiType.PUT:
                    message.Method = HttpMethod.Put;
                    break;
                default:
                    message.Method = HttpMethod.Get;
                    break;
            }
            return message;
        }
    }
}
