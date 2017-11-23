using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BPopAPI.Models
{
    public class QandA
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string ChoiceA { get; set; }
        public string ChoiceB { get; set; }
        public string ChoiceC { get; set; }
        public string ChoiceD { get; set; }
        public string Answer { get; set; }
    }
}