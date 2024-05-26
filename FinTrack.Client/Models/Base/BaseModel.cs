using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Client.Models.Base
{
    public class BaseModel : ObservableObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
