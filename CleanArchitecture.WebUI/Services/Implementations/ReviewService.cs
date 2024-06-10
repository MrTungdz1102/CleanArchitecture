using CleanArchitecture.WebUI.Models;
using CleanArchitecture.WebUI.Models.DTOs;
using CleanArchitecture.WebUI.Services.Interfaces;
using CleanArchitecture.WebUI.Utilities;

namespace CleanArchitecture.WebUI.Services.Implementations
{
    public class ReviewService : IReviewService
    {
        private readonly IBaseService _baseService;

        public ReviewService(IBaseService baseService)
        {
            _baseService = baseService;

        }
        public async Task<ResponseDTO?> CreateReview(Review review)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                Data = review,
                ApiType = Utilities.Constants.ApiType.POST,
                Url = Constants.APIUrlBase + "/api/ReviewAPI/CreateReview"
            });
        }

        public async Task<ResponseDTO?> DeleteReview(int reviewId)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                Url = Constants.APIUrlBase + "/api/ReviewAPI/DeleteReview/" + reviewId,
                ApiType = Constants.ApiType.DELETE,
            });
        }

        public async Task<ResponseDTO?> GetAllReviewByVillaId(int villaId)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                Url = Constants.APIUrlBase + "/api/ReviewAPI/GetAllReviewByVillaId/" + villaId,
                ApiType = Constants.ApiType.GET
            });
        }

        public async Task<ResponseDTO?> GetReview(int reviewId)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                Url = Constants.APIUrlBase + "/api/ReviewAPI/GetReview/" + reviewId,
                ApiType = Constants.ApiType.GET,
            });
        }

        public async Task<ResponseDTO?> UpdateReview(Review review)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                Url = Constants.APIUrlBase + "/api/ReviewAPI/UpdateReview",
                ApiType = Constants.ApiType.PUT,
                Data = review
            });
        }
    }
}
