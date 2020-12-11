using System;
using System.Collections.Generic;
using System.Linq;

namespace RockPaperScissors
{
    static class Program
    {
        static void Main(string[] moves)
        {
            if (!ProgramUtil.ValidateArgs(moves)) return;
            ProgramUtil.InitComputerMove(moves, out var computerChoice, out var key, out var hmac);
            Console.WriteLine($"HMAC:\n{BitConverter.ToString(hmac)}");
            ProgramUtil.InitUserMove(moves, out var userChoice);
            Console.WriteLine($"Your move: {moves[userChoice]}\nComputer move: {moves[computerChoice]}");
            Console.WriteLine(ProgramUtil.ProcessMoves(moves, userChoice, computerChoice));
            Console.WriteLine($"HMAC Key:\n{BitConverter.ToString(key)}");
        }
        
    }
}