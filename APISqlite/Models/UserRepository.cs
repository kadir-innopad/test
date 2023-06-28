using APISqlite.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
 
namespace APISqlite.Models
{
    public class UserRepository : IUserRepository
    {
        private readonly  IConfiguration _configuration;
       
        private readonly TblUserContext _dbContext;
        public UserRepository(TblUserContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }



        public List<User> GetUserlist()
        {
            
            List<User> userslist = _dbContext.Users.ToList();
            return userslist;


        }


        public async Task<User> GetUserById(int id)
        {
            return await _dbContext.Users.FirstAsync(x => x.Id == id);

        }

        public async Task<User> DeleteUser(int id)
        {
            var user = await GetUserById(id);
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return user;

        }



        public async Task<int> CreateUser(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user.Id;
        }

        async Task IUserRepository.UpdateUser(User user)
        {
            _dbContext.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public string Authenticate(string username, string email)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == email && u.FirstName == username);
            if (user != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenkey = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, username),
                        new Claim(ClaimTypes.Name, email)
                    }),
                    Expires = DateTime.Now.AddHours(1),
                    SigningCredentials =
                    new SigningCredentials(
                        new SymmetricSecurityKey(tokenkey),
                        SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
              return  tokenHandler.WriteToken(token);
               
            }
            return null;
        }
    }
}
