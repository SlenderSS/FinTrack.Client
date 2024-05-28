using FinTrack.Client.Models.Base;

namespace FinTrack.Client.Models
{
    public class Income : BaseModel
    {
        public string Description { get; set; }
        public decimal IncomeVolume { get; set; }
        public DateTime IncomeDate { get; set; }
        public int IncomeCategoryId { get; set; }
        public IncomeCategory IncomeCategory { get; set; }
    }


}
