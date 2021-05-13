using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Queens
{
    class Program
    {
        static int dimansion = 8;

        static bool[,] board;

        static double successed = 0;

        static StringBuilder stringBuilder = new StringBuilder();

        static void Main(string[] args)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            if (args.Length > 0)
            {
                int.TryParse(args[0], out dimansion);
            }

            board = new bool[dimansion, dimansion];

            placeQueenAtRow(0);

            Console.WriteLine(stringBuilder.ToString());

            Console.WriteLine(successed);

            Console.WriteLine(Math.Pow(dimansion, dimansion) - successed);

            stopwatch.Stop();

            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }

        static void placeQueenAtRow(int y)
        {
            if (y == dimansion)
            {
                successed++;

                writeBoard();

                return;
            }

            for (int x = 0; x < dimansion; x++)
            {
                if (check(x, y))
                {
                    continue;
                }

                board[x, y] = true;

                placeQueenAtRow(y + 1);

                board[x, y] = false;
            }

            return;
        }

        static void writeBoard()
        {
            for (int x = 0; x < dimansion; x++)
            {
                for (int y = 0; y < dimansion; y++)
                {
                    stringBuilder.Append(Convert.ToInt32(board[x, y]));
                }

                stringBuilder.AppendLine();
            }

            stringBuilder.AppendLine();
            stringBuilder.AppendLine();
        }

        static bool check(int x, int y)
        {
            return checkLeftTop(x, y) || checkLeftDown(x, y) || checkLeft(x, y);
        }

        static bool checkLeftTop(int x, int y)
        {

            for (int i = 0; i <= Math.Min(x, y); i++)
            {
                if (board[x - i, y - i])
                {
                    return true;
                }
            }

            return false;

            //return Enumerable.Range(1, Math.Min(x, y)).Select(i => board[x - i, y - i]).Any(b => b);

            //var a = Enumerable.Range(1, Math.Min(x, y)).Select(i => new BoardInfo
            //{
            //    X = x - i,
            //    Y = y - i,
            //    Queen = board[x - i, y - i]
            //});
            //return a.Any(b => b.Queen);
        }

        static bool checkLeft(int x, int y)
        {
            for (int i = 0; i <= y; i++)
            {
                if (board[x, y - i])
                {
                    return true;
                }
            }

            return false;

            //return Enumerable.Range(1, y).Select(i => board[x, y - i]).Any(b => b);

            //var a = Enumerable.Range(1, y).Select(i =>
            // new BoardInfo
            // {
            //     X = x,
            //     Y = y - i,
            //     Queen = board[x, y - i]
            // });
            //return a.Any(b => b.Queen);
        }

        static bool checkLeftDown(int x, int y)
        {
            for (int i = 0; i <= Math.Min((dimansion - 1) - x, y); i++)
            {
                if (board[x + i, y - i])
                {
                    return true;
                }
            }

            return false;

            //return Enumerable.Range(1, Math.Min((dimansion - 1) - x, y)).Select(i => board[x + i, y - i]).Any(b => b);

            //var a = Enumerable.Range(1, Math.Min((dimansion - 1) - x, y)).Select(i =>
            //    new BoardInfo
            //    {
            //        X = x + i,
            //        Y = y - i,
            //        Queen = board[x + i, y - i]
            //    });
            //return a.Any(b => b.Queen);
        }
    }

    public class BoardInfo
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool Queen { get; set; }
    }
}


