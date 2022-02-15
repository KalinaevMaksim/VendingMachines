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
    public class VendingMachineCoinsController : ControllerBase
    {
        private readonly VendingmachinefordrinksContext _context;

        public VendingMachineCoinsController(VendingmachinefordrinksContext context)
        {
            _context = context;
        }

        // GET: api/VendingMachineCoins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VendingMachineCoin>>> GetVendingMachineCoins()
        {
            return await _context.VendingMachineCoins.Include(machine => machine.IdCoinNavigation).ToListAsync();
        }

        // GET: api/VendingMachineCoins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VendingMachineCoin>> GetVendingMachineCoin(int id)
        {
            var vendingMachineCoin = await _context.VendingMachineCoins
                .Include(machine => machine.IdCoinNavigation)
                .FirstOrDefaultAsync(machine => machine.Id == id);

            if (vendingMachineCoin == null)
            {
                return NotFound();
            }

            return vendingMachineCoin;
        }

        // GET: api/VendingMachine/5
        [HttpGet("VendingMachine/{idVendingMachine}")]
        public async Task<IEnumerable<VendingMachineCoin>> GetVendingMachineCoinByVendingMachine(int idVendingMachine)
        {
            List<VendingMachineCoin> vendingMachineCoin = await _context.VendingMachineCoins
                .Where(machineCoin => machineCoin.IdVendingMachine == idVendingMachine)
                .Include(machine => machine.IdCoinNavigation)
                .ToListAsync();
            return vendingMachineCoin;
        }

        // GET: api/VendingMachine/Coin/5/5
        [HttpGet("VendingMachine/Coin/{idVendingMachine}/{idCoin}")]
        public async Task<ActionResult<VendingMachineCoin>> GetVendingMachineCoin(int idVendingMachine, int idCoin)
        {
            VendingMachineCoin vendingMachineCoin = await _context.VendingMachineCoins
                .Include(machine => machine.IdCoinNavigation)
                .FirstOrDefaultAsync(machineCoin => machineCoin.IdVendingMachine == idVendingMachine
                && machineCoin.IdCoin == idCoin);

            if (vendingMachineCoin == null)
            {
                return NotFound();
            }

            return vendingMachineCoin;
        }

        // PUT: api/VendingMachineCoins/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVendingMachineCoin(int id, VendingMachineCoin vendingMachineCoin)
        {
            if (id != vendingMachineCoin.Id)
            {
                return BadRequest();
            }

            _context.Entry(vendingMachineCoin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendingMachineCoinExists(id))
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

        // POST: api/VendingMachineCoins
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VendingMachineCoin>> PostVendingMachineCoin(VendingMachineCoin vendingMachineCoin)
        {
            _context.VendingMachineCoins.Add(vendingMachineCoin);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVendingMachineCoin", new { id = vendingMachineCoin.Id }, vendingMachineCoin);
        }

        // DELETE: api/VendingMachineCoins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendingMachineCoin(int id)
        {
            var vendingMachineCoin = await _context.VendingMachineCoins.FindAsync(id);
            if (vendingMachineCoin == null)
            {
                return NotFound();
            }

            _context.VendingMachineCoins.Remove(vendingMachineCoin);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VendingMachineCoinExists(int id)
        {
            return _context.VendingMachineCoins.Any(e => e.Id == id);
        }
    }
}
