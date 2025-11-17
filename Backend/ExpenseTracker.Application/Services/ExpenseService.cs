using ExpenseTracker.Application.DTOs;
using ExpenseTracker.Application.Interfaces;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
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
        private readonly IGenericRepository<Category> _catRepo;

        public ExpenseService(IGenericRepository<Expense> repo, IGenericRepository<Category> catRepo)
        {
            _repo = repo;
            _catRepo = catRepo;
        }

        public async Task<IEnumerable<ExpenseDto>> GetAllForUserAsync(string userId)
        {
            var all = await _repo.Query()
                .Include(e => e.Category)
                .Where(e => e.UserId == userId)
                .ToListAsync();

            return all.Select(e => new ExpenseDto
            {
                Id = e.Id,
                Title = e.Title,
                Amount = e.Amount,
                CategoryId = e.CategoryId,
                CategoryName = e.Category?.Name ?? "",
                Date = e.Date
            });
        }

        public async Task<ExpenseDto?> GetByIdForUserAsync(string userId, int id)
        {
            var e = await _repo.Query().Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);

            if (e == null || e.UserId != userId) return null;

            return new ExpenseDto {
                Id = e.Id,
                Title = e.Title,
                Amount = e.Amount,
                CategoryId = e.CategoryId,
                CategoryName = e.Category?.Name ?? "",
                Date = e.Date
            };
        }

        public async Task<ExpenseDto> AddExpenseForUserAsync(string userId, ExpenseDto dto)
        {
            var catExists = await _catRepo.GetByIdAsync(dto.CategoryId);
            
            if (catExists == null)
                throw new Exception("Category does not exist");

            var expense = new Expense
            {
                Title = dto.Title,
                Amount = dto.Amount,
                CategoryId = dto.CategoryId,
                Date = dto.Date,
                UserId = userId
            };
            
            await _repo.AddAsync(expense);
            await _repo.SaveChangesAsync();
            
            dto.Id = expense.Id;
            dto.CategoryName = catExists.Name;

            return dto;
        }

        public async Task UpdateExpenseForUserAsync(string userId, ExpenseDto dto)
        {
            var expense = await _repo.GetByIdAsync(dto.Id);
            
            if (expense == null || expense.UserId != userId) 
                throw new Exception("Not found or access denied");

            // validate category exists
            var catExists = await _catRepo.GetByIdAsync(dto.CategoryId);
            if (catExists == null)
                throw new Exception("Category does not exist");

            expense.Title = dto.Title;
            expense.Amount = dto.Amount;
            expense.CategoryId = dto.CategoryId;
            expense.Date = dto.Date;

            _repo.Update(expense);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteExpenseForUserAsync(string userId, int id)
        {
            var expense = await _repo.GetByIdAsync(id);
            
            if (expense == null || expense.UserId != userId) 
                throw new Exception("Not found or access denied");
            
            _repo.Delete(expense);
            await _repo.SaveChangesAsync();
        }
    }
}
