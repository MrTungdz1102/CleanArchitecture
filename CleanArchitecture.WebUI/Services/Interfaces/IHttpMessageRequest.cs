using CleanArchitecture.WebUI.Models.DTOs;

namespace CleanArchitecture.WebUI.Services.Interfaces
{
    public interface IHttpMessageRequest
    {
        HttpRequestMessage Build(RequestDTO requestDTO);
    }
}
