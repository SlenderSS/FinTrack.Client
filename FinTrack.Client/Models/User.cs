using FinTrack.Client.Models.Base;

namespace FinTrack.Client.Models
{
    public class User : BaseModel
    {
        private string password;

        public string Password { get => password; set => SetProperty(ref password, value); }
    }


}
