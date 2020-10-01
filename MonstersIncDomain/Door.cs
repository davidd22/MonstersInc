using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MonstersIncDomain
{
    public class Door
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public float Energy { get; set; }
    }
}
