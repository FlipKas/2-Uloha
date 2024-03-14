using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PVA_uloha3_Pech
{
    internal class Program
    {
        public static bool IsPalindrome(long num, int radix)
        {
            long reversed = 0;
            long starting = num;

            while (num > 0)
            {
                reversed = reversed * (long)radix + num % (long)radix;
                num /= (long)radix;
            }

            return reversed == starting;
        }

        public static bool NextPal(long from, int radix, out long next)
        {
            next = 0;

            if (radix < 2 || radix > 36)
            {
                Console.WriteLine("neplatná číselná soustava.");
                return false;
            }

            long current = from + 1; //hledání dalšího čísla

            while (current > from)
            {
                if (IsPalindrome(current, radix))
                {
                    next = current;
                    return true;
                }
                current++;
            }
            Console.WriteLine("Palindrom není větší než definované číslo.");
            return false;
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("základní číslo.");
            long from = Int64.Parse(Console.ReadLine());
            Console.WriteLine("Číselná soustava pro Palindrom");
            int radix = int.Parse(Console.ReadLine());
            long next;

            if (NextPal(from, radix, out next))
            {
                Console.WriteLine($"Palindrom v soustavě {radix} větší než {from}: {next}");
            }
            else
            {
                Console.WriteLine($"Palindrom v soustavě {radix} není větší než {from}.");
            }
            Console.ReadKey();
        }
    }
}