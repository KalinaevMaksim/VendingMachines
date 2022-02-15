#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendingMachineWebAPINet5.Models;

namespace VendingMachinesWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoinsController : ControllerBase
    {
        private readonly VendingmachinefordrinksContext _context;

        public CoinsController(VendingmachinefordrinksContext context)
        {
            _context = context;
        }

        // GET: api/Coins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Coin>>> GetCoins()
        {
            return await _context.Coins.ToListAsync();
        }

        // GET: api/Coins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Coin>> GetCoin(int id)
        {
            var coin = await _context.Coins.FindAsync(id);

            if (coin == null)
            {
                return NotFound();
            }

            return coin;
        }

        // GET: api/Coins/VendingMachine/5
        [HttpGet("VendingMachine/{id}")]
        public async Task<List<Coin>> GetCoinVendingMachine(int id)
        {
            Task<List<Coin>> coins = _context.Coins.Include(coin => coin.VendingMachineCoins).Where(coin => coin.VendingMachineCoins.Any(machine => machine.IdVendingMachine == id)).ToListAsync();
            coins.Result.ForEach(coin => coin.VendingMachineCoins = coin.VendingMachineCoins.Where(machine => machine.IdVendingMachine == id).ToList());
            return await coins;
        }

        // GET: api/Coins/VendingMachine/Active/5
        [HttpGet("IsStock/VendingMachine/{id}")]
        public async Task<IEnumerable<Coin>> GetIsStockCoinVendingMachine(int id)
        {
            List<Coin> coins = await GetCoinVendingMachine(id);
            return coins.Where(machine => machine.VendingMachineCoins.All(machine => machine.Count > 0));
        }

        // GET: api/Coins/VendingMachine/Active/5
        [HttpGet("Active/VendingMachine/{id}")]
        public async Task<IEnumerable<Coin>> GetActiveCoinVendingMachine(int id)
        {
            List<Coin> coins = await GetCoinVendingMachine(id);
            return coins.Where(machine => machine.VendingMachineCoins.All(machine => machine.IsActive));
        }

        // PUT: api/Coins/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoin(int id, Coin coin)
        {
            if (id != coin.Id)
            {
                return BadRequest();
            }

            _context.Entry(coin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoinExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Coins
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Coin>> PostCoin(Coin coin)
        {
            _context.Coins.Add(coin);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoin", new { id = coin.Id }, coin);
        }

        // DELETE: api/Coins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoin(int id)
        {
            var coin = await _context.Coins.FindAsync(id);
            if (coin == null)
            {
                return NotFound();
            }

            _context.Coins.Remove(coin);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CoinExists(int id)
        {
            return _context.Coins.Any(e => e.Id == id);
        }
    }
}
