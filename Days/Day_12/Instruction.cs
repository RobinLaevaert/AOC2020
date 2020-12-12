using System;
using System.Linq;

namespace Days
{
    public class Instruction
    {
        private Instruction(){}
        public static Instruction Create_from_input_string(string input)
        {
            Direction direction;
            if (Enum.TryParse(input.First().ToString(), out direction))
            {
                return new Instruction()
                {
                    Direction = direction,
                    Steps = int.Parse(new string(input[1..]))
                };
            }
            throw new Exception("Should not happen");
        }
        public Direction Direction { get; set; }
        public int Steps { get; set; }
    }

}
