using System;
using tower_api.Repositories.Models;

namespace tower_api.Repositories.Interfaces
{
	public interface ICardRepository
	{
        public Task<IEnumerable<Card>> GetAllCardsAsync();
        public Task CreateCardAsync(Card card);
        public Task<Card> GetCardByNumberAsync(string cardNumber);

    }
}

