using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        try
        {
            var inputLines = new List<string>();
            string line;
            while ((line = Console.ReadLine()) != null)
            {
                inputLines.Add(line);
            }

            if (inputLines.Count == 0)
            {
                throw new Exception();
            }

            var (shelves, shoppingLists) = LoadData(inputLines);

            foreach (var shoppingList in shoppingLists)
            {
                Console.WriteLine("Optimalizovany seznam:");
                var optimizedList = OptimizeShoppingList(shelves, shoppingList);
                for (int idx = 0; idx < optimizedList.Count; idx++)
                {
                    var (item, shelfId, shelfItem) = optimizedList[idx];
                    if (shelfId == "N/A")
                    {
                        Console.WriteLine($" {idx}. {item} -> {shelfId}");
                    }
                    else
                    {
                        Console.WriteLine($" {idx}. {item} -> #{shelfId} {shelfItem}");
                    }
                }
                Console.WriteLine();
            }
        }
        catch
        {
            Console.WriteLine("Nespravny vstup.");
            Environment.Exit(1);
        }
    }

    static (Dictionary<int, List<string>>, List<List<string>>) LoadData(List<string> lines)
    {
        var shelves = new Dictionary<int, List<string>>();
        var shoppingLists = new List<List<string>>();
        int i = 0;

        try
        {
            while (i < lines.Count && !string.IsNullOrWhiteSpace(lines[i]))
            {
                if (lines[i].StartsWith("#"))
                {
                    int shelfId = int.Parse(lines[i].Substring(1));
                    if (shelfId != shelves.Count)
                    {
                        throw new Exception();
                    }
                    shelves[shelfId] = new List<string>();
                    i++;
                    while (i < lines.Count && !string.IsNullOrWhiteSpace(lines[i]) && !lines[i].StartsWith("#"))
                    {
                        shelves[shelfId].Add(lines[i].Trim());
                        i++;
                    }
                }
                else
                {
                    throw new Exception();
                }
            }

            if (i == lines.Count || !string.IsNullOrWhiteSpace(lines[i]))
            {
                throw new Exception();
            }

            i++;
            var currentList = new List<string>();

            while (i < lines.Count)
            {
                if (!string.IsNullOrWhiteSpace(lines[i]))
                {
                    currentList.Add(lines[i].Trim());
                }
                else
                {
                    if (currentList.Count > 0)
                    {
                        shoppingLists.Add(new List<string>(currentList));
                        currentList.Clear();
                    }
                }
                i++;
            }
            if (currentList.Count > 0)
            {
                shoppingLists.Add(currentList);
            }
        }
        catch
        {
            throw new Exception();
        }

        return (shelves, shoppingLists);
    }

    static (int, string) FindItemInShelves(Dictionary<int, List<string>> shelves, string item)
    {
        foreach (var shelf in shelves)
        {
            foreach (var shelfItem in shelf.Value)
            {
                if (string.Equals(item, shelfItem, StringComparison.OrdinalIgnoreCase))
                {
                    return (shelf.Key, shelfItem);
                }
            }
        }

        foreach (var shelf in shelves)
        {
            foreach (var shelfItem in shelf.Value)
            {
                if (shelfItem.IndexOf(item, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return (shelf.Key, shelfItem);
                }
            }
        }

        return (-1, null);
    }

    static List<(string, string, string)> OptimizeShoppingList(Dictionary<int, List<string>> shelves, List<string> shoppingList)
    {
        var foundItems = new List<(string, int, string)>();
        var notFoundItems = new List<(string, string, string)>();

        foreach (var item in shoppingList)
        {
            var foundItem = FindItemInShelves(shelves, item);
            if (foundItem.Item1 != -1)
            {
                foundItems.Add((item, foundItem.Item1, foundItem.Item2));
            }
            else
            {
                notFoundItems.Add((item, "N/A", ""));
            }
        }

        foundItems = foundItems.OrderBy(x => x.Item2).ThenBy(x => shoppingList.IndexOf(x.Item1)).ToList();
        var optimizedList = foundItems.Select(x => (x.Item1, x.Item2.ToString(), x.Item3)).ToList();
        optimizedList.AddRange(notFoundItems);

        return optimizedList;
    }
}