using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Nihon.Utility
{
    public class Utilities
    {
        public static void Write(string Text, bool SwitchLines = false)
        {
            Console.ForegroundColor = ConsoleColor.White;
            string Content = $"  {Text}\n";

            for (int l = 0; l < Content.Length; l++)
            {
                Thread.Sleep(20);
                Console.Write(Content[l]);
            }
        }
    }
}
