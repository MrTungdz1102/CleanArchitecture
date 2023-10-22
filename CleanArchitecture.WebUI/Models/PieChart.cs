using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.WebUI.Models
{
    public class PieChart
    {
        public decimal[] Series { get; set; }
        public string[] Labels { get; set; }
    }
}
