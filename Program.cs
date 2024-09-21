using System;
using System.Linq;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
                          11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };

        int elementsPerThread = 10;
        int numberOfThreads = (int)Math.Ceiling((double)numbers.Length / elementsPerThread);

        Task<int>[] tasks = new Task<int>[numberOfThreads];

        for (int i = 0; i < numberOfThreads; i++)
        {
            int start = i * elementsPerThread;
            int end = Math.Min(start + elementsPerThread, numbers.Length);

            tasks[i] = Task.Run(() =>
            {
                int sum = 0;
                for (int j = start; j < end; j++)
                {
                    sum += numbers[j];
                }
                return sum;
            });
        }

        Task.WaitAll(tasks);
        int totalSum = tasks.Sum(t => t.Result);

        Console.WriteLine($"Итоговая сумма элементов массива: {totalSum}");
    }
}
