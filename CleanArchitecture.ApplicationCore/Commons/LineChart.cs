using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.ApplicationCore.Commons
{
    public class LineChart
    {
        public List<ChartData> Series { get; set; }
        public string[] Categories { get; set; }
    }
    public class ChartData
    {
        public string Name { get; set; }
        public int[] Data { get; set; }
    }
}
