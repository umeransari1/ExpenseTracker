using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string RowId { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;

        // One category → many expenses
        public ICollection<Expense>? Expenses { get; set; }
    }
}
