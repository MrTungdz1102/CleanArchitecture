using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.WebUI.Models
{
    public class RadialBarChart
    {
        public decimal TotalCount { get; set; }
        public decimal CountInCurrentMonth { get; set; }
        public bool HasRatioIncreased { get; set; }
        public int[] Series { get; set; }
    }
}
