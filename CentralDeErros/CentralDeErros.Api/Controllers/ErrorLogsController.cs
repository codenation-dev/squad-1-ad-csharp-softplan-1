using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CentralDeErros.Data.Context;
using CentralDeErros.Domain.Models;

namespace CentralDeErros.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorLogsController : ControllerBase
    {
        private readonly CentralDeErrosContext _context;

        public ErrorLogsController(CentralDeErrosContext context)
        {
            _context = context;
        }

        // GET: api/ErrorLogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ErrorLog>>> GetErrorLogs()
        {
            return await _context.ErrorLogs.ToListAsync();
        }

        // GET: api/ErrorLogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ErrorLog>> GetErrorLog(Guid id)
        {
            var errorLog = await _context.ErrorLogs.FindAsync(id);

            if (errorLog == null)
            {
                return NotFound();
            }

            return errorLog;
        }

        // PUT: api/ErrorLogs/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutErrorLog(Guid id, ErrorLog errorLog)
        {
            if (id != errorLog.Id)
            {
                return BadRequest();
            }

            _context.Entry(errorLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ErrorLogExists(id))
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

        // POST: api/ErrorLogs
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ErrorLog>> PostErrorLog(ErrorLog errorLog)
        {
            _context.ErrorLogs.Add(errorLog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetErrorLog", new { id = errorLog.Id }, errorLog);
        }

        // DELETE: api/ErrorLogs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ErrorLog>> DeleteErrorLog(Guid id)
        {
            var errorLog = await _context.ErrorLogs.FindAsync(id);
            if (errorLog == null)
            {
                return NotFound();
            }

            _context.ErrorLogs.Remove(errorLog);
            await _context.SaveChangesAsync();

            return errorLog;
        }

        private bool ErrorLogExists(Guid id)
        {
            return _context.ErrorLogs.Any(e => e.Id == id);
        }
    }
}
