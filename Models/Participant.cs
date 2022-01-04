using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestArea.Models
{
    internal class Participant
    {
        //public int Id { get; set; }
        internal string Name { get; set; } = "";
        internal string Last { get; set; } = "";
        internal string Number { get; set; } = "";

        internal string FullName => $"{Name} {Last}";
    }
}
