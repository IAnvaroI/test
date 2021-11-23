using System;

namespace Lab3._2_sync
{
    class Program
    {
        static void Main(string[] args)
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
