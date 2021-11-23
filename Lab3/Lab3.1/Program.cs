using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Lab3
{
    class Program
    {
        private const int ArraySize = 1000000;
        private const int ThreadsCountArraySize = 4;
        private const int MinElementValue = 1;
        private const int MaxElementValue = 50;

        public struct Plinq
        {
            private int begin;
            private int end;

            public int Begin { get => begin; set => begin = value; }
            public int End { get => end; set => end = value; }
        }

        public struct AllOperationsResult
        {
            private double sum;
            private double average;
            private KeyValuePair<double, int> moda;

            public AllOperationsResult(double sum, double average, KeyValuePair<double, int> moda)
            {
                this.sum = sum;
                this.average = average;
                this.moda = moda;
            }
            public double Sum { get => sum; set => sum = value; }
            public double Average { get => average; set => average = value; }
            public KeyValuePair<double, int> Moda { get => moda; set => moda = value; }
        }

        static void Main(string[] args)
        {
            int[] ThreadsCount = { 1, 2, 4, 10 };

            Console.WriteLine("                   |     " 
                              + ThreadsCount[0] + " Threads     |     "
                              + ThreadsCount[1] + " Threads     |     "
                              + ThreadsCount[2] + " Threads     |     "
                              + ThreadsCount[3] + " Threads    |");
            WriteFilledLine(120, '-');
            
            TaskObjectsResult(ThreadsCount);
            WriteFilledLine(120, '-');
            
            PlinqResult(ThreadsCount);
            WriteFilledLine(120, '-');
            
            ThreadPoolResult(ThreadsCount);
            WriteFilledLine(120, '-');
            
            ParallelResult(ThreadsCount);
            WriteFilledLine(120, '-');
        }

        private static void TaskObjectsResult(int[] threadsCount)
        {
            Stopwatch stopwatch = new Stopwatch();
            string result = "Task objects result| ";

            for (int i = 0; i < ThreadsCountArraySize; i++)
            {
                List<Task< AllOperationsResult >> TaskList = new();
                int elementsCount = ArraySize / threadsCount[i];
                int begin = i * elementsCount;
                int end = (i + 1) * elementsCount - 1;
                for (int j = 0; j < threadsCount[i]; j++)
                {
                    TaskList.Add(new(() => DoAllOperations(begin, end)));
                }

                stopwatch.Start();
                TaskList.ForEach(task => task.Start());
                Task.WaitAll(TaskList.ToArray());

                List<AllOperationsResult> ResultsList = new();
                Dictionary<double, int> ModaGlobalDict = new();
                double FullSum = 0;
                double FullAverage = 0;
                double FullModa = 0;

                TaskList.ForEach(task => ResultsList.Add(task.Result));
                foreach (var resultExample in ResultsList)
                {
                    FullSum += resultExample.Sum;
                    FullAverage += resultExample.Average;
                    if (ModaGlobalDict.ContainsKey(resultExample.Moda.Key))
                    {
                        ModaGlobalDict[resultExample.Moda.Key] += resultExample.Moda.Value;
                    }
                    else
                    {
                        ModaGlobalDict.Add(resultExample.Moda.Key, resultExample.Moda.Value);
                    }
                }
                FullModa = ModaGlobalDict.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
                FullAverage /= ModaGlobalDict.Count;

                stopwatch.Stop();

                //Console.WriteLine("Sum: {0}, Average: {1:0.01}, Mode: {2}", FullSum, FullAverage, FullModa);

                result += stopwatch.Elapsed + "  | ";
                stopwatch.Reset();
            }

            Console.WriteLine(result);
        }

        private static void PlinqResult(int[] threadsCount)
        {
            Stopwatch stopwatch = new Stopwatch();
            string result = "PLINQ result       | ";

            Plinq[][] plinqs = { new Plinq[threadsCount[0]], new Plinq[threadsCount[1]], new Plinq[threadsCount[2]], new Plinq[threadsCount[3]] };

            for (int i = 0; i < ThreadsCountArraySize; i++)
            {
                int elementsCount = ArraySize / threadsCount[i];
                int begin = i * elementsCount;
                int end = (i + 1) * elementsCount - 1;
                for (int j = 0; j < threadsCount[i]; j++)
                {
                    plinqs[i][j] = new Plinq {Begin = begin, End = end};
                }
            }
            for (int i = 0; i < ThreadsCountArraySize; i++)
            {
                stopwatch.Start();
                var ThreadsResults = plinqs[i].AsParallel().WithDegreeOfParallelism(threadsCount[i])
                                    .Select(plinqInstance => DoAllOperations(plinqInstance.Begin, plinqInstance.End));

                List<AllOperationsResult> ResultsList = new(ThreadsResults.ToList());
                Dictionary<double, int> ModaGlobalDict = new();
                double FullSum = 0;
                double FullAverage = 0;
                double FullModa = 0;

                foreach (var resultExample in ResultsList)
                {
                    FullSum += resultExample.Sum;
                    FullAverage += resultExample.Average;
                    if (ModaGlobalDict.ContainsKey(resultExample.Moda.Key))
                    {
                        ModaGlobalDict[resultExample.Moda.Key] += resultExample.Moda.Value;
                    }
                    else
                    {
                        ModaGlobalDict.Add(resultExample.Moda.Key, resultExample.Moda.Value);
                    }
                }
                FullModa = ModaGlobalDict.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
                FullAverage /= ModaGlobalDict.Count;

                stopwatch.Stop();

                //Console.WriteLine("Sum: {0}, Average: {1:0.01}, Mode: {2}", FullSum, FullAverage, FullModa);

                result += stopwatch.Elapsed + "  | ";
                stopwatch.Reset();
            }


            Console.WriteLine(result);
        }

        private static void ThreadPoolResult(int[] threadsCount)
        {
            Stopwatch stopwatch = new Stopwatch();
            string result = "ThreadPool result  | ";

            
            for (int i = 0; i < ThreadsCountArraySize; i++)
            {
                List<AllOperationsResult> ResultsList = new();
                
                int elementsCount = ArraySize / threadsCount[i];
                int begin = i * elementsCount;
                int end = (i + 1) * elementsCount - 1;
                
                stopwatch.Start();
                for (int j = 0; j < threadsCount[i]; j++)
                {
                    ThreadPool.QueueUserWorkItem(x => ResultsList.Add(DoAllOperations(begin, end)));
                }

                int workerThreads = 0;
                int completionPortThreads = 0;
                int maxWorkerThreads = 0;
                int maxCompletionPortThreads = 0;
                ThreadPool.GetMaxThreads(out maxWorkerThreads, out maxCompletionPortThreads);
                while (true)
                {
                    ThreadPool.GetAvailableThreads(out workerThreads, out completionPortThreads);
                    if (workerThreads == maxWorkerThreads)
                    {
                        break;
                    }
                }

                Dictionary<double, int> ModaGlobalDict = new();
                double FullSum = 0;
                double FullAverage = 0;
                double FullModa = 0;

                foreach (var resultExample in ResultsList)
                {
                    FullSum += resultExample.Sum;
                    FullAverage += resultExample.Average;
                    if (ModaGlobalDict.ContainsKey(resultExample.Moda.Key))
                    {
                        ModaGlobalDict[resultExample.Moda.Key] += resultExample.Moda.Value;
                    }
                    else
                    {
                        ModaGlobalDict.Add(resultExample.Moda.Key, resultExample.Moda.Value);
                    }
                }
                FullModa = ModaGlobalDict.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
                FullAverage /= ModaGlobalDict.Count;

                stopwatch.Stop();

                //Console.WriteLine("Sum: {0}, Average: {1:0.01}, Mode: {2}", FullSum, FullAverage, FullModa);

                result += stopwatch.Elapsed + "  | ";
                stopwatch.Reset();
            }

            Console.WriteLine(result);
        }

        private static void ParallelResult(int[] threadsCount)
        {
            Stopwatch stopwatch = new Stopwatch();
            string result = "Parallel result    | ";

            for (int i = 0; i < ThreadsCountArraySize; i++)
            {
                List<AllOperationsResult> ResultsList = new();
                Action[] actions = new Action[threadsCount[i]];

                int elementsCount = ArraySize / threadsCount[i];
                int begin = i * elementsCount;
                int end = (i + 1) * elementsCount - 1;

                for (int j = 0; j < threadsCount[i]; j++)
                {
                    actions[j] = () => ResultsList.Add(DoAllOperations(begin, end));
                }

                stopwatch.Start();
                Parallel.Invoke(new ParallelOptions() { MaxDegreeOfParallelism = threadsCount[i] }, actions);

                Dictionary<double, int> ModaGlobalDict = new();
                double FullSum = 0;
                double FullAverage = 0;
                double FullModa = 0;

                foreach (var resultExample in ResultsList)
                {
                    FullSum += resultExample.Sum;
                    FullAverage += resultExample.Average;
                    if (ModaGlobalDict.ContainsKey(resultExample.Moda.Key))
                    {
                        ModaGlobalDict[resultExample.Moda.Key] += resultExample.Moda.Value;
                    }
                    else
                    {
                        ModaGlobalDict.Add(resultExample.Moda.Key, resultExample.Moda.Value);
                    }
                }
                FullModa = ModaGlobalDict.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
                FullAverage /= ModaGlobalDict.Count;

                stopwatch.Stop();

                //Console.WriteLine("Sum: {0}, Average: {1:0.01}, Mode: {2}", FullSum, FullAverage, FullModa);

                result += stopwatch.Elapsed + "  | ";
                stopwatch.Reset();
            }

            Console.WriteLine(result);
        }
        private static AllOperationsResult DoAllOperations(int begin, int end)
        {
            double[] elements = new double[end - begin];

            GenerateArray(elements);
            double sum = GetSum(elements);
            double average = GetAverage(elements);
            KeyValuePair<double, int> moda = GetMode(elements);

            return new AllOperationsResult(sum, average, moda);
        }

        private static void GenerateArray(double[] array)
        {
            var rand = new Random();
            
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rand.Next(MinElementValue, MaxElementValue);
            }
        }
        
        private static double GetSum(double[] array)
        {
            double sum = 0;
            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i];
            }

            return sum;
        }

        private static double GetAverage(double[] array)
        {
            double sum = 0;
            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i];
            }

            return sum / array.Length;
        }

        private static KeyValuePair<double, int> GetMode(double[] array)
        {
            Dictionary<double, int> UniqueElements = new Dictionary<double, int>();
            for (int i = 0; i < array.Length; i++)
            {
                if (UniqueElements.ContainsKey(array[i]))
                {
                    UniqueElements[array[i]]++;
                }
                else
                {
                    UniqueElements.Add(array[i], 1);
                }
            }

            return UniqueElements.Aggregate((x, y) => x.Value > y.Value ? x : y);
        }

        private static void WriteFilledLine(int length, char symbol)
        {
            for (int i = 0; i < length; i++)
            {
                Console.Write(symbol);
            }
            Console.Write("\n");
        } 
    }
}
