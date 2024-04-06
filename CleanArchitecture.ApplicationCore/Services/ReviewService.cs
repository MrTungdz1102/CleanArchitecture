using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Entities;
using CleanArchitecture.ApplicationCore.Interfaces.Repositories;
using CleanArchitecture.ApplicationCore.Interfaces.Services;
using CleanArchitecture.ApplicationCore.Specifications;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.ApplicationCore.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private ResponseDTO _response;
        public ReviewService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _response = new ResponseDTO();
        }
        public async Task<ResponseDTO> CreateReview(Review review)
        {
            try
            {
                _response.Result = await _unitOfWork.reviewRepo.AddAsync(review);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> DeleteReview(int reviewId)
        {
            try
            {
                Review? deleteReview= await _unitOfWork.reviewRepo.GetByIdAsync(reviewId);
                if (deleteReview is null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Cannot find out the review";
                }
                else
                {
                    await _unitOfWork.reviewRepo.DeleteAsync(deleteReview);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> GetAllReviewByVillaId(int villaId)
        {
            try
            {
                ReviewSpecification specification = new ReviewSpecification(villaId);
                _response.Result = await _unitOfWork.reviewRepo.ListAsync(specification);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> UpdateReview(Review review)
        {
            try
            {
                await _unitOfWork.reviewRepo.UpdateAsync(review);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
