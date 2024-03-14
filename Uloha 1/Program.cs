using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

class Program
{
    static void Main()
    {
        int roomSize = 0;

        Console.WriteLine("Rozměr místnosti:");
        string tempRoomSize = Console.ReadLine();
        if (!int.TryParse(tempRoomSize, out roomSize))
        {
            Console.WriteLine("Nesprávný vstup");
            return;
        }

        Console.WriteLine("Bod 1#:");
        Point point1 = ReadPoint(roomSize);

        Console.WriteLine("Bod 2#:");
        Point point2 = ReadPoint(roomSize);

        if (IsValidPoint(point1, roomSize) && IsValidPoint(point2, roomSize))
        {
            Console.WriteLine("Délka potrubí: " + PipeLenghtCalculation(point1, point2, roomSize));
            Console.WriteLine("Délka hadice: " + HoseLenghtCalculation(point1, point2, roomSize));
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine("Nesprávný vstup");
        }

    }

    private static Point ReadPoint(int roomSize)
    {

        string[] input = Console.ReadLine().Split(' ');
        if (input.Length == 3 && int.TryParse(input[0], out int x) && int.TryParse(input[1], out int y) && int.TryParse(input[2], out int z) && x >= 0 && y >= 0 && z >= 0 && x <= roomSize && y <= roomSize && z <= roomSize)
        {
            return new Point(x, y, z);
        }
        else
        {
            Console.WriteLine("Nesprávný bod");
            return null;
        }
    }

