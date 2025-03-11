using System;

namespace morskoi_boi
{
    internal class Program
    {
        #region 
        static char[,] boardBot = new char[10, 10]; 
        static char[,] boardHit = new char[10, 10]; 
        static char[,] boardIgrok = new char[10, 10]; 
        static Random rnd = new Random();
        static int ships1 = 4; 
        static int ships2 = 3; 
        static int ships3 = 2; 
        static int ships4 = 1; 
        static int x = 0;
        static int y = 0;
        #endregion

        static void Main(string[] args)
        {
            InitializeBoard(); 
            PrintGrid(); 
        }
        public static void InitializeBoard()
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
            ZapolnenieIgrok1();
        }
        public static void ZapolnenieBot() // Завершено
        {
            for (int i = 0; i < ships1; i++)
                PlaceShip(1);
            for (int i = 0; i < ships2; i++)
                PlaceShip(2);
            for (int i = 0; i < ships3; i++)
                PlaceShip(3);
            for (int i = 0; i < ships4; i++)
                PlaceShip(4);
        }
        public static void ZapolnenieIgrok1()
        {
            for(int i = 0;i < ships1 + ships2 + ships3 + ships4; i++)
            {
                if (i < ships1)
                    ZapolnenieIgrok2(1);
                else if (i < ships1 + ships2)
                    ZapolnenieIgrok2(2);
                else if (i < ships1 + ships2 + ships3)
                    ZapolnenieIgrok2(3);
                else if (i < ships1 + ships2 + ships3 + ships4)
                    ZapolnenieIgrok2(4);
            }
        } // Завершено
        public static void ZapolnenieIgrok2(int size)
        {
            Console.WriteLine($"Разместите корабль (размер: {size})");
            bool placed = false;

            while (!placed)
            {
                Console.WriteLine("Текущее состояние вашего поля:");
                PrintGrid(); 

                Console.Write("Введите координаты: ");
                string input = Console.ReadLine().ToUpper();
                if (input.Length < 2) continue;

                int x = input[0] - 'A';
                int y = int.Parse(input.Substring(1)) - 1;

                Console.Write("Введите направление (H - горизонтально, V - вертикально): ");
                char direction = Console.ReadLine().ToUpper()[0];

                if (CanPlaceShip(x, y, direction, ))
                {
                    PrintGrid();
                    playerShips.Add(new Ship(size));
                    placed = true;
                }
                else
                {
                    Console.WriteLine("Невозможно разместить корабль здесь. Попробуйте снова.");
                }
            }
        }
        public static void PlaceShip(int length)
        {
            bool placed = false;
            while (!placed)
            {
                x = rnd.Next(0, 10);
                y = rnd.Next(0, 10);
                int direction = rnd.Next(0, 4); 

                if (CanPlaceShip(direction, length))
                {
                    for (int i = 0; i < length; i++)
                    {
                        if (direction == 0) boardBot[x - i, y] = 'S'; 
                        else if (direction == 1) boardBot[x, y + i] = 'S'; 
                        else if (direction == 2) boardBot[x + i, y] = 'S'; 
                        else if (direction == 3) boardBot[x, y - i] = 'S'; 
                    }
                    placed = true;
                    N(direction, length); 
                }
            }
        }

        // Проверка возможности размещения корабля
        public static bool CanPlaceShip(int direction, int length)
        {
            for (int i = 0; i < length; i++)
            {
                int newX = x;
                int newY = y;
                if (direction == 0) newX = x - i;
                else if (direction == 1) newY = y + i;
                else if (direction == 2) newX = x + i;
                else if (direction == 3) newY = y - i; 
                if (newX < 0 || newX >= 10 || newY < 0 || newY >= 10 || boardBot[newX, newY] != '~')
                    return false; 
            }
            return true; // Корабль может быть размещен
        }
        public static void N(int direction, int length)
        {
            for (int i = 0; i < length; i++)
            {
                int newX = x;
                int newY = y;

                if (direction == 0) newX = x - i; 
                else if (direction == 1) newY = y + i; 
                else if (direction == 2) newX = x + i;
                else if (direction == 3) newY = y - i; 

                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        int nx = newX + dx;
                        int ny = newY + dy;

                        if (nx >= 0 && nx < 10 && ny >= 0 && ny < 10 && boardBot[nx, ny] == '~')
                        {
                            boardBot[nx, ny] = 'N'; 
                        }
                    }
                }
            }
        }
        static void PrintGrid()
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
                    Console.Write(boardIgrok[i, j] + " "); // Поле игрока
                }
                Console.Write(" " + (i + 1) + probel);
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(boardBot[i, j] + " "); // Поле бота
                }
                Console.WriteLine();
            }
        }
    }
}