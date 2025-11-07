using ExpenseTracker.Application.DTOs;
using ExpenseTracker.Application.Interfaces;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IGenericRepository<Expense> _repo;

        public ExpenseService(IGenericRepository<Expense> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ExpenseDto>> GetAllForUserAsync(string userId)
        {
            var all = await _repo.GetAllAsync();
            var userExpenses = all.Where(e => e.UserId == userId);
            return userExpenses.Select(e => new ExpenseDto
            {
                Id = e.Id,
                Title = e.Title,
                Amount = e.Amount,
                Category = e.Category,
                Date = e.Date
            });
        }

        public async Task<ExpenseDto?> GetByIdForUserAsync(string userId, int id)
        {
            var e = await _repo.GetByIdAsync(id);
            if (e == null || e.UserId != userId) return null;
            return new ExpenseDto { Id = e.Id, Title = e.Title, Amount = e.Amount, Category = e.Category, Date = e.Date };
        }

        public async Task<ExpenseDto> AddExpenseForUserAsync(string userId, ExpenseDto dto)
        {
            var expense = new Expense
            {
                Title = dto.Title,
                Amount = dto.Amount,
                Category = dto.Category,
                Date = dto.Date,
                UserId = userId
            };
            await _repo.AddAsync(expense);
            await _repo.SaveChangesAsync();
            dto.Id = expense.Id;
            return dto;
        }

        public async Task UpdateExpenseForUserAsync(string userId, ExpenseDto dto)
        {
            var expense = await _repo.GetByIdAsync(dto.Id);
            if (expense == null || expense.UserId != userId) throw new Exception("Not found or access denied");

            expense.Title = dto.Title;
            expense.Amount = dto.Amount;
            expense.Category = dto.Category;
            expense.Date = dto.Date;

            _repo.Update(expense);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteExpenseForUserAsync(string userId, int id)
        {
            var expense = await _repo.GetByIdAsync(id);
            if (expense == null || expense.UserId != userId) throw new Exception("Not found or access denied");
            _repo.Delete(expense);
            await _repo.SaveChangesAsync();
        }
    }
}
