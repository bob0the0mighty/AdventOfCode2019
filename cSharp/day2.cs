using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace cSharp.day2 {

  enum Opcode {
    add = 1,
    mul = 2,
    end = 99
  }

  public class DayTwo {

    public static void Run(){
      Console.WriteLine("Day 2");
      Console.WriteLine("Part 1");

      var intcodes = File.ReadLines("../Input/day2.1")
        .Select(line => line.Split(","))
        .First()
        .Select(str => int.Parse(str));

      var lastMinuteFix = intcodes.ToArray();
      lastMinuteFix[1] = 12;
      lastMinuteFix[2] = 2;

      var computedCodes = ComputeIntcodes(lastMinuteFix);
      Console.WriteLine($"Index 0 value: {computedCodes[0]}");
      Console.WriteLine("Part 2");

      var range = Enumerable.Range(0, 100);
      var nounAndVerbs = new List<(int, int)>();
      foreach(var noun in range.ToArray()){
        foreach(var verb in range.ToArray()){
          nounAndVerbs.Add((noun, verb));
        }
      }

      var correctNounAndVerb = nounAndVerbs
        .Select(tup => {
          var tempArr = intcodes.ToArray();

          tempArr[1] = tup.Item1;
          tempArr[2] = tup.Item2;

          return (ComputeIntcodes(tempArr)[0], tup);
        })
        .Where(search => search.Item1 == 19690720)
        .First();
        
        Console.WriteLine(correctNounAndVerb.tup);
        Console.WriteLine($"100 * Noun + Verb = {100 * correctNounAndVerb.tup.Item1 + correctNounAndVerb.tup.Item2}");
    }
    
    public static int[] ComputeIntcodes(int[] intcodes){
      for(var index = 0; index < intcodes.Length; index+=4){
        var opcode = (Opcode) intcodes[index];
        var result = 0;
        switch (opcode){
          case Opcode.add:
            result = intcodes[intcodes[index+1]] + intcodes[intcodes[index+2]];
            intcodes[intcodes[index+3]] = result;
            break;
          case Opcode.mul:
            result = intcodes[intcodes[index+1]] * intcodes[intcodes[index+2]];
            intcodes[intcodes[index+3]] = result;
            break;
          case Opcode.end:
            index = intcodes.Length;
            break;
          default:
            break;
        }
      }

      return intcodes;
    }

  }
}
