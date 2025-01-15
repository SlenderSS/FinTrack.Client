using FinTrack.Client.Models.Base;

namespace FinTrack.Client.Models
{
    public class IncomeCategory : BaseModel
    {
        private User user;

        public User User { get => user; set => SetProperty(ref user, value); }
    }


}
