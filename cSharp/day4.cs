using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoreLinq;

namespace cSharp
{
  public static class DayFour
  {
    public static void Run()
    {
      Console.WriteLine("Day 4");
      Console.WriteLine("Part 1");

      var count = 630395 - 153517;
      var result = Enumerable.Range(153517, count)
        .Where(number => NumberHasTwins(number))
        .Where(number => NeverDecreases(number));

      Console.WriteLine($"There are {result.Count()} possible passwords");

      Console.WriteLine("Part 2");

      var filteredResult = result.Where(number => HasAtLeastOneLoneSet(number));

      Console.WriteLine($"There are {filteredResult.Count()} filtered passwords");
    }

    public static bool NumberHasTwins(int number)
    {
      return number.ToString()
          .Window(2)
          .Any(couple => couple.First() == couple.Last());
    }

    public static bool NeverDecreases(int number)
    {
      return number.ToString()
        .Window(2)
        .Select(list => list.First() <= list.Last())
        .Aggregate((acc, nxt) => acc && nxt);
    }

    //based of part 2 of https://github.com/fdouw/AoC2019/blob/master/Day04/Day04.cs
    //This works since the range has already been filtered, so any number with 
    //a count of 2 is guarenteeed to have one lone pair.
    public static bool HasAtLeastOneLoneSet(int number)
    {
      return number.ToString()
        .GroupBy(chr => chr)
        .Select(chr => chr.Count())
        .Any(count => count == 2);
    }
  }
}