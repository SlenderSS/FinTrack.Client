using FinTrack.Client.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Client.Models
{
    public class Budget : BaseModel
    {
        public required decimal PlannedAmountOfMoney { get; set; }
        public required decimal TotalAmountOfMoney { get; set; }
        public required int UserId { get; set; }
        public required User User { get; set; }
        public required DateTime CreationDate { get; set; }
        public required int CurrencyId { get; set; }
        public required Currency Currency { get; set; }
    }


}
