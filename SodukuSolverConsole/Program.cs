using System;
using System.Linq;

namespace SudokuSolverConsole
{
    class Program
    {
        private static readonly PlayingField field = new PlayingField("007000069010906040000302000238790100400030020000260030609000000040080507370019000");

        static void Main(string[] args)
        {
            Print(field);

            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }

        private static void Print(PlayingField field)
        {
            const string borderTop =    "┌───┬───┬───┐";
            const string borderRow =    "|{0}{1}{2}|{3}{4}{5}|{6}{7}{8}|";
            const string borderMiddle = "├───┼───┼───┤";
            const string borderBottom = "└───┴───┴───┘";
            // └ ┴ ┼ ─ ┘┌ ┬ ─ ┐ |

            Console.WriteLine(borderTop);  

            for (var y = 0; y < PlayingField.Height; y++)
            {
                if(y>0 && y%3==0)
                    Console.WriteLine(borderMiddle);

                var rowNumbers = field.GetRow(y).Select(x=>x.Value > 0 ? x.Value.ToString() : " ").ToArray();
                var row = string.Format(borderRow,
                    rowNumbers[0],
                    rowNumbers[1],
                    rowNumbers[2],
                    rowNumbers[3],
                    rowNumbers[4],
                    rowNumbers[5],
                    rowNumbers[6],
                    rowNumbers[7],
                    rowNumbers[8]
                    );

                Console.WriteLine(row);
            }

            Console.WriteLine(borderBottom);
        }
    }
}
