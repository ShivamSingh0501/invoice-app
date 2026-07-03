using Microsoft.AspNetCore.Mvc;
using InvoiceApp.Models;
using System.Collections.Generic;

namespace InvoiceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        /// <summary>
        /// Returns the current invoice with its line items.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(InvoiceDto), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetInvoice()
        {
            // Previously: "List<Item> items = null;" then checked items.Count == 0
            // which threw a NullReferenceException before any data could be returned.
            // Fixed: build a real, non-null invoice with items.
            var invoice = new InvoiceDto
            {
                InvoiceId = 1,
                CustomerName = "John Doe",
                Items = new List<Item>
                {
                    new Item { Name = "Widget A", Price = 19.99 },
                    new Item { Name = "Widget B", Price = 5.50 }
                }
            };

            if (invoice.Items.Count == 0)
            {
                return NotFound("No invoice found");
            }

            return Ok(invoice);
        }
    }
}
