using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonstersInc.Models
{
    public class IntimidatorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public int TentaclesNumber { get; set; }
        public DateTime StartToScareData { get; set; }
        public string ClientSecret { get; set; }
    }
}
