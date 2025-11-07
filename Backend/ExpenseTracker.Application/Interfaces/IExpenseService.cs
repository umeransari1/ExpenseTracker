using ExpenseTracker.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.Interfaces
{
    public interface IExpenseService
    {
        Task<IEnumerable<ExpenseDto>> GetAllForUserAsync(string userId);
        Task<ExpenseDto?> GetByIdForUserAsync(string userId, int id);
        Task<ExpenseDto> AddExpenseForUserAsync(string userId, ExpenseDto dto);
        Task UpdateExpenseForUserAsync(string userId, ExpenseDto dto);
        Task DeleteExpenseForUserAsync(string userId, int id);
    }
}
