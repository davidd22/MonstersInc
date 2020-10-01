using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MonstersIncDomain
{
    public class IntimidatorIntimidation
    {
        [Key]
        public int Id { get; set; }
        public int IntimidatorId { get; set; }

        public int DoorId { get; set; }
        public DateTime Day { get; set; }
        public bool Depleted { get; set; }
    }
}
