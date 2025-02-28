using System;

namespace morskoi_boi
{
    internal class Program
    {
        #region 
        static char[,] boardBot = new char[10, 10];
        static char[,] boardIgrok = new char[10, 10];
        static char currentPlayer = 'I';
        static bool win = false;
        static Random rnd = new Random();
        #endregion
        static void Main(string[] args)
        {
            InitializeBoard();
            PrintGrid();

        }
        public static void InitializeBoard() // базовые настройки 
        {
            bool initializePerson = false;
            for (int r = 0; r < 2; r++)
            {
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (initializePerson == false)
                            boardBot[i, j] = '~';
                        else if (initializePerson == true)
                            boardIgrok[i, j] = '~';
                    }
                }
                initializePerson = true;
            }
            ZapolnenieBot();
            //ZapolnenieIgrok();
        }
        public static void ZapolnenieBot()
        {
            for (int i = 0; i < 5; i++) // 5 кораблей
            {
                int x = rnd.Next(0, 9);
                int y = rnd.Next(0, 9);
                if (boardBot[x, y] == 'S')
                {
                    i--; 
                    continue;
                }
                boardBot[x, y] = 'S'; 
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
                {
                    Console.Write(boardIgrok[j, i] + " ");
                }
                Console.Write(" " + (i + 1) + probel);
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(boardBot[j, i] + " ");
                }
                
                Console.WriteLine();
            }
        }
    }
}