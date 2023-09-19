using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tower_api.Repositories.Models
{

    [Table("Cards")]
    public class Card
	{
        [Key]
        public int Id { get; set; }
        [Required]    
        public required string Name { get; set; }
        [Required]
        public required string CreditCard { get; set; }
        [Required]
        public required string CVC { get; set; }
        [Required]
        public required string ExpiryDate { get; set; }
    }
}

