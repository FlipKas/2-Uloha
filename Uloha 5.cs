using System;
using System.Collections.Generic;
using System.Globalization;

class Airplane
{
    public double X { get; set; }
    public double Y { get; set; }
    public string Name { get; set; }

    public Airplane(double x, double y, string name)
    {
        X = x;
        Y = y;
        Name = name;
    }
}

class Program
{
    static void Main()
    {
        var airplanes = new List<Airplane>();
        string input;
        Console.WriteLine("Souřadnice a označení letadel (pro ukončení zadejte 'END'):");

        while ((input = Console.ReadLine()) != null && input != "END")
        {
            if (string.IsNullOrWhiteSpace(input)) continue;

            var parts = input.Split(':');
            if (parts.Length != 2)
            {
                Console.WriteLine("Nespravny vstup.");
                continue;
            }

            var coordinates = parts[0].Split(',');
            if (coordinates.Length != 2)
            {
                Console.WriteLine("Nespravny vstup.");
                continue;
            }

            if (!double.TryParse(coordinates[0], NumberStyles.Float, CultureInfo.InvariantCulture, out double x) ||
                !double.TryParse(coordinates[1], NumberStyles.Float, CultureInfo.InvariantCulture, out double y))
            {
                Console.WriteLine("Nespravny vstup.");
                continue;
            }

            var name = parts[1].Trim();
            if (name.Length == 0 || name.Length > 199) continue;

            airplanes.Add(new Airplane(x, y, name));
        }

        if (airplanes.Count < 2)
        {
            Console.WriteLine("Nedostatečný počet letadel na výpočet.");
            Console.ReadKey();
            return;
        }

        double minDistance = double.MaxValue;
        var pairs = new List<Tuple<Airplane, Airplane>>();

        for (int i = 0; i < airplanes.Count; i++)
        {
            for (int j = i + 1; j < airplanes.Count; j++)
            {
                var dist = Distance(airplanes[i], airplanes[j]);
                if (dist < minDistance)
                {
                    minDistance = dist;
                    pairs.Clear();
                    pairs.Add(new Tuple<Airplane, Airplane>(airplanes[i], airplanes[j]));
                }
                else if (dist == minDistance)
                {
                    pairs.Add(new Tuple<Airplane, Airplane>(airplanes[i], airplanes[j]));
                }
            }
        }

        Console.WriteLine($"Vzdalenost nejblizsich letadel: {minDistance.ToString("F6", CultureInfo.InvariantCulture)}");
        Console.WriteLine($"Nalezenych dvojic: {pairs.Count}");
        foreach (var pair in pairs)
        {
            Console.WriteLine($"{pair.Item1.Name} - {pair.Item2.Name}");
        }

        Console.ReadKey();
    }

    static double Distance(Airplane a1, Airplane a2)
    {
        return Math.Sqrt(Math.Pow(a1.X - a2.X, 2) + Math.Pow(a1.Y - a2.Y, 2));
    }
}

// pouzito ChatGpt