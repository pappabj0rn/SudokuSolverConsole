using System;

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
            for (var x = 0; x < PlayingField.Width; x++)
            {
                var rowNumbers = field.GetRow(x);
                var row = "";
                for (var y = 0; y < PlayingField.Height; y++)
                {
                    row += field.Squares[x, y].Value != 0 
                        ? field.Squares[x, y].Value.ToString() 
                        : " ";
                }    
                Console.WriteLine(row);
            }
        }
    }
}
