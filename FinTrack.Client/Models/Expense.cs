using FinTrack.Client.Models.Base;

namespace FinTrack.Client.Models
{
    public class Expense : BaseModel
    {
        public string Description { get; set; }
        public decimal ExpenseVolume { get; set; }
        public DateTime ExpenseDate { get; set; }
        public int ExpenseCategoryId { get; set; }
        public ExpenseCategory ExpenseCategory { get; set; }
    }


}
