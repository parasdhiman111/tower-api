using System;
using System.ComponentModel.DataAnnotations;

namespace tower_api.Business.Models
{
	public class Card
	{
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string CreditCard { get; set; }
        public required string CVC { get; set; }
        public required string ExpiryDate { get; set; }
    }
}

