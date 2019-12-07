using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace cSharp
{
  public class TreeNode<T>
  {
    public T value;
    public TreeNode<T> parent;
    public ICollection<TreeNode<T>> children;

    public TreeNode(T val)
    {
      value = val;
    }
  }

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

    public static IEnumerable<T> BreadthFirstSearch<T>(this T head, Func<T, IEnumerable<T>> childrenFunc)
    {
      yield return head;
      var last = head;
      foreach (var node in BreadthFirstSearch(head, childrenFunc))
      {
        foreach (var child in childrenFunc(node))
        {
          yield return child;
          last = child;
        }
        if (last.Equals(node)) yield break;
      }
    }
  }

  public static class DaySix
  {

    public static void Run()
    {
      Console.WriteLine("Day 6");
      Console.WriteLine("Part 1");

      var intcodes = File.ReadLines("../Input/day6.1")
          .Select(line => line.Split(")"));
    }
  }
}