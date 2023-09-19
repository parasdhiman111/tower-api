using System;
using tower_api.Business.Models;
using tower_api.Models.Requests;

namespace tower_api.Business.Interfaces
{
	public interface ICardsCreateUseCase
	{
        public Task CreateCardAsync(ApiCardCreateRequest apiCardCreateRequest);

    }
}

