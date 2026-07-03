using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InvoiceApp.Data;
using InvoiceApp.Models;
using System.Linq;

namespace InvoiceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly AppDbContext _db;

        public InvoiceController(AppDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Returns the current invoice with its line items, read from the database.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(InvoiceDto), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetInvoice()
        {
            // Previously: "List<Item> items = null;" then checked items.Count == 0
            // which threw a NullReferenceException before any data could be returned.
            // Fixed: query real invoice data from SQLite via EF Core.
            var invoice = _db.Invoices
                .Include(i => i.Items)
                .FirstOrDefault(i => i.InvoiceID == 1);

            if (invoice == null || invoice.Items.Count == 0)
            {
                return NotFound("No invoice found");
            }

            var dto = new InvoiceDto
            {
                InvoiceId = invoice.InvoiceID,
                CustomerName = invoice.CustomerName,
                Items = invoice.Items
                    .Select(i => new Item { Name = i.Name, Price = i.Price })
                    .ToList()
            };

            return Ok(dto);
        }
    }
}
