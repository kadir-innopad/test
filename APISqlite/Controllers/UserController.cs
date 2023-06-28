using APISqlite.Data;
using APISqlite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Primitives;


namespace APISqlite.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private IUserRepository iuserRepository { get; set; }
        public UserController(IUserRepository iuserRepository)
        {
            
            this.iuserRepository = iuserRepository;
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public Task<IActionResult> GetAllUser()

        {
          
            List<User> listofuser = iuserRepository.GetUserlist();
                return Task.FromResult<IActionResult>(Ok(listofuser));
            
        }

        [HttpGet]
        [Route("api/User/GetUserInfo")]
        public async Task<ActionResult> GetUserByID(int ID)
        {
            var userID = await iuserRepository.GetUserById(ID);
            return Ok(userID);
        }

        [HttpPost]
        [Route("api/User/Delete")]
        public async Task<ActionResult> DeleteUser(int ID)
        {
            var userID = await iuserRepository.DeleteUser(ID);
            return Ok(userID);
        }

        [HttpPost]
        [Route("api/User/Create")]
        public async Task<ActionResult> AddUser(User user)
        {
            var userID = await iuserRepository.CreateUser(user);
            return Ok(userID);

        }


        [HttpPost]
        [Route("api/User/Update")]
        public async Task<ActionResult> UpdateUser(User user)
        {
            await iuserRepository.UpdateUser(user);
            return Ok();

        }


        [HttpPost]
        [AllowAnonymous]
        [Route("api/User/login")]
        public async Task<IActionResult> Login(string username, string email)
        {
            var token = iuserRepository.Authenticate(username, email);
            return Ok(token);
        }




    }
}
