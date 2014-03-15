using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PicrossCJL;
using System.Diagnostics;

namespace PicrossSolverCLI
{
    class Program
    {
        const string DEFAULT_FILENAME = "exemple_30x30.xml";

        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            PicrossPuzzle puzzle;
            PicrossSolver solver = new PicrossSolver();

            string filename;
            if (args.Length >= 1)
                filename = args[0];
            else
            {
                Console.Write("Puzzle's file [{0}]:", DEFAULT_FILENAME);
                filename = Console.ReadLine();
                if (filename == string.Empty)
                    filename = DEFAULT_FILENAME;
            }

            Console.WriteLine("Loading of the puzzle");
            if (filename.EndsWith(".non"))
                puzzle = PicrossPuzzle.LoadFromNonFile(filename);
            else
                puzzle = PicrossPuzzle.LoadXmlFile(filename);
            Console.WriteLine("Start of the solving");
            sw.Restart();
            solver.Solve(puzzle);
            sw.Stop();
            Console.WriteLine("Time to solve the puzzle: {0} ms", sw.ElapsedMilliseconds);
            Console.WriteLine(puzzle);
            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadLine();

        }
    }
}
