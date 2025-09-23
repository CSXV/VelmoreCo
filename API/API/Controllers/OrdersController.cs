using BusinessLayer;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/Orders")]
public class OrdersController : ControllerBase
{
    [HttpGet("AllProductOrders/{id}", Name = "GetAllOrdersForProductID")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<OrderDTO>> GetAllOrdersForProductID(int id)
    {
        if (id < 1)
        {
            return BadRequest($"invalid product id: {id}");
        }

        List<OrderDTO> ProductOrders = bl_Orders.GetAllOrdersForProductID(id);

        if (ProductOrders.Count == 0)
        {
            return NotFound($"No order found.");
        }

        return Ok(ProductOrders);
    }

    [HttpGet("AllProductOrdersForUser/{id}/{UserID}", Name = "AllProductOrdersForUser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<OrderDTO>> Get_All_ProductID_Orders_For_UserID(
        int id,
        int UserID
    )
    {
        if (id < 1)
        {
            return BadRequest($"invalid product id: {id}");
        }
        if (UserID < 1)
        {
            return BadRequest($"invalid user id: {id}");
        }

        List<OrderDTO> ProductOrders = bl_Orders.Get_All_ProductID_Orders_For_UserID(id, UserID);

        if (ProductOrders.Count == 0)
        {
            return NotFound($"No order found.");
        }

        return Ok(ProductOrders);
    }

    [HttpGet("AllUserOrders/{id}", Name = "GetAllOrdersForUserID")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<OrderDTO>> GetAllOrdersForUserID(int id)
    {
        if (id < 1)
        {
            return BadRequest($"invalid user id: {id}");
        }

        List<OrderDTO> ProductOrders = bl_Orders.GetAllOrdersForUserID(id);

        if (ProductOrders.Count == 0)
        {
            return NotFound($"No order found.");
        }

        return Ok(ProductOrders);
    }

    [HttpGet("{id}", Name = "GetOrderByID")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<OrderDTO> GetOrderByID(int id)
    {
        if (id < 1)
        {
            return BadRequest($"invalid id: {id}");
        }

        bl_Orders note = bl_Orders.Find(id);

        if (note == null)
        {
            return NotFound($"Order with id: {id} not found.");
        }

        OrderDTO ODTO = note.ODTO;
        return Ok(ODTO);
    }

    [HttpPost("Add", Name = "AddNewOrder")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<OrderDTO> AddNewOrder(OrderDTO newOrder)
    {
        if (
            newOrder == null
            || newOrder.UserID < 0
            || string.IsNullOrEmpty(newOrder.status)
            || string.IsNullOrEmpty(newOrder.ShippingAddress)
        )
        {
            return BadRequest("Invalid order data");
        }

        bl_Orders order = new bl_Orders(
            new OrderDTO(
                newOrder.ID,
                newOrder.UserID,
                newOrder.status,
                newOrder.ShippingAddress,
                newOrder.createDate,
                newOrder.updateDate
            )
        );

        order.Save();

        newOrder.ID = order.ID;

        return CreatedAtRoute("GetOrderByID", new { id = newOrder.ID }, newOrder);
    }

    [HttpPut("Update/{id}", Name = "UpdateOrder")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<OrderDTO> UpdateOrder(int id, OrderDTO updatedOrder)
    {
        if (
            id < 1
            || updatedOrder == null
            || updatedOrder.UserID < 0
            || string.IsNullOrEmpty(updatedOrder.status)
            || string.IsNullOrEmpty(updatedOrder.ShippingAddress)
        )
        {
            return BadRequest("Invalid order data");
        }

        bl_Orders SearchOrder = bl_Orders.Find(id);

        if (SearchOrder == null)
        {
            return NotFound($"Order with id: {id} not found");
        }

        SearchOrder.UserID = updatedOrder.UserID;
        SearchOrder.status = updatedOrder.status;
        SearchOrder.ShippingAddress = updatedOrder.ShippingAddress;

        SearchOrder.Save();

        return Ok(SearchOrder.ODTO);
    }

    [HttpDelete("Delete/{id}", Name = "DeleteOrder")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult DeleteOrder(int id)
    {
        if (id < 0)
        {
            return BadRequest("Invalid order id");
        }

        if (bl_Orders.DeleteOrder(id))
        {
            return Ok($"order with id: {id} has been removed successfully.");
        }
        else
        {
            return NotFound($"order with id: {id} not found");
        }
    }
}
