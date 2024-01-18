using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise1.Entity
{
    public class Entity
    {
        [Key]
        public string Id { get; set; }
        public Entity () 
        {
            Id = Guid.NewGuid ().ToString ("N");
        }
    }
}
