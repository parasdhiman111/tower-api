using AutoMapper;
using tower_api.Business.Interfaces;
using tower_api.Models.Requests;
using tower_api.Repositories.Interfaces;

namespace tower_api.Business.Implementations
{
    public class CardsCreateUseCase : ICardsCreateUseCase
    {

        private readonly ICardRepository _cardRepository;
        private readonly IMapper _mapper;

        public CardsCreateUseCase(ICardRepository cardRepository, IMapper mapper)
        {
            _cardRepository = cardRepository;
            _mapper = mapper;
        }

        public async Task CreateCardAsync(ApiCardCreateRequest apiCardCreateRequest)
        {
            var cardToCreate = _mapper.Map<Repositories.Models.Card>(apiCardCreateRequest);

            await _cardRepository.CreateCardAsync(cardToCreate);
        }
    }
}

