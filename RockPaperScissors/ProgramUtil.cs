using System;
using System.Collections.Generic;
using System.Linq;

namespace RockPaperScissors
{
    public static class ProgramUtil
    {
        private static void ShowMenu(IReadOnlyList<string> moves)
        {
            Console.WriteLine("Available moves:");
            for(var i = 0; i < moves.Count; i++)
            {
                Console.WriteLine($"{i+1} - {moves[i]}");
            }
            Console.Write("Enter your move (0-exit): ");
        }

        public static void InitComputerMove(string[] moves, out int move, out byte[] key, out byte[] hmac)
        {
            const int keySize = 128 / 8;
            key = SecurityUtil.GenerateRandomKey(keySize);
            move = new Random().Next(0, moves.Length);
            hmac = SecurityUtil.GetHmac(key, moves[move]);
        }

        public static void InitUserMove(string[] moves, out int userChoice)
        {
            do
                ShowMenu(moves);
            while (!int.TryParse(Console.ReadLine(), out userChoice) || userChoice < 0 || moves.Length < userChoice);
            if (userChoice == 0) Environment.Exit(1);
            userChoice--;
        }

        public static string ProcessMoves(string[] moves, int userChoice, int computerChoice)
        {
            var loses = new List<int>();
            for (var i = userChoice + 1; i < userChoice + moves.Length / 2 + 1; i++)
            {
                loses.Add(i % moves.Length);
            }
            return loses.Contains(computerChoice) ? "You lost!" : "You won!";
        }
        
        public static bool ValidateArgs(string[] moves)
        {
            return ValidateArgsLength(moves) && ValidateArgsOdd(moves) && ValidateArgsDuplicate(moves);
        }

        private static bool ValidateArgsLength(IReadOnlyCollection<string> moves)
        {
            if (moves.Count >= 3) return true;
            Console.WriteLine("Please enter at least 3 possible moves.");
            return false;
        }

        private static bool ValidateArgsOdd(IReadOnlyCollection<string> moves)
        {
            if (moves.Count % 2 != 0) return true;
            Console.WriteLine("Please enter an odd number of moves ");
            return false;
        }

        private static bool ValidateArgsDuplicate(IEnumerable<string> moves)
        {
            if (!moves.GroupBy(x => x).Any(g => g.Count() > 1)) return true;
            Console.WriteLine("Please delete the duplicates.");
            return false;
        }
    }
}