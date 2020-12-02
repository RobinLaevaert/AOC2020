using System.Collections.Generic;
using System.IO;


namespace Shared
{
    public static class Helpers
    {
        public static IEnumerable<string> ReadAllLines(this StreamReader reader)
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                yield return line;
            }
        }
    }
}
