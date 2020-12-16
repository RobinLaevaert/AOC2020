using System;
using System.Collections.Generic;

namespace Days
{
    public class Ticket_type
    {
        public string ticket_type { get; set; }
        public List<Tuple<int,int>> Ranges { get; set; }
    }
}