using CleanArchitecture.WebUI.Models;
using CleanArchitecture.WebUI.Models.DTOs;

namespace CleanArchitecture.WebUI.Services.Interfaces
{
	public interface IReviewService
	{
		Task<ResponseDTO?> GetAllReviewByVillaId(int villaId);
		Task<ResponseDTO?> CreateReview(Review review);
		Task<ResponseDTO?> UpdateReview(Review review);
		Task<ResponseDTO?> DeleteReview(int reviewId);
	}
}
