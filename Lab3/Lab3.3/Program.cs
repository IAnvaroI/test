using System;
using System.IO;
using System.Threading.Tasks;

namespace Lab3._3
{
    class Program
    {
        private const string filename_pattern = "datafile";

        static void Main(string[] args)
        {
            Console.WriteLine("Enter \'+\' to write one another file.");

            string input = Console.ReadLine();
            int i = 0;
            while (input != "#")
            {
                if (input == "+" && i < 2)
                {
                    WriteFile(filename_pattern + i + ".txt");
                    Console.WriteLine("Start writing in the file \"" + filename_pattern + i + ".txt\"");
                    ++i;
                }
                else
                {
                    Console.WriteLine(input);
                }

                input = Console.ReadLine();
            }
        }

        private static async void WriteFile(string filename)
        {
            
            string tmp = "Tmp string for task number 3.";

            for (int i = 0; i < 25; i++)
            {
                FileStream inputStream = new FileStream(filename, FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(inputStream);

                sw.WriteLine(tmp);
                await Task.Delay(1000);

                sw.Close();
                inputStream.Close();
            }

            Console.WriteLine("End writing in the file \"" + filename + "\"");
        }
    }
}
