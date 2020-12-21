using System;
using System.Collections.Generic;
using System.Text;

namespace DSM.EntityModels
{
    public class ReportsEntity
    {
        public class PieChart
        {
            public string[] labels { get; set; }
            public int[] values { get; set; }
        }

        public class ActualVsExpected
        {
            public string[] labels { get; set; }
            public List<BarCharData> datas { get; set; }
        }

        public class BarCharData
        {
            public decimal[] data { get; set; }
            public string label { get; set; }
        }

        public class LineGraphTable
        {
            public string estimatedL1 { get; set; }
            public string estimatedL2 { get; set; }
            public string estimatedAverage { get; set; }
            public string actualL1 { get; set; }
            public string actualL2 { get; set; }
            public string actualAverage { get; set; }
        }
    }
}
