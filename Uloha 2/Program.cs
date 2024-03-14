using System;

class Program
{
    struct Point
    {
        public decimal X;
        public decimal Y;
    }

    static void Main(string[] args)
    {
        Point[] points = new Point[3];
        // zkontrolujeme vstupy
        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine($"Bod {((char)('A' + i))}:");
            if (!TryReadPoint(out points[i]))
            {
                Console.WriteLine("Nespravny vstup.");
                return;
            }
        }
        // vypis bodu
        if (CollinearPoints(points))
        {
            Console.WriteLine("Body lezi na jedne primce.");
            Point middlePoint = FindMiddlePoint(points);
            Console.WriteLine($"Prostredni je bod {(char)('A' + Array.IndexOf(points, middlePoint))}.");
        }
        else if (EqualPoints(points))
        {
            Console.WriteLine("Nektere body splyvaji.");
        }
        else
        {
            Console.WriteLine("Body nelezi na jedne primce.");
        }
    }

    static bool TryReadPoint(out Point point)
    {
        point = new Point();
        string input = Console.ReadLine();
        string[] inputs = input.Split(' ');

        if (inputs.Length != 2 || !decimal.TryParse(inputs[0], out point.X) || !decimal.TryParse(inputs[1], out point.Y))
        {
            return false;
        }

        return true;
    }


    static bool EqualPoints(Point[] points)
    {
        // Zda jsou body stejne
        for (int i = 0; i < points.Length - 1; i++)
        {
            for (int j = i + 1; j < points.Length; j++)
            {
                if (points[i].X == points[j].X && points[i].Y == points[j].Y)
                {
                    return true;
                }
            }
        }
        return false;
    }

    static bool CollinearPoints(Point[] points)
    {

        decimal plane = Math.Abs((points[0].X * (points[1].Y - points[2].Y) +
                                points[1].X * (points[2].Y - points[0].Y) +
                                points[2].X * (points[0].Y - points[1].Y)) / 2);

        return Math.Abs(plane) < decimal.Zero;
    }

    static Point FindMiddlePoint(Point[] points)
    {

        if (points[0].X == points[1].X && points[0].Y == points[1].Y) return points[0];
        if (points[1].X == points[2].X && points[1].Y == points[2].Y) return points[1];
        if (points[0].X == points[2].X && points[0].Y == points[2].Y) return points[0];

        // stredovy bod
        Point middlePoint = new Point();
        middlePoint.X = (points[0].X + points[1].X + points[2].X) / 3;
        middlePoint.Y = (points[0].Y + points[1].Y + points[2].Y) / 3;

        return middlePoint;
    }
}
