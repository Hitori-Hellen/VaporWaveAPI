using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using API_PBL.Models.DatabaseModels;
using API_PBL.Models.DtoModels;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using API_PBL.Services;
using Microsoft.AspNetCore.Authorization;

namespace API_PBL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _dbcontext;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private static Random  rand = new Random();
        private static int randomNumber;
        public AuthController(DataContext context, IConfiguration configuration, IEmailService emailService)
        {
            _dbcontext = context;
            _configuration = configuration;
            _emailService = emailService;  
        }
        private void createPasswordHash(string password, out byte[] pHash, out byte[] pSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                pSalt = hmac.Key;
                pHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        [HttpPost("forgotPassword/email")]
        public async Task<IActionResult> ForgotPassword(string requestEmail)
        {
            var user = _dbcontext.Users.Where(w => w.email == requestEmail).FirstOrDefault();
            randomNumber = rand.Next(100000,999999);
            if(user == null)
            {
                return BadRequest("Email not found");
            }
            EmailDto email = new EmailDto
            {
                ToEmail = requestEmail,
                Subject = "Reset Code",
                Body = randomNumber.ToString()
            };
            await _emailService.SendEmail(email);
            return Ok("Code has been send");
        }
        [HttpPost("VerifyPassword")]
        public async Task<bool> VerifyPassword(string code)
        {
            if(code == randomNumber.ToString())
            {
                return true;
            }return false;
        }
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassWord(string Password, string email)
        {
            var user = _dbcontext.Users.Where(w => w.email == email).FirstOrDefault();
            var account = _dbcontext.Accounts.Where(w => w.username == user.userName).FirstOrDefault();
            createPasswordHash(Password, out byte[] pHash, out byte[] pSalt);
            account.passwordHash = pHash;
            account.passwordSalt = pSalt;
            await _dbcontext.SaveChangesAsync();
            return Ok("Reset successfully");
        }
        [HttpPost("register")]
        public async Task<IActionResult> registerAccount(registerAccount request)
        {
            var account = _dbcontext.Accounts.Where(w => w.username == request.username).FirstOrDefault();
            if(account != null)
            {
                return BadRequest("Username is exist");
            }
            Random rand = new Random();
            String randomId = rand.Next(100000, 999999).ToString();
            var user = new User
            {
                userId = randomId,
                userName = request.username,
                dateOfBirth = String.Empty,
                email = String.Empty,
                phone = String.Empty,
                userWallet = 0,
                imageName = String.Empty
            };
            _dbcontext.Users.Add(user);
            _dbcontext.SaveChanges();
            createPasswordHash(request.password, out byte[] pHash, out byte[] pSalt);
            var accountTemp = new Account
            {
                username = request.username,
                passwordHash = pHash,
                passwordSalt = pSalt,
                role = "User",
                userId = randomId
            };
            _dbcontext.Accounts.Add(accountTemp);
            await _dbcontext.SaveChangesAsync();
            return Ok("Successful");
        }
        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Account>>> getAllAccounts()
        {
            return await _dbcontext.Accounts.ToListAsync();
        }
        private bool verifyPasswordHash(string password, byte[] pHash, byte[] pSalt)
        {
            using(var hmac = new HMACSHA512(pSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(pHash);
            }
        }
        [HttpPost("login")]
        public async Task<ActionResult<string>> loginAccount(registerAccount request)
        {
            Account userLogin = await _dbcontext.Accounts.Where(w => w.username == request.username).FirstOrDefaultAsync();
            if(userLogin == null)
            {
                return BadRequest("User not found");
            }
            if(!(verifyPasswordHash(request.password, userLogin.passwordHash, userLogin.passwordSalt)))
            {
                return BadRequest("Username or Password is Wrong!");
            }
            string token = createToken(userLogin);
            return Ok(token);
        }
        private string createToken(Account userLogin)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userLogin.username),
                new Claim(ClaimTypes.Role, userLogin.role)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
            );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
        