using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.ApplicationCore.Interfaces.Services
{
    public interface IReviewService
    {
        Task<ResponseDTO> GetAllReviewByVillaId(int villaId); // QueryParameter query
        Task<ResponseDTO> CreateReview(Review review);
        Task<ResponseDTO> UpdateReview(Review review);
        Task<ResponseDTO> DeleteReview(int reviewId);
    }
}
