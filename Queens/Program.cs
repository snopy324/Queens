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

            Console.WriteLine(stringBuilder.ToString());

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
                    stringBuilder.Append(chessBoard[x, y] ? 'Q' : '.');
                }

                stringBuilder.AppendLine();
            }

            stringBuilder.AppendLine().AppendLine();
        }

        static void setLayer(int x, int y, bool placeQueen)
        {
            setYLayer(x, y, placeQueen);
            setSlashLayer(x, y, placeQueen);
            setBackslashLayer(x, y, placeQueen);

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
        }

        static bool check(int x, int y)
        {
            return checkLeft(x, y) || checkLeftTop(x, y) || checkLeftDown(x, y);
            static bool checkLeftTop(int x, int y)
            {
                return backslashLayer[x + y];
            }
            static bool checkLeft(int x, int y)
            {
                return yLayer[x];
            }
            static bool checkLeftDown(int x, int y)
            {
                return slashLayer[dimansion - 1 - x + y];
            }
        }

    }
}


