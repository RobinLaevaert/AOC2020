using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Days
{
    public class Password_validation
    {
        public int Lower_index { get; set; }
        public int Upper_index { get; set; }
        public char Character { get; set; }
        public string Password { get; set; }

        public bool Is_valid_part1() 
        {
            var count = Password.Count(x => x == Character);
            return Lower_index <= count && count <= Upper_index;
        }

        public bool Is_valid_part2()
        {
            var password_char_array = Password.ToCharArray();
            return password_char_array[Lower_index - 1] == Character ^ password_char_array[Upper_index - 1] == Character;
        }
    }
}
