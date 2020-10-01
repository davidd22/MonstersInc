using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MonstersIncDomain
{
    public class Log
    {
        [Key]
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string RequestMethod { get; set; }
        public string RequestPath { get; set; }
        public int ResponseStatusCode { get; set; }

        public string ErrorMsg { get; set; }
        public string StackTrace { get; set; }
    }
}
