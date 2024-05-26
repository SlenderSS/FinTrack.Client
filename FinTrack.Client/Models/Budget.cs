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
        public decimal PlannedAmountOfMoney { get; set; }
        public decimal TotalAmountOfMoney { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime CreationDate { get; set; }
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
    }


}
