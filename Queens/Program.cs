using System;
using System.Diagnostics;
using System.Text;

namespace Queens
{
    class Program
    {
        static int dimansion = 12;

        static bool[,] chessBoard;

        static double successed = 0;

        static StringBuilder stringBuilder = new StringBuilder();

        static bool[] yLayer;
        static bool[] slashLayer;
        static bool[] backslashLayer;


        static void Main(string[] args)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            if (args.Length > 0)
            {
                int.TryParse(args[0], out dimansion);
            }

            initChessBoard();

            placeQueenAtRow(0);

            stopwatch.Stop();

            //Console.WriteLine(stringBuilder.ToString());

            Console.WriteLine(successed);

            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }

        static void initChessBoard()
        {
            chessBoard = new bool[dimansion, dimansion];
            yLayer = new bool[dimansion];
            slashLayer = new bool[2 * dimansion - 1];
            backslashLayer = new bool[2 * dimansion - 1];
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

                chessBoard[x, y] = true;
                setLayer(x, y, true);

                placeQueenAtRow(y + 1);

                chessBoard[x, y] = false;
                setLayer(x, y, false);
            }

            return;
        }

        static void writeBoard()
        {
            stringBuilder.AppendLine($"//Solution {successed + 1}");

            for (int x = 0; x < dimansion; x++)
            {
                for (int y = 0; y < dimansion; y++)
                {
                    //stringBuilder.Append($"[{x},{y}] ");
                    stringBuilder.Append(Convert.ToInt32(chessBoard[x, y]));
                    //stringBuilder.Append($"     ");
                }

                stringBuilder.AppendLine();
            }

            stringBuilder.AppendLine();
            stringBuilder.AppendLine();
        }

        static void setLayer(int x, int y, bool placeQueen)
        {
            setYLayer(x, y, placeQueen);
            setSlashLayer(x, y, placeQueen);
            setBackslashLayer(x, y, placeQueen);
        }
        static void setYLayer(int x, int y, bool placeQueen)
        {
            yLayer[x] = placeQueen;
        }
        static void setSlashLayer(int x, int y, bool placeQueen)
        {
            slashLayer[dimansion - 1 - x + y] = placeQueen;
        }
        static void setBackslashLayer(int x, int y, bool placeQueen)
        {
            backslashLayer[x + y] = placeQueen;
        }

        static bool check(int x, int y)
        {
            return checkLeft(x, y) || checkLeftTop(x, y) || checkLeftDown(x, y);
        }
        static bool checkLeftTop(int x, int y)
        {
            return backslashLayer[x + y];

            //for (int i = 0; i <= Math.Min(x, y); i++)
            //{
            //    if (chessBoard[x - i, y - i])
            //    {
            //        return true;
            //    }
            //}

            //return false;

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
            return yLayer[x];

            //for (int i = 0; i <= y; i++)
            //{
            //    if (chessBoard[x, y - i])
            //    {
            //        return true;
            //    }
            //}

            //return false;

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
            return slashLayer[dimansion - 1 - x + y];

            //for (int i = 0; i <= Math.Min((dimansion - 1) - x, y); i++)
            //{
            //    if (chessBoard[x + i, y - i])
            //    {
            //        return true;
            //    }
            //}

            //return false;

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


