using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.Xml;
using AutoMapper;
using Azure.Core;
using credit_work_app.Models.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using tower_api.Business.Interfaces;
using tower_api.Business.Models;
using tower_api.Models.Requests;
using tower_api.Models.Responses;
using tower_api.Utilities;

namespace tower_api.Controllers
{
    [Route("api/cards")]
    [ApiController]
    public class CardController : ControllerBase
	{
        private readonly ICardsGetUseCase _cardGetUseCase;
        private readonly ICardsCreateUseCase _cardCreateUseCase;
        private readonly IMapper _mapper;

        public CardController(ICardsGetUseCase cardGetUseCase, ICardsCreateUseCase cardCreateUseCase, IMapper mapper)
		{
            _cardGetUseCase = cardGetUseCase;
            _cardCreateUseCase = cardCreateUseCase;
            _mapper = mapper;
		}


        [HttpGet]
        public async Task<IActionResult> GetCardsAsync()
        {
            try
            {
                var cards = await _cardGetUseCase.GetCardsAsync();
                return HandleResponse(cards);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet("{cardNumber}", Name = "GetCard")]
        public async Task<IActionResult> GetCard(string cardNumber)
        {
            try
            {
                var category = await _cardGetUseCase.GetCardByNumberAsync(cardNumber);
                return Ok(new ApiCardResponse(category));
            }
            catch (NoCard ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPost]
        public  async Task<IActionResult> CreateCard([FromBody] string encryptedData)
        {
            try
            {
                return await DecryptDataAndValidateRequest(encryptedData);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private IActionResult HandleResponse(IEnumerable<Card> cards)
        {
            return Ok(cards.Select(cards => new ApiCardResponse(cards)));
        }

        private IActionResult HandleException(Exception ex)
        {
            if (ex is BadGatewayException badGatewayException)
                return HandleBadGateWayException(badGatewayException);

            return HandleUnhandledException(ex);
        }

        private IActionResult HandleBadGateWayException(BadGatewayException ex)
        {
            return StatusCode(StatusCodes.Status502BadGateway, new ApiError(ex));
        }

        private IActionResult HandleUnhandledException(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ApiError(ex));
        }

        private async Task<IActionResult> DecryptDataAndValidateRequest(string encryptedData)
        {
            try
            {
                string decryptedData = EncryptionDecryptionHelper.Decrypt(encryptedData);
                if (decryptedData == null)
                    return BadRequest("Failed to decrypt data");
                ApiCardCreateRequest apiCardCreateRequest = DeserializeApiCardCreateRequest(decryptedData);
                List<string> errors = ValidateApiCardCreateRequest(apiCardCreateRequest);
                if (errors.Any())
                    return BadRequest(errors);
                await _cardCreateUseCase.CreateCardAsync(apiCardCreateRequest);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        private ApiCardCreateRequest DeserializeApiCardCreateRequest(string decryptedData)
        {
            return JsonConvert.DeserializeObject<ApiCardCreateRequest>(decryptedData);
        }
          
        private List<string> ValidateApiCardCreateRequest(ApiCardCreateRequest request)
        {
            var validationContext = new ValidationContext(request, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(request, validationContext, validationResults, validateAllProperties: true);

            if (isValid)
                return new List<string>();

            return validationResults.Select(vr => vr.ErrorMessage).ToList();
        }

        private async Task CreateCardAsync(ApiCardCreateRequest request)
        {
            await _cardCreateUseCase.CreateCardAsync(request);
        }

    }
}

