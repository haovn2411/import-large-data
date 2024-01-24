using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise1.Entity
{
    [Table("Score")]
    public class Score : Entity
    {
        public string student_id { get; set; }
        public string? province { get; set; }
        public double? mathematics { get; set; }

        public double? literature { get; set; }

        public double? physics { get; set; }

        public double? chemistry { get; set; }

        public double? biology { get; set; }
        public double? history { get; set; }

        public double? geography { get; set; }
        public double? civic_education { get; set; }

        public double? english { get; set; }
        public int? year { get; set; }
    }
}
