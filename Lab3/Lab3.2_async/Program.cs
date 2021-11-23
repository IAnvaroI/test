using System;
using System.Threading.Tasks;

namespace Lab3._2_async
{
    class Program
    {
        static async Task Main(string[] args)
        {
            for (int i = 0; i < 20; i++)
            {
                if (i % 2 == 0)
                {
                    WriteFilledLine(120, '/');
                }
                else
                {
                    WriteFilledLine(120, '\\');
                }

            }
        }
        static void WriteFilledLine(int length, char ch)
        {
            for (int i = 1; i < length; i++)
            {
                Console.Write(ch);
            }
            Console.Write('\n');
        }
    }
}
