using DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly User _user;

        public UserController(User user)
        {
            this._user = user;
        }

        // GET: api/<UserController>
        [HttpGet("GetAllUsers")]
        public IActionResult Get()
        {
            try
            {
                List<User> users = _user.GetUsers();
                if (users == null || users.Count == 0)
                    return StatusCode(StatusCodes.Status404NotFound);
                else
                    return StatusCode(StatusCodes.Status200OK, users);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // GET api/<UserController>/5
        [HttpGet("GetUser/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                User user = _user.GetUserById(id);
                if (user != null)
                    return StatusCode(StatusCodes.Status200OK, user);
                else
                    return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex); ;
            }
        }

        // POST api/<UserController>
        [HttpPost]
        public IActionResult Post([FromBody] User userModel)
        {
            try
            {
                int userId = _user.AddUser(userModel);
                if (userId > 0)
                    return StatusCode(StatusCodes.Status201Created, userModel);
                else
                    return StatusCode(StatusCodes.Status400BadRequest); //we can return some other status code may be have to see
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("UpdateUser{id}")]
        public IActionResult Put(int id, [FromBody] User userModel)
        {
            try
            {
                int userId = _user.UpdateUser(userModel, id);
                if (userId > 0)
                    return StatusCode(StatusCodes.Status204NoContent, "user updated successfully");
                else
                    return StatusCode(StatusCodes.Status404NotFound); //we can return some other status code may be have to see
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("DeleteUser{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool IsDeleted = _user.DeleteUser(id);
                if (IsDeleted)
                    return StatusCode(StatusCodes.Status204NoContent, "user deleted successfully");
                else
                    return StatusCode(StatusCodes.Status400BadRequest); //we can return some other status code may be have to see
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

    }
}
