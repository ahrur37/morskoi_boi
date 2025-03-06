using System;

namespace morskoi_boi
{
    internal class Program
    {
        #region 
        static char[,] boardBot = new char[10, 10];
        static char[,] boardHit = new char[10, 10];
        static char[,] boardIgrok = new char[10, 10];
        static char currentPlayer = 'I';
        static bool win = false;
        static Random rnd = new Random();
        static int ships1 = 3;
        static int ships2 = 2;
        static int ships3 = 2;
        static int ships4 = 1;

        #endregion
        static void Main(string[] args)
        {
            InitializeBoard();
            PrintGrid();
        }
        public static void InitializeBoard() // базовые настройки 
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    boardBot[i, j] = '~';
                    boardIgrok[i, j] = '~';
                }
            }
            ZapolnenieBot();
            //ZapolnenieIgrok();
        }
        public static void ZapolnenieBot()
        {
            for (int i = 0; i < 8; i++) // 8 кораблей
            {
                int x = rnd.Next(1, 9);
                int y = rnd.Next(1, 9);
                if (boardBot[x, y] == 'S' || boardBot[x, y] == 'N')
                {
                    i--; 
                    continue;
                }
                boardBot[x, y] = 'S'; // одинарный
                int sboi = 1;
                while (sboi == 1)
                {
                    if (i >= 3 && i <= 4) // двойной
                    {
                        int v = rnd.Next(0, 3);
                        if (v == 0 && y - 1 >= 0 && y - 1 <= 9)
                        {
                            boardBot[x, y - 1] = 'S';
                            sboi = 0;
                        }
                        else if (v == 1 && y + 1 >= 0 && y + 1 <= 9)
                        {
                            boardBot[x, y + 1] = 'S';
                            sboi = 0;
                        }
                        else if (v == 2 && x - 1 >= 0 && x - 1 <= 9)
                        {
                            boardBot[x - 1, y] = 'S';
                            sboi = 0;
                        }
                        else if (v == 3 && x + 1 >= 0 && x + 1 <= 9)
                        {
                            boardBot[x + 1, y] = 'S';
                            sboi = 0;
                        }
                    }
                    else if (i >= 5 && i <= 6) // тройной
                    {
                        int v = rnd.Next(0, 3);
                        if (v == 0 && y - 2 >= 0 && y - 2 <= 9)
                        {
                            boardBot[x, y - 1] = 'S';
                            boardBot[x, y - 2] = 'S';
                            sboi = 0;
                        }
                        else if (v == 1 && y + 2 >= 0 && y + 2 <= 9)
                        {
                            boardBot[x, y + 1] = 'S';
                            boardBot[x, y + 2] = 'S';
                            sboi = 0;
                        }
                        else if (v == 2 && x - 2 >= 0 && x - 2 <= 9)
                        {
                            boardBot[x - 2, y] = 'S';
                            boardBot[x - 1, y] = 'S';
                            sboi = 0;
                        }
                        else if (v == 3 && x + 2 >= 0 && x + 2 <= 9)
                        {
                            boardBot[x + 2, y] = 'S';
                            boardBot[x + 1, y] = 'S';
                            sboi = 0;
                        }
                    }
                    else if (i == 7) // четверной
                    {
                        int v = rnd.Next(0, 3);
                        if (v == 0 && y - 3 >= 0 && y - 3 <= 9)
                        {
                            boardBot[x, y - 1] = 'S';
                            boardBot[x, y - 2] = 'S';
                            boardBot[x, y - 3] = 'S';
                            sboi = 0;
                        }
                        else if (v == 1 && y + 3 >= 0 && y + 3 <= 9)
                        {
                            boardBot[x, y + 1] = 'S';
                            boardBot[x, y + 2] = 'S';
                            boardBot[x, y + 3] = 'S';
                            sboi = 0;
                        }
                        else if (v == 2 && x - 3 >= 0 && x - 3 <= 9)
                        {
                            boardBot[x - 2, y] = 'S';
                            boardBot[x - 1, y] = 'S';
                            boardBot[x - 3, y] = 'S';
                            sboi = 0;
                        }
                        else if (v == 3 && x + 3 >= 0 && x + 3 <= 9)
                        {
                            boardBot[x + 2, y] = 'S';
                            boardBot[x + 1, y] = 'S';
                            boardBot[x + 3, y] = 'S';
                            sboi = 0;
                        }
                    }
                }
            }
        }
        public static void ZapolnenieIgrok()
        {
            Console.WriteLine($"Введите номер строчки на которую хотите поставить {currentPlayer}");
            int stroc = int.Parse(Console.ReadLine());
            Console.WriteLine($"Введите номер столбеца на который хотите поставить {currentPlayer}");
            int stolb = int.Parse(Console.ReadLine());
            if (boardIgrok[stroc - 1, stolb - 1] == '~')
                boardIgrok[stroc - 1, stolb - 1] = currentPlayer;
            else
                Console.WriteLine("Нельзя поставить туда куда уже поставили");
        }
        static void PrintGrid() // Вывод поля на экран ЗАКОНЧЕНО
        {
            string probel = "  ";
            Console.WriteLine("   A B C D E F G H I J     A B C D E F G H I J");
            for (int i = 0; i < 10; i++)
            {
                if (i == 9)
                    probel = " ";
                Console.Write((i + 1) + probel);
                for (int j = 0; j < 10; j++)
                    Console.Write(boardIgrok[j, i] + " ");
                Console.Write(" " + (i + 1) + probel);
                for (int j = 0; j < 10; j++)
                    Console.Write(boardBot[j, i] + " ");
                
                Console.WriteLine();
            }
        }
    }
}
