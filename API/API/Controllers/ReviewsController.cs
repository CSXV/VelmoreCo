using BusinessLayer;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/Reviews")]
public class ReviewsController : ControllerBase
{
    [HttpGet("AllProductReviews/{id}", Name = "GetReviewByProductID")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<AllReviewDTO>> GetReviewByProductID(int id)
    {
        if (id < 1)
        {
            return BadRequest($"invalid product id: {id}");
        }

        List<AllReviewDTO> ProductReviews = bl_Reviews.GetAllReviewsForProductID(id);

        // if (ProductReviews.Count == 0)
        // {
        //     return NotFound($"No review found.");
        // }

        return Ok(ProductReviews);
    }

    [HttpGet("{id}", Name = "GetReviewByID")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ReviewDTO> GetReivewByID(int id)
    {
        if (id < 1)
        {
            return BadRequest($"invalid id: {id}");
        }

        bl_Reviews review = bl_Reviews.Find(id);

        if (review == null)
        {
            return NotFound($"Review with id: {id} not found.");
        }

        ReviewDTO RDTO = review.RDTO;
        return Ok(RDTO);
    }

    [HttpPost("Add", Name = "AddNewReview")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<ReviewDTO> AddNewReview(ReviewDTO newReview)
    {
        if (
            newReview == null
            || newReview.ProductID < 0
            || newReview.UserID < 0
            || newReview.rating < 0
            || string.IsNullOrEmpty(newReview.comment)
        )
        {
            return BadRequest("Invalid review data");
        }

        bl_Reviews review = new bl_Reviews(
            new ReviewDTO(
                newReview.ID,
                newReview.ProductID,
                newReview.UserID,
                newReview.rating,
                newReview.comment,
                newReview.createDate
            )
        );

        review.Save();

        newReview.ID = review.ID;

        return CreatedAtRoute("GetUserByID", new { id = newReview.ID }, newReview);
    }

    [HttpPut("Update/{id}", Name = "UpdateReivew")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ReviewDTO> UpdateReview(int id, ReviewDTO updatedReview)
    {
        if (
            id < 1
            || updatedReview == null
            || updatedReview.ProductID < 0
            || updatedReview.UserID < 0
            || updatedReview.rating < 0
            || string.IsNullOrEmpty(updatedReview.comment)
        )
        {
            return BadRequest("Invalid review data");
        }

        bl_Reviews SearchReview = bl_Reviews.Find(id);

        if (SearchReview == null)
        {
            return NotFound($"Reivew with id: {id} not found");
        }

        SearchReview.UserID = updatedReview.UserID;
        SearchReview.ProductID = updatedReview.ProductID;
        SearchReview.rating = updatedReview.rating;
        SearchReview.comment = updatedReview.comment;

        SearchReview.Save();

        return Ok(SearchReview.RDTO);
    }

    [HttpDelete("Delete/{id}", Name = "DeleteReview")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult DeleteReview(int id)
    {
        if (id < 0)
        {
            return BadRequest("Invalid review id");
        }

        if (bl_Reviews.DeleteReview(id))
        {
            return Ok($"review with id: {id} has been removed successfully.");
        }
        else
        {
            return NotFound($"review with id: {id} not found");
        }
    }
}
