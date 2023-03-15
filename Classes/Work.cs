using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Work
    {
        public int? Work_ID { get; set; }
        public int? Worker_ID { get; set; }
        public int? WorkType_ID { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public int Reward { get; set; }

    }
}
