using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise1.Entity
{
    [Table("Subject")]
    public class Subject : Entity
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
    }
}
