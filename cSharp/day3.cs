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

      var wireRuns = new List<IEnumerable<(int, int, int)>>();

      foreach (var line in lines)
      {
        var wireRun = new List<(int, int, int)>();
        var startPoint = (0, 0, 0);
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
      var connectionWireSet = wireRunSets[0].Join(wireRunSets[1], 
        first => (first.Item1, first.Item2), 
        second => (second.Item1, second.Item2),
        (first, second) => (first.Item1, first.Item2, first.Item3 + second.Item3))
        .ToList();

      var closestIntersection = connectionWireSet.MinBy(tup => Math.Abs(tup.Item1) + Math.Abs(tup.Item2))
        .First();
      var distance = Math.Abs(closestIntersection.Item1) + Math.Abs(closestIntersection.Item2);

      Console.WriteLine($"The manhatten distance to the closest intersection {closestIntersection} is {distance}");
      Console.WriteLine("Part 2");

      var cheapestIntersection = connectionWireSet.MinBy(tup => tup.Item3)
        .First();
      Console.WriteLine($"Point {cheapestIntersection} total cost is {cheapestIntersection.Item3}");
    }

    public static IEnumerable<(int x, int y, int cost)> CalculateWireRun(string instruction, (int x, int y, int cost) startPoint)
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
          return (x, y, startPoint.cost + count);
        });
    }
  }
}
