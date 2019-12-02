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
