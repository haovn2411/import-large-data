using Exercise1.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise1.Entity
{
    [Table("SchoolYear")]
    public class SchoolYear : Entity
    {
        public string? Name { get; set; }
        public string? ExamYear { get; set; }
        public StatusSchoolYear? StatusSchoolYear { get; set; }
    }
}
