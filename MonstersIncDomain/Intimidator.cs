using System;
using System.ComponentModel.DataAnnotations;

namespace MonstersIncDomain
{
    public class Intimidator
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int TentaclesNumber { get; set; }
        public DateTime StartToScareData { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
