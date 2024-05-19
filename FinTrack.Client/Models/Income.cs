using FinTrack.Client.MVVM.Models.Base;

namespace FinTrack.Client.MVVM.Models
{
    internal class Income : BaseModel
    {
        public required string Description { get; set; }
        public required decimal IncomeVolume { get; set; }
        public required DateTime IncomeDate { get; set; }
        public required int IncomeCategoryId { get; set; }
        public required IncomeCategory IncomeCategory { get; set; }
    }


}
