using System.Web.Http;
using API_PBL.Models.DatabaseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_PBL.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class ReceiptController : ControllerBase
    {
        private readonly DataContext _context;

        public ReceiptController(DataContext context)
        {
            _context = context;
        }
        [Microsoft.AspNetCore.Mvc.HttpGet("all")]
        public async Task<ActionResult<List<Receipt>>> GetAllReceipt()
        {
            return Ok(await _context.Receipts.ToListAsync());
        }
        [Microsoft.AspNetCore.Mvc.HttpPost("{userName}/{gameName}"), Authorize(Roles = "User")]
        public async Task<IActionResult> PayUp(string gameName, string userName)
        {
            var user = _context.Users.Where(w => w.userName == userName).FirstOrDefault();
            var game = _context.Games.Where(w => w.Name == gameName).FirstOrDefault();
            if (user.userWallet >= game.Price)
            {
                user.userWallet -= game.Price;
                Library library = new Library
                {
                    userId = user.userId,   
                    gameName = game.Name
                };
                _context.Library.Add(library);
                await _context.SaveChangesAsync();
                Receipt receipt = new Receipt
                {
                    gamePrice = game.Price,
                    purchaseDate = DateTime.Now,
                    userId = user.userId,
                    gameId = game.Id
                };
                _context.Receipts.Add(receipt);
                await _context.SaveChangesAsync();
                return Ok("Successfully");
            }
            return BadRequest("Not enough money");
        }
    }
}
