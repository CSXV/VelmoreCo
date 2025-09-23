using BusinessLayer;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/Products")]
public class ProdcutController : ControllerBase
{
    [HttpGet("All", Name = "GetAllProducts")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<ProductDTO>> GetAllProducts()
    {
        List<ProductDTO> ProductsList = bl_Products.GetAllProducts();

        if (ProductsList.Count == 0)
        {
            return NotFound("No products was found.");
        }

        return Ok(ProductsList);
    }

    [HttpGet("{id}", Name = "GetProductByID")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ProductDTO> GetProductByID(int id)
    {
        if (id < 1)
        {
            return BadRequest($"invalid id: {id}");
        }

        bl_Products Product = bl_Products.Find(id);

        if (Product == null)
        {
            return NotFound($"Product with id: {id} not found.");
        }

        ProductDTO PDTO = Product.PDTO;
        return Ok(PDTO);
    }

    [HttpPost("Add", Name = "AddNewProduct")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<ProductDTO> AddNewProduct(ProductDTO newProduct)
    {
        if (
            newProduct == null
            || string.IsNullOrEmpty(newProduct.certification)
            || string.IsNullOrEmpty(newProduct.clarity)
            || string.IsNullOrEmpty(newProduct.cut)
            || string.IsNullOrEmpty(newProduct.description)
            || string.IsNullOrEmpty(newProduct.gemstone)
            || string.IsNullOrEmpty(newProduct.material)
            || string.IsNullOrEmpty(newProduct.name)
            || newProduct.categoryID < 0
            || newProduct.carat < 0
            || newProduct.price < 0
            || newProduct.stock < 0
        )
        {
            return BadRequest("Invalid Product data");
        }

        bl_Products Product = new bl_Products(
            new ProductDTO(
                newProduct.ID,
                newProduct.name,
                newProduct.description,
                newProduct.categoryID,
                newProduct.price,
                newProduct.material,
                newProduct.gemstone,
                newProduct.carat,
                newProduct.clarity,
                newProduct.cut,
                newProduct.certification,
                newProduct.stock,
                newProduct.isFeatured,
                newProduct.createDate,
                newProduct.updateDate
            )
        );

        Product.Save();

        newProduct.ID = Product.ID;

        return CreatedAtRoute("GetProductByID", new { id = newProduct.ID }, newProduct);
    }

    [HttpPut("Update/{id}", Name = "UpdateProduct")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ProductDTO> UpdateProduct(int id, ProductDTO updatedProduct)
    {
        if (
            id < 1
            || updatedProduct == null
            || string.IsNullOrEmpty(updatedProduct.certification)
            || string.IsNullOrEmpty(updatedProduct.clarity)
            || string.IsNullOrEmpty(updatedProduct.cut)
            || string.IsNullOrEmpty(updatedProduct.description)
            || string.IsNullOrEmpty(updatedProduct.gemstone)
            || string.IsNullOrEmpty(updatedProduct.material)
            || string.IsNullOrEmpty(updatedProduct.name)
            || updatedProduct.categoryID < 0
            || updatedProduct.carat < 0
            || updatedProduct.price < 0
            || updatedProduct.stock < 0
        )
        {
            return BadRequest("Invalid Product data");
        }

        bl_Products SearchProduct = bl_Products.Find(id);

        if (SearchProduct == null)
        {
            return NotFound($"Product with id: {id} not found");
        }

        SearchProduct.categoryID = updatedProduct.categoryID;

        SearchProduct.name = updatedProduct.name;
        SearchProduct.description = updatedProduct.description;
        SearchProduct.price = updatedProduct.price;
        SearchProduct.material = updatedProduct.material;
        SearchProduct.gemstone = updatedProduct.gemstone;
        SearchProduct.carat = updatedProduct.carat;
        SearchProduct.clarity = updatedProduct.clarity;
        SearchProduct.cut = updatedProduct.cut;
        SearchProduct.certification = updatedProduct.certification;
        SearchProduct.createDate = updatedProduct.createDate;
        SearchProduct.updateDate = updatedProduct.updateDate;
        SearchProduct.isFeatured = updatedProduct.isFeatured;

        SearchProduct.Save();

        return Ok(SearchProduct.PDTO);
    }

    [HttpDelete("Delete/{id}", Name = "DeleteProduct")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult DeleteProduct(int id)
    {
        if (id < 0)
        {
            return BadRequest("Invalid Product id");
        }

        if (bl_Products.DeleteProduct(id))
        {
            return Ok($"Product with id: {id} has been removed successfully.");
        }
        else
        {
            return NotFound($"Product with id: {id} not found");
        }
    }
}
