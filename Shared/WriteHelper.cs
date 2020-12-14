using System;
using System.Collections.Generic;

namespace Shared
{
    public static class WriteHelper
    {
        public static void PrintInfos(List<string> infos)
        {
            for (var ctr = 0; ctr < Math.Ceiling((double) infos.Count / 2); ctr++)
            {
                Console.WriteLine("{0,-30} {1,-30}", infos[ctr],
                    Math.Ceiling((double) infos.Count / 2) + ctr < infos.Count
                        ? infos[(Index) (Math.Ceiling((double) infos.Count / 2) + ctr)]
                        : "");
            }
        }
    }
}