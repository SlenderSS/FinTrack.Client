using FinTrack.Client.MVVM.Models.Base;

namespace FinTrack.Client.MVVM.Models
{
    internal class Expense : BaseModel
    {
        public required string Description { get; set; }
        public required decimal IncomeVolume { get; set; }
        public required DateTime IncomeDate { get; set; }
        public required int IncomeCategoryId { get; set; }
        public required ExpenseCategory IncomeCategory { get; set; }
    }


}
