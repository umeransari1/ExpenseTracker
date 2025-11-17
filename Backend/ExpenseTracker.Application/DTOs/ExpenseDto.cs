using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.DTOs
{
    public class ExpenseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Amount { get; set; }

        public int CategoryId { get; set; }   // FK reference
        public string CategoryName { get; set; } = string.Empty;

        public DateTime Date { get; set; }
    }
}
