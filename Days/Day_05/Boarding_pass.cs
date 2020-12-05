namespace Days
{
    public class Boarding_pass
    {
        public Boarding_pass(string row_instruction, string column_instruction)
        {
            Row_instruction = row_instruction;
            Column_instruction = column_instruction;
        }
        public string Row_instruction { get; set; }
        public string Column_instruction { get; set; }

        public int Row { get; set; }
        public int Column { get; set; }
        public int Seat_id => (Row * 8) + Column;
    }
}
