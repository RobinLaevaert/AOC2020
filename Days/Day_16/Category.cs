using System;
using System.Collections.Generic;

namespace Days
{
    public class Category
    {
        public string Name { get; set; }
        public List<Tuple<int,int>> Ranges { get; set; }
    }
}