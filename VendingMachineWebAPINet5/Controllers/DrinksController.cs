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
    public class DrinksController : ControllerBase
    {
        private readonly VendingmachinefordrinksContext _context;

        public DrinksController(VendingmachinefordrinksContext context)
        {
            _context = context;
        }

        // GET: api/Drinks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Drink>>> GetDrinks()
        {
            return await _context.Drinks.ToListAsync();
        }

        // GET: api/Drinks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Drink>> GetDrink(int id)
        {
            var drink = await _context.Drinks.FindAsync(id);

            if (drink == null)
            {
                return NotFound();
            }

            return drink;
        }

        // GET: api/Drinks/VendingMachine/5
        [HttpGet("VendingMachine/{id}")]
        public async Task<List<Drink>> GetDrinkVendingMachine(int id)
        {
            Task<List<Drink>> drinks = _context.Drinks.Include(drink => drink.VendingMachineDrinks).Where(drink => drink.VendingMachineDrinks.Any(machine => machine.IdVendingMachine == id)).ToListAsync();
            drinks.Result.ForEach(drink => drink.VendingMachineDrinks = drink.VendingMachineDrinks.Where(machine => machine.IdVendingMachine == id).ToList());
            return await drinks;
        }

        // GET: api/Drinks/5
        [HttpGet("IsStock/VendingMachine/{id}")]
        public async Task<IEnumerable<Drink>> GetIsStockDrinkVendingMachine(int id)
        {
            List<Drink> drinks = await GetDrinkVendingMachine(id);
            return drinks.Where(machine => machine.VendingMachineDrinks.All(machine => machine.Count > 0));
        }

        // PUT: api/Drinks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDrink(int id, Drink drink)
        {
            if (id != drink.Id)
            {
                return BadRequest();
            }

            _context.Entry(drink).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DrinkExists(id))
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

        // POST: api/Drinks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Drink>> PostDrink(Drink drink)
        {
            _context.Drinks.Add(drink);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDrink", new { id = drink.Id }, drink);
        }

        // DELETE: api/Drinks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDrink(int id)
        {
            var drink = await _context.Drinks.FindAsync(id);
            if (drink == null)
            {
                return NotFound();
            }

            _context.Drinks.Remove(drink);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DrinkExists(int id)
        {
            return _context.Drinks.Any(e => e.Id == id);
        }
    }
}
