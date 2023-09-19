using System;
using Newtonsoft.Json;
using tower_api.Business.Models;

namespace tower_api.Models.Responses
{
	public class ApiCardResponse
	{
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? CreditCard { get; set; }
        public string? CVC { get; set; }
        public string? ExpiryDate { get; set; }


        public ApiCardResponse(Card card)
        {
            Id = card.Id;
            Name = card.Name;
            CreditCard = card.CreditCard;
            CVC = card.CVC;
            ExpiryDate = card.ExpiryDate;
        }

        [JsonConstructor]
        public ApiCardResponse(int id, string? name, string? creditCard, string? cVC, string? expiryDate)
        {
            Id = id;
            Name = name;
            CreditCard = creditCard;
            CVC = cVC;
            ExpiryDate = expiryDate;
        }
    }
}

