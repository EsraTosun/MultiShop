using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.Comment.Context;

namespace MultiShop.Comment.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentStatisticsController : ControllerBase
    {
        private readonly CommentContext _commentContext;
        public CommentStatisticsController(CommentContext commentContext)
        {
            _commentContext = commentContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetCommentCount()
        {
            int values = await _commentContext.UserComments.CountAsync();
            return Ok(values);
        }

        [HttpGet("GetCommentCountByProductId/{productId}")]
        public async Task<IActionResult> GetCommentCountByProductId(string productId)
        {
            int values = await _commentContext.UserComments
                .Where(x => x.ProductId == productId)
                .CountAsync();

            return Ok(values);
        }

        [HttpGet("GetAverageRatingByProductId/{productId}")]
        public IActionResult GetAverageRatingByProductId(string productId)
        {
            var ratings = _commentContext.UserComments
                .Where(x => x.ProductId == productId && x.Rating > 0)
                .Select(x => x.Rating)
                .ToList();

            if (!ratings.Any())
                return Ok(0);

            var average = ratings.Average();
            return Ok(Math.Round(average, 1));
        }
    }
}
