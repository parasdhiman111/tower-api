using System;
using AutoMapper;
using credit_work_app.Models.Exceptions;
using tower_api.Business.Interfaces;
using tower_api.Business.Models;
using tower_api.Repositories.Interfaces;

namespace tower_api.Business.Implementations
{
	public class CardsGetUseCase : ICardsGetUseCase
	{
        private readonly ICardRepository _cardRepository;
        private readonly IMapper _mapper;

        public CardsGetUseCase(ICardRepository cardRepository, IMapper mapper)
        {
            _cardRepository = cardRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Card>> GetCardsAsync()
        {
            var cards= await _cardRepository.GetAllCardsAsync();

            return _mapper.Map<IEnumerable<Card>>(cards);
        }

        public async Task<Card> GetCardByNumberAsync(string cardNumber)
        {
            var card = await _cardRepository.GetCardByNumberAsync(cardNumber);
            if (card == null)
            {
                throw new NoCard();
            }
            return _mapper.Map<Card>(card);
        }
    }
}

