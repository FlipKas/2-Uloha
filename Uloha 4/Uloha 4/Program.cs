using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Uloha_4
{
    internal class Program
    {
        static void Main()
        {
        start:

            Console.WriteLine("Zadejte posloupnost čísel");

            List<int> numbers = new List<int>();
            string input;

            while ((input = Console.ReadLine()) != null)
            {
                if (int.TryParse(input, out int number))
                {
                    numbers.Add(number);
                }
                else
                {
                    Console.WriteLine("Chyba: Vstup obsahuje neplatné celé číslo.");
                    Console.ReadKey();
                    Console.Clear();
                    goto start;
                }
            }

            if (numbers.Count == 0)
            {
                Console.WriteLine("Chyba: Vstupní posloupnost je prázdná.");
                Console.ReadKey();
                Console.Clear();
                goto start;
            }

            if (numbers.Count > 2000)
            {
                Console.WriteLine("Chyba: Vstupní posloupnost je příliš dlouhá.");
                Console.ReadKey();
                Console.Clear();
                goto start;
            }

            Dictionary<int, List<(int, int)>> sumIntervals = new Dictionary<int, List<(int, int)>>();

            int intervalCount = 0;

            for (int i = 0; i < numbers.Count; i++)
            {
                int sum = 0;
                for (int j = i; j < numbers.Count; j++)
                {
                    sum += numbers[j];
                    if (j > i)
                    {
                        intervalCount++;
                        if (!sumIntervals.ContainsKey(sum))
                        {
                            sumIntervals[sum] = new List<(int, int)>();
                        }
                        sumIntervals[sum].Add((i, j));
                    }
                }
            }

            Console.WriteLine($"Počet možných intervalů délky minimálně dvě čísla: {intervalCount}");

            int pairsCount = 0;

            foreach (var intervalList in sumIntervals.Values)
            {
                int count = intervalList.Count;
                if (count > 1)
                {
                    pairsCount += (count * (count - 1)) / 2;
                }
            }

            Console.WriteLine($"Počet dvojic intervalů se stejným součtem: {pairsCount}");
            Console.ReadLine();
        }
    }
}