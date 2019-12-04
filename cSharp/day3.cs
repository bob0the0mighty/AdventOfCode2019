using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static MoreLinq.Extensions.MinByExtension;

namespace cSharp.day3
{
  public static class DayThree
  {
    public static void Run()
    {
      Console.WriteLine("Day 3");
      Console.WriteLine("Part 1");

      var lines = File.ReadLines("../Input/day3.1")
        .Select(line => line.Split(","));

      var wireRuns = new List<IEnumerable<(int, int)>>();

      foreach (var line in lines)
      {
        var wireRun = new List<(int, int)>();
        var startPoint = (0, 0);
        foreach (var run in line)
        {
          var currentRun = CalculateWireRun(run, startPoint);
          startPoint = currentRun.Last();
          wireRun.AddRange(currentRun);
        }
        wireRuns.Add(wireRun);
      }

      var wireRunSets = wireRuns.Select(run => run.ToHashSet())
        .ToList();
      wireRunSets[0].IntersectWith(wireRunSets[1]);

      var closestIntersection = wireRunSets[0].MinBy(tup => Math.Abs(tup.Item1) + Math.Abs(tup.Item2))
        .First();
      var distance = Math.Abs(closestIntersection.Item1) + Math.Abs(closestIntersection.Item2);

      Console.WriteLine($"The manhatten distance to the closest intersection {closestIntersection} is {distance}");
      Console.WriteLine("Part 2");

      var runsWithCost = wireRuns
        .Select(run => run.Select((point, index) => (point.Item1, point.Item2, index)))
        .Select(run => run.Where(point => point.Item1 == closestIntersection.Item1 && point.Item2 == closestIntersection.Item2))
        .Aggregate(0, (cost, run) => run.First().Item3 + cost);

      Console.WriteLine($"Total cost is {runsWithCost}");
    }

    public static IEnumerable<(int x, int y)> CalculateWireRun(string instruction, (int x, int y) startPoint)
    {
      var direction = instruction[0];
      var magnitude = int.Parse(instruction.Substring(1));

      var xDirection = (direction == 'L') ? -1 : (direction == 'R') ? 1 : 0;
      var yDirection = (direction == 'D') ? -1 : (direction == 'U') ? 1 : 0;

      return Enumerable.Range(1, magnitude)
        .Select(count =>
        {
          var x = xDirection * count + startPoint.x;
          var y = yDirection * count + startPoint.y;
          return (x, y);
        });
    }
  }
}
