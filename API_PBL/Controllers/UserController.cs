using API_PBL.Models.DatabaseModels;
using API_PBL.Models.DtoModels;
using API_PBL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_PBL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IBlobService _blobService;
        private readonly DataContext _context;
        public UserController(DataContext context, IBlobService blobService)
        {
            _context = context;
            _blobService = blobService;
        }
        [HttpGet("userHomepage"), Authorize(Roles = "User")]
        public async Task<ActionResult<UserHomePageDto>> getUserInformation(string userName)
        {
            var user = _context.Users.Where(w => w.userName == userName).FirstOrDefault();
            var responseUser = new UserHomePageDto
            {
                userName = userName,
                imageUri = _blobService.GetBlob(user.imageName, "images")
            };
            return responseUser;
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
        [HttpPut("userImage"), Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateImage(string userName, IFormFile file)
        {
            var user = _context.Users.Where(w => w.userName == userName).FirstOrDefault();
            bool res = await AddImage(user.userId, file);
            if (!res)
            {
                return BadRequest("Upload image failed");
            }
            return Ok("Successfully");
        }
        private async Task<bool> AddImage(string userId, IFormFile file)
        {
            if (file == null)
            {
                return false;
            }
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var res = await _blobService.UploadBlobAsync(fileName, file, "images");
            if (res)
            {
                var user = _context.Users.Find(userId);
                user.imageName = fileName;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
