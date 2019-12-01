using System;

namespace cSharp.day1
{
  public class dayOne
  {
    public static void run()
    {
      Console.WriteLine("Day 1");
      
      Console.WriteLine($"Fuel requirement: {GetFuelRequirement(12)}");
    }

    public static int GetFuelRequirement(int mass)
    {
      return (mass / 3) - 2;
    }
  }
}