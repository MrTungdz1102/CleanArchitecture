using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Entities;

namespace CleanArchitecture.ApplicationCore.Interfaces.Services
{
    public interface IReviewService
    {
        Task<ResponseDTO> GetAllReviewByVillaId(int villaId); // QueryParameter query
        Task<ResponseDTO> CreateReview(Review review);
        Task<ResponseDTO> UpdateReview(Review review);
        Task<ResponseDTO> DeleteReview(int reviewId);
        Task<ResponseDTO> GetReview(int reviewId);
    }
}
