﻿using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Entities;
using CleanArchitecture.ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost("CreateReview")]
        public async Task<ActionResult<ResponseDTO>> CreateReview([FromBody] Review review)
        {
            return Ok(await _reviewService.CreateReview(review));
        }

        [HttpPut("UpdateReview")]
        public async Task<ActionResult<ResponseDTO>> UpdateReview([FromBody] Review review)
        {
            return Ok(await _reviewService.UpdateReview(review));
        }

        [HttpDelete("DeleteReview/{reviewId:int}")]
        public async Task<ActionResult<ResponseDTO>> DeleteReview([FromRoute] int reviewId)
        {
            return Ok(await _reviewService.DeleteReview(reviewId));
        }

        [HttpGet("GetAllReviewByHotelId/{hotelId:int}")]
        public async Task<ActionResult<ResponseDTO>> GetAllReviewByHotelId([FromRoute] int hotelId)
        {
            return Ok(await _reviewService.GetAllReviewByHotelId(hotelId));
        }
    }
}
