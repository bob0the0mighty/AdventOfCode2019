using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace cSharp.day1 {
  public class dayOne {
    public static void run() {
      Console.WriteLine("Day 1");
      Console.WriteLine("Part 1");

      var masses = File.ReadLines("../Input/day1.1")
        .Select(line => int.Parse(line));


      var fuel = masses.Select(mass => GetFuelRequirement(mass))
        .Sum();

      Console.WriteLine($"Fuel requirement: {fuel}");
      Console.WriteLine("Part 2");

      var betterFuel = masses.Select(mass => GetBetterFuelRequirement(mass))
        .Sum();

      Console.WriteLine($"Fuel requirement: {betterFuel}");
    }

    public static int GetBetterFuelRequirement(int mass) {
      var unmanagedMass = mass;
      var totalFuel = 0;

      while(true){
        var newMass = GetFuelRequirement(unmanagedMass);
        unmanagedMass = newMass;
        if(unmanagedMass <= 0)
          break;
        totalFuel += newMass; 
      }

      return totalFuel;
    }

    public static int GetFuelRequirement(int mass) {
      return (mass / 3) - 2;
    }
  }
}