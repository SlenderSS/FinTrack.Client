using FinTrack.Client.MVVM.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Client.MVVM.Models
{
    internal class Budget : BaseModel
    {
        public required decimal AmountOfMoney { get; set; }
        public required int UserId { get; set; }
        public required User User { get; set; }
        public required DateTime CreationDate { get; set; }
        public required int CurrencyId { get; set; }
        public required Currency Currency { get; set; }
    }


}
