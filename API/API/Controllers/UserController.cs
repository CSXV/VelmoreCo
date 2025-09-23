using BusinessLayer;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Utils;

namespace API.Controllers;

[ApiController]
[Route("api/Users")]
public class UserController : ControllerBase
{
    [HttpGet("{id}", Name = "GetUserByID")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<UserDTO> GetUserByID(int id)
    {
        if (id < 1)
        {
            return BadRequest($"invalid id: {id}");
        }

        bl_Users user = bl_Users.Find(id);

        if (user == null)
        {
            return NotFound($"User with id: {id} not found.");
        }

        UserDTO UDTO = user.UDTO;
        return Ok(UDTO);
    }

    [HttpPut("Update/{id}", Name = "UpdateUser")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<UserDTO> UpdateUser(int id, UserDTO updatedUser)
    {
        if (
            id < 1
            || updatedUser == null
            || string.IsNullOrEmpty(updatedUser.firstName)
            || string.IsNullOrEmpty(updatedUser.lastName)
            || string.IsNullOrEmpty(updatedUser.email)
            || string.IsNullOrEmpty(updatedUser.password)
        )
        {
            return BadRequest("Invalid user data");
        }

        bl_Users SearchUser = bl_Users.Find(id);

        if (SearchUser == null)
        {
            return NotFound($"User with id: {id} not found");
        }

        SearchUser.firstName = updatedUser.firstName;
        SearchUser.lastName = updatedUser.lastName;

        SearchUser.email = updatedUser.email;
        SearchUser.password = clsUtils.ComputeHash(updatedUser.password);
        SearchUser.type = updatedUser.type;

        SearchUser.Save();

        return Ok(SearchUser.UDTO);
    }

    [HttpDelete("Delete/{id}", Name = "DeleteUser")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult DeleteUser(int id)
    {
        if (id < 0)
        {
            return BadRequest("Invalid user id");
        }

        if (bl_Users.DeleteUser(id))
        {
            return Ok($"user with id: {id} has been removed successfully.");
        }
        else
        {
            return NotFound($"User with id: {id} not found");
        }
    }

    [HttpPost("Add", Name = "AddNewUser")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<UserDTO> AddNewUser(UserDTO newUser)
    {
        if (
            newUser == null
            || string.IsNullOrEmpty(newUser.firstName)
            || string.IsNullOrEmpty(newUser.lastName)
            || string.IsNullOrEmpty(newUser.email)
            || string.IsNullOrEmpty(newUser.password)
            || newUser.type < 0
        )
        {
            return BadRequest("Invalid user data");
        }

        // string passwordHash = BCrypt.Net.BCrypt.HashPassword(newUser.password);
        string passwordHash = clsUtils.ComputeHash(newUser.password);

        bl_Users user = new bl_Users(
            new UserDTO(
                newUser.ID,
                newUser.firstName,
                newUser.lastName,
                newUser.email,
                passwordHash,
                newUser.createDate,
                newUser.updateDate,
                newUser.type
            )
        );

        user.Save();

        newUser.ID = user.ID;

        return CreatedAtRoute("GetUserByID", new { id = newUser.ID }, newUser);
    }

    [HttpPost("login", Name = "login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<UserDTO> login([FromBody] LoginRequest loginRequest)
    {
        if (string.IsNullOrEmpty(loginRequest.email) || string.IsNullOrEmpty(loginRequest.password))
        {
            return BadRequest($"invalid user creds");
        }

        string passwordHash = clsUtils.ComputeHash(loginRequest.password);
        // string passwordHash = loginRequest.password;

        bl_Users user = bl_Users.login(loginRequest.email, passwordHash);

        if (user == null)
        {
            return NotFound($"could not procced in login");
        }

        UserDTO UDTO = user.UDTO;
        return Ok(UDTO);
    }
}
