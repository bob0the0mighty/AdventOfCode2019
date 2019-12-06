using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace cSharp
{

  public enum ModeCode
  {
    pos = 0,
    imm = 1
  }

  public enum OpCode2
  {
    add = 1,
    mul = 2,
    sav = 3,
    wrt = 4,
    jmt = 5,
    jmf = 6,
    lst = 7,
    eql = 8,
    end = 99
  }

  public static class DayFive
  {

    public static void Run()
    {
      Console.WriteLine("Day 5");
      Console.WriteLine("Part 1");

      var intcodes = File.ReadLines("../Input/day5.1")
        .Select(line => line.Split(","))
        .First()
        .Select(str => int.Parse(str));

      ComputeIntcodes(intcodes.ToArray());

      Console.WriteLine("Part 2");

      ComputeIntcodes(intcodes.ToArray());
    }

    public static int[] ComputeIntcodes(int[] intcodes)
    {
      var threeParamSize = 4;
      var twoParamSize = 3;
      var oneParamSize = 2;
      var opSize = threeParamSize;

      for (var ip = 0; ip < intcodes.Length; ip += opSize)
      {
        var modeCodes = GetParameterModeCodes(intcodes[ip] / 100);
        var opcode = (OpCode2)(intcodes[ip] % 100);
        var result = 0;
        int paramOne, paramTwo, paramThree;
        switch (opcode)
        {
          case OpCode2.add:
            opSize = threeParamSize;
            paramOne = GetParameterByMode(modeCodes[0], intcodes, ip + 1);
            paramTwo = GetParameterByMode(modeCodes[1], intcodes, ip + 2);
            paramThree = intcodes[ip + 3];
            result = paramOne + paramTwo;
            intcodes[paramThree] = result;

            break;
          case OpCode2.mul:
            opSize = threeParamSize;
            paramOne = GetParameterByMode(modeCodes[0], intcodes, ip + 1);
            paramTwo = GetParameterByMode(modeCodes[1], intcodes, ip + 2);
            paramThree = intcodes[ip + 3];
            result = paramOne * paramTwo;
            intcodes[paramThree] = result;
            break;
          case OpCode2.sav:
            opSize = oneParamSize;
            paramOne = intcodes[ip + 1];
            Console.WriteLine("Please enter a single integer: ");
            var input = int.Parse(Console.ReadLine());
            intcodes[paramOne] = input;
            break;
          case OpCode2.wrt:
            opSize = oneParamSize;
            paramOne = intcodes[ip + 1];
            Console.WriteLine(intcodes[paramOne]);
            break;
          case OpCode2.jmt:
            opSize = twoParamSize;
            paramOne = GetParameterByMode(modeCodes[0], intcodes, ip + 1);
            if(paramOne != 0){
              ip = GetParameterByMode(modeCodes[1], intcodes, ip + 2);
              opSize = 0;
            }
            break;
          case OpCode2.jmf:
            opSize = twoParamSize;
            paramOne = GetParameterByMode(modeCodes[0], intcodes, ip + 1);
            if(paramOne == 0){
              ip = GetParameterByMode(modeCodes[1], intcodes, ip + 2);
              opSize = 0;
            }
            break;
          case OpCode2.lst:
            opSize = threeParamSize;
            paramOne = GetParameterByMode(modeCodes[0], intcodes, ip + 1);
            paramTwo = GetParameterByMode(modeCodes[1], intcodes, ip + 2);
            paramThree = intcodes[ip + 3];
            if(paramOne < paramTwo){
              intcodes[paramThree] = 1;
            } else {
              intcodes[paramThree] = 0;
            }
            break;
          case OpCode2.eql:
            opSize = threeParamSize;
            paramOne = GetParameterByMode(modeCodes[0], intcodes, ip + 1);
            paramTwo = GetParameterByMode(modeCodes[1], intcodes, ip + 2);
            paramThree = intcodes[ip + 3];
            if(paramOne == paramTwo){
              intcodes[paramThree] = 1;
            } else {
              intcodes[paramThree] = 0;
            }
            break;
          case OpCode2.end:
            ip = intcodes.Length;
            break;
          default:
            break;
        }
      }

      return intcodes;
    }

    public static int GetParameterByMode(ModeCode code, int[] intcodes, int ip){
      return (code == ModeCode.imm) ? intcodes[ip] : intcodes[intcodes[ip]];
    }

    public static IList<ModeCode> GetParameterModeCodes(int codes)
    {
      var modeCodes = new List<ModeCode>() { ModeCode.pos, ModeCode.pos, ModeCode.pos };

      for (var count = 0; count < 3; ++count)
      {
        var code = (ModeCode)(codes % 10);
        modeCodes[count] = code;
        codes /= 10;
      }

      return modeCodes;
    }
  }
}