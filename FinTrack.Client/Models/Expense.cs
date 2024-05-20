using FinTrack.Client.Models.Base;

namespace FinTrack.Client.Models
{
    public class Expense : BaseModel
    {
        public required string Description { get; set; }
        public required decimal IncomeVolume { get; set; }
        public required DateTime IncomeDate { get; set; }
        public required int IncomeCategoryId { get; set; }
        public required ExpenseCategory IncomeCategory { get; set; }
    }


}
