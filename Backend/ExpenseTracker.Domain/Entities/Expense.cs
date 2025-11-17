using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain.Entities
{
    public class Expense
    {
        public int Id { get; set; }
        public string RowId { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string UserId { get; set; } = string.Empty;
        public User? User { get; set; }

        public int CategoryId { get; set; }     // FK
        public Category? Category { get; set; } // Navigation
    }
}
