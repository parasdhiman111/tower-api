using System;
using credit_work_app.Models.Exceptions;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using tower_api.Business.Interfaces;
using tower_api.Business.Models;
using tower_api.Controllers;
using tower_api.Models.Responses;

namespace tower_api_test.Controllers
{
    [TestFixture]
    public class CardControllerTests
	{
        // Mock the ICategoryService
        private ICardsGetUseCase CardsGetUseCaseMock { get; set; }
        private CardController Controller { get; set; }

        [SetUp]
        public void Setup()
        {
            CardsGetUseCaseMock = Substitute.For<ICardsGetUseCase>();
            Controller = new CardController(CardsGetUseCaseMock,null, null);
        }

        [Test]
        public async Task Given_UseCase_Throws_Exception_When_Call_GetCardsAsync_Then_Throw_Error()
        {
            SetupCardsController(new NoCard());
            var response = await Controller.GetCardsAsync();
            ApiError? error = VerifyError(response);
            error.Code.Should().Be("No_Card");
        }

        [Test]
        public async Task Given_No_Cards_When_Call_GetCardsAsync_Then_Return_Empty_List_Of_Cards()
        {
            SetupCardController(new List<Card>());
            var response = await Controller.GetCardsAsync();
            VerifyHttpOk<IEnumerable<ApiCardResponse>>(response);
            await VerifyUseCaseReceivedCall();
        }

        [Test]
        public async Task Given_Cards_When_Call_GetCardsAsync_Then_Return_List_Of_Cards()
        {

            SetupCardController(MockGetCards());
            var cardsResponse = await Controller.GetCardsAsync();
            var cards = VerifyHttpOk<IEnumerable<ApiCardResponse>>(cardsResponse);
            await VerifyUseCaseReceivedCall();
            cards.Count().Should().BeGreaterThan(0);
        }


        // TODO: More tests goes here for full coverage. for assignment purpoese only one added



        // private

        private void SetupCardsController(Exception exception)
        {
            CardsGetUseCaseMock.GetCardsAsync().Throws(exception);
        }

        private void SetupCardController(List<Card> cards)
        {
            CardsGetUseCaseMock.GetCardsAsync().Returns(cards);
        }

        private static List<Card> MockGetCards()
        {
            return new List<Card>
            {
                new Card { Id= 1, Name = "User1", CreditCard= "1234567890123456", CVC = "123" , ExpiryDate= "08/24"  },
                new Card { Id= 2, Name = "User2", CreditCard= "1234567890123456", CVC = "456" , ExpiryDate= "12/29"   },
            };
        }

        private async Task VerifyUseCaseReceivedCall()
        {
            await CardsGetUseCaseMock.Received(1).GetCardsAsync();
        }

        private static T VerifyHttpOk<T>(IActionResult response)
        {
            response.Should().BeOfType<OkObjectResult>();
            var result = response as OkObjectResult;
            return (T)result.Value;
        }

        private static ApiError? VerifyError(IActionResult response)
        {
            response.Should().BeOfType<ObjectResult>();
            var result = response as ObjectResult;
            var error = result.Value as ApiError;
            return error;
        }
    }
}

