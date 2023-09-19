using System;
using credit_work_app.Data;
using Microsoft.EntityFrameworkCore;
using tower_api.Repositories.Interfaces;
using tower_api.Repositories.Models;

namespace tower_api.Repositories.Implementations
{
    public class CardRepository : ICardRepository
    {
        private readonly ApplicationDbContext _context;

        public CardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Card>> GetAllCardsAsync()
        {
            return await _context.Cards.ToListAsync();
        }

        public async Task CreateCardAsync(Card card)
        {
            await _context.Cards.AddAsync(card);

            _context.SaveChanges();
        }

        public async Task<Card> GetCardByNumberAsync(string cardNumber)
        {
            return await _context.Cards.FirstOrDefaultAsync(card => card.CreditCard.Equals(cardNumber));

        }
    }
}

