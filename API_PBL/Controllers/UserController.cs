using API_PBL.Models.DatabaseModels;
using API_PBL.Models.DtoModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_PBL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        public UserController(DataContext context)
        {
            _context = context;
        }
        [HttpPost("Information"), Authorize(Roles = "User")]
        public async Task<ActionResult<User>> updateInformation(UserDto request){
            User user = _context.Users.Where(w => w.userName == request.userName).FirstOrDefault();
            user.userName = request.userName;
            user.dateOfBirth = request.dateOfBirth;
            user.email = request.email;
            user.phone = request.phone;
            await _context.SaveChangesAsync();
            return Ok(user);
        }
        [HttpPost("Wallet"), Authorize(Roles = "User")]
        public async Task<ActionResult<User>> getMoreCast(WalletDto request)
        {
            User user = _context.Users.Where(w => w.userName == request.userName).FirstOrDefault();
            user.userWallet += request.cast;
            await _context.SaveChangesAsync();
            return Ok(user);
        }
    }
}
