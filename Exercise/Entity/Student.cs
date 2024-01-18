using Exercise1.Enum;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise1.Entity
{
    [Table("Student")]
    public class Student : Entity
    {
        public string? StudentCode { get; set; }
        public string? SchoolYearId { get; set;}
        [ForeignKey("SchoolYearId")]
        public SchoolYear? SchoolYear { get; set; }
        public StatusStudent StatusStudent { get; set; }
    }
}