    static bool IsValidPoint(Point point, int roomSize)
    {
        bool isCorner = (point.X == roomSize || point.X == 0) && (point.Y == roomSize || point.Y == 0) && (point.Z == roomSize || point.Z == 0);
        if (point != null)
        {
            if (!isCorner)
            {
                List<int> list = new List<int>();
                if (point.X < 20 || point.X > roomSize - 20)
                {
                    list.Add(point.X);
                }
                if (point.Y < 20 || point.Y > roomSize - 20)
                {
                    list.Add(point.Y);
                }
                if (point.Z < 20 || point.Z > roomSize - 20)
                {
                    list.Add(point.Z);
                }

                if (list.Count == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Tento bod je rohový");
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    static int PipeLenghtCalculation(Point point1, Point point2, int roomSize)
    {
        int xDistance = 0;
        int yDistance = 0;
        int zDistance = 0;

        if (Math.Abs(point1.X - point2.X) == roomSize)
        {
            if (Math.Abs(point1.Y - point2.Y) < Math.Abs(point1.Z - point2.Z))
            {
                if ((roomSize - point1.Z) + (roomSize - point2.Z) > point1.Z + point2.Z)
                {
                    zDistance = point1.Z + point2.Z;
                    xDistance = Math.Abs(point1.X - point2.X);
                    yDistance = Math.Abs(point1.Y - point2.Y);
                }
                else
                {
                    zDistance = (roomSize - point1.Z) + (roomSize - point2.Z);
                    xDistance = Math.Abs(point1.X - point2.X);
                    yDistance = Math.Abs(point1.Y - point2.Y);
                }
            }
            else
            {
                if ((roomSize - point1.Y) + (roomSize - point2.Y) > point1.Y + point2.Y)
                {
                    yDistance = point1.Y + point2.Y;
                    xDistance = Math.Abs(point1.X - point2.X);
                    zDistance = Math.Abs(point1.Z - point2.Z);
                }
                else
                {
                    yDistance = (roomSize - point1.Y) + (roomSize - point2.Y);
                    xDistance = Math.Abs(point1.X - point2.X);
                    zDistance = Math.Abs(point1.Z - point2.Z);
                }
            }
        }
        else if (Math.Abs(point1.Y - point2.Y) == roomSize)
        {
            if (Math.Abs(point1.X - point2.X) < Math.Abs(point1.Z - point2.Z))
            {
                if ((roomSize - point1.Z) + (roomSize - point2.Z) > point1.Z + point2.Z)
                {
                    zDistance = point1.Z + point2.Z;
                    xDistance = Math.Abs(point1.X - point2.X);
                    yDistance = Math.Abs(point1.Y - point2.Y);
                }
                else
                {
                    zDistance = (roomSize - point1.Z) + (roomSize - point2.Z);
                    xDistance = Math.Abs(point1.X - point2.X);
                    yDistance = Math.Abs(point1.Y - point2.Y);
                }
            }
            else
            {
                if ((roomSize - point1.X) + (roomSize - point2.X) > point1.X + point2.X)
                {
                    xDistance = point1.X + point2.X;
                    yDistance = Math.Abs(point1.Y - point2.Y);
                    zDistance = Math.Abs(point1.Z - point2.Z);
                }
                else
                {
                    xDistance = (roomSize - point1.X) + (roomSize - point2.X);
                    yDistance = Math.Abs(point1.Y - point2.Y);
                    zDistance = Math.Abs(point1.Z - point2.Z);
                }
            }
        }
        else if (Math.Abs(point1.Z - point2.Z) == roomSize)
        {
            if (Math.Abs(point1.Y - point2.Y) < Math.Abs(point1.X - point2.X))
            {
                if ((roomSize - point1.X) + (roomSize - point2.X) > point1.X + point2.X)
                {
                    xDistance = point1.X + point2.X;
                    yDistance = Math.Abs(point1.Y - point2.Y);
                    zDistance = Math.Abs(point1.Z - point2.Z);
                }
                else
                {
                    xDistance = (roomSize - point1.X) + (roomSize - point2.X);
                    yDistance = Math.Abs(point1.Y - point2.Y);
                    zDistance = Math.Abs(point1.Z - point2.Z);
                }
            }
            else
            {
                if ((roomSize - point1.Y) + (roomSize - point2.Y) > point1.Y + point2.Y)
                {
                    yDistance = point1.Y + point2.Y;
                    xDistance = Math.Abs(point1.X - point2.X);
                    zDistance = Math.Abs(point1.Z - point2.Z);
                }
                else
                {
                    yDistance = (roomSize - point1.Y) + (roomSize - point2.Y);
                    xDistance = Math.Abs(point1.X - point2.X);
                    zDistance = Math.Abs(point1.Z - point2.Z);
                }
            }
        }
        else
        {
            xDistance = Math.Abs(point1.X - point2.X);
            yDistance = Math.Abs(point1.Y - point2.Y);
            zDistance = Math.Abs(point1.Z - point2.Z);
        }
        return xDistance + yDistance + zDistance;
    }

    static double HoseLenghtCalculation(Point point1, Point point2, int roomSize)
    {
        int shortSide = 0;
        int longSide = 0;

        if (Math.Abs(point1.X - point2.X) == roomSize)
        {
            shortSide = Math.Abs(point1.Y - point2.Y);

            if ((roomSize - point1.Z) + (roomSize - point2.Z) < point1.Z + point2.Z)
            {
                longSide = (roomSize - point1.Z) + (roomSize - point2.Z) + roomSize;
            }
            else
            {
                longSide = point1.Z + point2.Z + roomSize;
            }
        }
        else if (Math.Abs(point1.Y - point2.Y) == roomSize)
        {
            shortSide = Math.Abs(point1.X - point2.X);

            if ((roomSize - point1.Z) + (roomSize - point2.Z) < point1.Z + point2.Z)
            {
                longSide = (roomSize - point1.Z) + (roomSize - point2.Z) + roomSize;
            }
            else
            {
                longSide = point1.Z + point2.Z + roomSize;
            }
        }
        else if (Math.Abs(point1.Z - point2.Z) == roomSize)
        {
            shortSide = Math.Abs(point1.X - point2.X);

            if ((roomSize - point1.Y) + (roomSize - point2.Y) < point1.Y + point2.Y)
            {
                longSide = (roomSize - point1.Y) + (roomSize - point2.Y) + roomSize;
            }
            else
            {
                longSide = point1.Y + point2.Y + roomSize;
            }

        }
        else
        {
            shortSide = Math.Min(Math.Abs(point1.X - point2.X), Math.Min(Math.Abs(point1.Y - point2.Y), Math.Abs(point1.Z - point2.Z)));
            longSide = Math.Abs(point1.X - point2.X) + Math.Abs(point1.Y - point2.Y) + Math.Abs(point1.Z - point2.Z) - shortSide;
        }

        return Math.Sqrt(shortSide * shortSide + longSide * longSide);
    }
}

class Point
{
    public int X { get; }
    public int Y { get; }
    public int Z { get; }

    public Point(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }
}
