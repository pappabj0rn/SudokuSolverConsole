using System;
using SudokuSolverConsole.Strategies;

namespace SudokuSolverConsole
{
    class Program
    {
        private static PlayingField _field = new PlayingField(
            "094080002200003010000000905000000006000040730009150000800020094000000500530007000");

        static void Main(string[] args)
        {
            Console.WriteLine("Enter field, press s to try solve.");

            var input = Console.ReadKey(true);

            var fieldStr = "";

            while (input.Key != ConsoleKey.S)
            {
                input = Console.ReadKey();
                if (!int.TryParse(input.KeyChar.ToString(), out var i))
                {
                    Console.WriteLine("Invalid input. Enter 0-9 or s.");
                    continue;
                }

                fieldStr += i;

                if (fieldStr.Length == 81)
                    break;
            }

            if(fieldStr.Length==81)
                _field = new PlayingField(fieldStr);

            var strategy = new CandidateFilterStrategy();


            strategy.TrySolve(_field);
            Console.WriteLine();
            PrintField();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        private static void PrintField()
        {
            const string borderTop =    "┌───┬───┬───┐";
            const string borderMiddle = "├───┼───┼───┤";
            const string borderBottom = "└───┴───┴───┘";
            // └ ┴ ┼ ─ ┘┌ ┬ ─ ┐ |

            Console.WriteLine(borderTop);  

            for (var y = 0; y < PlayingField.Height; y++)
            {
                if(y>0 && y%3==0)
                    Console.WriteLine(borderMiddle);

                PrintRow(y);
            }

            Console.WriteLine(borderBottom);
        }

        private static void PrintRow(int y)
        {
            const string separator = "|";

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(separator);

            var rowSquares = _field.GetRow(y);

            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    var k = j + i * 3;

                    Console.ForegroundColor = rowSquares[k].Given 
                        ? ConsoleColor.White 
                        : ConsoleColor.Green;

                    Console.Write(rowSquares[k].Value == 0
                        ? " "
                        : rowSquares[k].Value.ToString());

                }

                Console.ForegroundColor = ConsoleColor.White;
                if (i == 2)
                    Console.WriteLine(separator);
                else
                    Console.Write(separator);
            }
        }
    }
}