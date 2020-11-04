using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Iovanesc_Roxana_Lab5.Models;

namespace Iovanesc_Roxana_Lab5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly ExpenseContext _context;

        public ExpensesController(ExpenseContext context)
        {
            _context = context;
        }

        // GET: api/ExpenseDTO
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpenseDTO>>> GetExpense()
        {
            return await _context.ExpenseDTO.ToListAsync();
        }

        // GET: api/ExpenseDTO/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseDTO>> GetExpenses(int id)
        {
            var expenses = await _context.ExpenseDTO.FindAsync(id);

            if (expenses == null)
            {
                return NotFound();
            }

            return expenses;
        }

        // PUT: api/ExpenseDTO/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpenses(int id, ExpenseDTO expenses)
        {
            if (id != expenses.Id)
            {
                return BadRequest();
            }

            _context.Entry(expenses).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpensesExists(id))
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

        // POST: api/ExpenseDTO
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ExpenseDTO>> PostExpenses(ExpenseDTO expenses)
        {
            _context.ExpenseDTO.Add(expenses);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetExpenses), new { id = expenses.Id }, expenses);
        }

        // DELETE: api/ExpenseDTO/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ExpenseDTO>> DeleteExpenses(int id)
        {
            var expenses = await _context.ExpenseDTO.FindAsync(id);
            if (expenses == null)
            {
                return NotFound();
            }

            _context.ExpenseDTO.Remove(expenses);
            await _context.SaveChangesAsync();

            return expenses;
        }

        private bool ExpensesExists(int id)
        {
            return _context.ExpenseDTO.Any(e => e.Id == id);
        }
    }
}
