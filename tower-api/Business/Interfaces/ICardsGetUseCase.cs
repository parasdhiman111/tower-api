using System;
using tower_api.Business.Models;

namespace tower_api.Business.Interfaces
{
	public interface ICardsGetUseCase
	{
        Task<IEnumerable<Card>> GetCardsAsync();
        Task<Card> GetCardByNumberAsync(string cardNumber);
    }
}

