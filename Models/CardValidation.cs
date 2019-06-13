using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCard.Models
{
    public class CardValidation
    {
        public string RegistrationDate { get; set; }

        public string Token { get; set; }

        public int CVV { get; set; }
    }
}
