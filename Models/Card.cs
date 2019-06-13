using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCard.Models
{
    public class Card
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public long CardNumber { get; set; }

        public int CVV { get; set; }

        public string DateNowUtc { get; set; }

        public string Token { get; set; }
    }
}