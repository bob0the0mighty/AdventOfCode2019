using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace cSharp
{
  //Based off http://www.claassen.net/geek/blog/2009/06/searching-tree-of-objects-with-linq.html
  public static class TreeExtensions
  {
    public static IEnumerable<T> DepthFirstSearch<T>(this T head, Func<T, IEnumerable<T>> childrenFunc)
    {
      yield return head;
      foreach (var node in childrenFunc(head))
      {
        foreach (var child in DepthFirstSearch(node, childrenFunc))
        {
          yield return child;
        }
      }
    }

  public static class DaySix
  {

    public static void Run()
    {
      Console.WriteLine("Day 6");
      Console.WriteLine("Part 1");

      var orbitTree = File.ReadLines("../Input/day6.1")
          .Select(line => line.Split(")"))
          .GroupBy(pair => pair[0], pair => pair[1])
          .ToDictionary(group => group.Key, group => group.ToList());

      Console.WriteLine(orbitTree.Count);

      var dfs = ("COM", new List<string>()).DepthFirstSearch(root =>
      {
        var list = new List<string>();
        orbitTree.TryGetValue(root.Item1, out list);

        list = list ?? new List<string>();

        var trail = root.Item2.Concat(new List<string>() { root.Item1 })
          .ToList();

        return list.Select(child => (child, trail));
      })
        .ToList();

      Console.WriteLine($"Total number of orbits: {dfs.Sum(tup => tup.Item2.Count())}");
      Console.WriteLine("Part 2");

      var you = dfs.Where(tup => tup.Item1 == "YOU")
        .FirstOrDefault()
        .Item2;

      var santa = dfs.Where(tup => tup.Item1 == "SAN")
        .FirstOrDefault()
        .Item2;

      var differenceYou = you.Except(santa);
      var differenceSanta = santa.Except(you);

      Console.WriteLine(differenceYou.Count() + differenceSanta.Count());
    }
  }
}