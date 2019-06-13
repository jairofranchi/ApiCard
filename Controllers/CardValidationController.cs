using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardValidationController : ControllerBase
    {
        private readonly CardContext _context;

        string jsonResult = "";

        public CardValidationController(CardContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> ValidateCardInfo(CardValidation card)
        {
            bool validation = await _context.Card.Where(c => c.Token.Equals(card.Token) 
                && c.DateNowUtc.Equals(card.RegistrationDate)
                && c.CVV == card.CVV).AnyAsync();

            jsonResult = "{\"Validated\": " + validation + "}";

            if (validation)
            {
                return Ok(jsonResult);
            }
            else
            {
                return NotFound(jsonResult);
            }

        }
    }
}