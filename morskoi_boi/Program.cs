using System;
using System.Collections.Generic;
using System.Linq;

namespace morskoi_boi
{
    internal class Program
    {
        static MyKart p = new MyKart();
        static void Main(string[] args)
        {
            Console.WriteLine("Введите свои корабли:");

            AddBoatWithCheck("Введи координаты 4-х палубного. Пример: А1 Б1 В1 Г1", 4);
            AddBoatWithCheck("Введи координаты 3-х палубного. Пример: А1 Б1 В1", 3);
            AddBoatWithCheck("Введи координаты 3-х палубного. Пример: А1 Б1 В1", 3);
            AddBoatWithCheck("Введи координаты 2-х палубного. Пример: А1 Б1", 2);
            AddBoatWithCheck("Введи координаты 2-х палубного. Пример: А1 Б1", 2);
            AddBoatWithCheck("Введи координаты 2-х палубного. Пример: А1 Б1", 2);
            AddBoatWithCheck("Введи координаты 1-х палубного. Пример: А1", 1);
            AddBoatWithCheck("Введи координаты 1-х палубного. Пример: А1", 1);
            AddBoatWithCheck("Введи координаты 1-х палубного. Пример: А1", 1);
            AddBoatWithCheck("Введи координаты 1-х палубного. Пример: А1", 1);

            Bot bot = new Bot();
            List<Boat> botboats = bot.botboat();

            MyKart botkart = new MyKart();
            foreach (Boat boat in botboats)
                botkart.AddBoat(boat);

            botkart.Print(false);
            while (botkart.Alivefleet() && p.Alivefleet())
            {
                int fireresult = 1;
                while (fireresult != 0 && botkart.Alivefleet())
                {

                    Console.WriteLine("Сделайте ход");
                    string hod = Console.ReadLine();
                    fireresult = botkart.Fire(hod);

                    if (fireresult == 1)
                    {
                        Console.WriteLine("Вы попали");

                    }
                    else if (fireresult == 0)
                    {
                        Console.WriteLine("мимо");
                    }
                    else if (fireresult == -1)
                    {
                        Console.WriteLine("Вы потапили");

                    }
                    botkart.Print(false);
                }

                int botFireResult = 1;
                while (botFireResult != 0 && p.Alivefleet())
                {
                    Console.WriteLine("Соперник делает ход");
                    botFireResult = p.Fire(bot.hod());
                    if (botFireResult == 1)
                    {
                        Console.WriteLine("В вас попали");
                    }
                    else if (botFireResult == 0)
                    {
                        Console.WriteLine("мимо");
                    }
                    else if (botFireResult == -1)
                    {
                        Console.WriteLine("Вы потеряли корабль");

                    }
                    p.Print(true);

                }
            }


            if (botkart.Alivefleet())
            {
                Console.WriteLine("Победил соперник");
                Console.ReadKey();
            }
            if (p.Alivefleet())
            {
                Console.WriteLine("Вы победили!");
                Console.ReadKey();
            }
        }
        static void AddBoatWithCheck(string prompt, int expectedLength)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                string input = Console.ReadLine().ToUpper();
                string[] coords = input.Split(' ');

                if (coords.Length != expectedLength)
                {
                    Console.WriteLine($"Неверное количество координат. Ожидается {expectedLength}.");
                    continue;
                }

                Boat boat = new Boat(coords);
                if (p.AddBoat(boat))
                {
                    p.Print(true);
                    break;
                }
                else
                    Console.WriteLine("Нельзя разместить корабль здесь или рядом с другим кораблем");
            }
        }
    }
    internal class MyKart
    {
        Dictionary<string, bool> history = new Dictionary<string, bool>();
        List<Boat> myBoats;
        public MyKart()
        {
            myBoats = new List<Boat>();
        }
        public bool AddBoat(Boat boat)
        {

            if (!IsValidPlacement(boat))
            {
                return false;
            }

            myBoats.Add(boat);
            return true;
        }
        private bool IsValidPlacement(Boat newBoat)
        {

            HashSet<string> forbiddenCells = new HashSet<string>();

            foreach (Boat existingBoat in myBoats)
            {
                foreach (string cell in existingBoat.Cord())
                {

                    AddCellAndNeighbors(forbiddenCells, cell);
                }
            }


            foreach (string newCell in newBoat.Cord())
            {
                if (forbiddenCells.Contains(newCell))
                {
                    return false;
                }
            }

            return true;
        }
        private void AddCellAndNeighbors(HashSet<string> forbiddenCells, string cell)
        {
            Dictionary<string, int> cord = new Dictionary<string, int>
            {
                {"A", 0}, {"B", 1}, {"C", 2}, {"D", 3}, {"E", 4},
                {"F", 5}, {"G", 6}, {"H", 7}, {"I", 8}, {"J", 9}
            };


            string letter = cell.Substring(0, 1);
            int number;
            if (cell.Length == 2)
            {
                number = int.Parse(cell.Substring(1, 1));
            }
            else
            {
                number = int.Parse(cell.Substring(1, 2));
            }


            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int newNumber = number + i;
                    int letterIndex = cord[letter] + j;


                    if (newNumber >= 1 && newNumber <= 10 && letterIndex >= 0 && letterIndex < 10)
                    {
                        string newLetter = cord.FirstOrDefault(x => x.Value == letterIndex).Key;
                        string newCell = newLetter + newNumber;
                        forbiddenCells.Add(newCell);
                    }
                }
            }
        }
        private string Getkey(Dictionary<string, int> cord, int value)
        {
            foreach (var pair in cord)
            {
                if (pair.Value == value)
                {
                    return pair.Key;
                }
            }
            return " ";
        }
        public void Print(bool showboat)
        {
            string[,] Karta = new string[10, 10];
            Dictionary<string, int> cord = new Dictionary<string, int>
            {
                {"A", 0}, {"B", 1}, {"C", 2}, {"D", 3}, {"E", 4},
                {"F", 5}, {"G", 6}, {"H", 7}, {"I", 8}, {"J", 9}
            };

            for (int s = 0; s < 10; s++)
            {
                for (int k = 0; k < 10; k++)
                {
                    Karta[s, k] = "-";
                    if (showboat)
                    {
                        foreach (Boat boat in myBoats)
                        {
                            string[] acord = boat.Cord();
                            for (int n = 0; n < acord.Length; n++)
                            {
                                string bykva = acord[n].Substring(0, 1);
                                int colindex = cord[bykva];
                                int stroka;
                                if (acord[n].Length == 2)
                                {
                                    stroka = int.Parse(acord[n].Substring(1, 1)) - 1;
                                }
                                else
                                {
                                    stroka = int.Parse(acord[n].Substring(1, 2)) - 1;
                                }
                                if (s == stroka && colindex == k)
                                {
                                    Karta[s, k] = "S";
                                    break;
                                }
                            }
                        }
                    }

                    string kartcord = Getkey(cord, k) + (s + 1);
                    if (history.ContainsKey(kartcord))
                    {
                        if (history[kartcord])
                        {
                            Karta[s, k] = "x";
                        }
                        else
                            Karta[s, k] = "*";
                    }
                }
            }

            Console.WriteLine("   A B C D E F G H I J");
            for (int s = 0; s < 10; s++)
            {
                if (s < 9)
                {
                    Console.Write(" ");
                }
                Console.Write(s + 1 + " ");

                for (int k = 0; k < 10; k++)
                {
                    Console.Write(Karta[s, k] + " ");
                }
                Console.WriteLine();
            }
        }
        public int Fire(string firecord)
        {
            foreach (Boat boat in myBoats)
            {
                string[] cordboat = boat.Cord();
                for (int i = 0; i < cordboat.Length; i++)
                {
                    if (firecord == cordboat[i])
                    {
                        history.Add(firecord, true);
                        int lifeboat = boat.Damage();
                        if (lifeboat > 0)
                        {
                            return 1;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                }
            }
            history.Add(firecord, false);
            return 0;
        }
        public bool Alivefleet()
        {
            foreach (Boat boat in myBoats)
            {
                if (boat.IsAlive())
                {
                    return true;
                }
            }
            return false;
        }
    }
    internal class Bot
    {
        List<string> historybot = new List<string>();
        char[] bykv = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
        public List<Boat> botboat()
        {

            List<Boat> boats = new List<Boat>();
            boats.Add(createboat(4));
            Boat boat3_1 = createboat(3);
            while (intersec(boat3_1, boats))
            {
                boat3_1 = createboat(3);
            }
            boats.Add(boat3_1);
            Boat boat3_2 = createboat(3);
            while (intersec(boat3_2, boats))
            {
                boat3_2 = createboat(3);
            }
            boats.Add(boat3_2);
            Boat boat2_1 = createboat(2);
            while (intersec(boat2_1, boats))
            {
                boat2_1 = createboat(2);
            }
            boats.Add(boat2_1);

            Boat boat2_2 = createboat(2);
            while (intersec(boat2_2, boats))
            {
                boat2_2 = createboat(2);
            }
            boats.Add(boat2_2);

            Boat boat2_3 = createboat(2);
            while (intersec(boat2_3, boats))
            {
                boat2_3 = createboat(2);
            }
            boats.Add(boat2_3);

            Boat boat1_1 = createboat(1);
            while (intersec(boat1_1, boats))
            {
                boat1_1 = createboat(1);
            }
            boats.Add(boat1_1);

            Boat boat1_2 = createboat(1);
            while (intersec(boat1_2, boats))
            {
                boat1_2 = createboat(1);
            }
            boats.Add(boat1_2);

            Boat boat1_3 = createboat(1);
            while (intersec(boat1_3, boats))
            {
                boat1_3 = createboat(1);
            }
            boats.Add(boat1_3);

            Boat boat1_4 = createboat(1);
            while (intersec(boat1_4, boats))
            {
                boat1_4 = createboat(1);
            }
            boats.Add(boat1_4);
            return boats;
        }
        private char compass(string startcoordinat, int size)
        {

            List<char> nesw = new List<char>();
            int n_endcoordinat = Int32.Parse(startcoordinat.Substring(1)) - (size - 1);
            if (n_endcoordinat >= 1)
            {
                nesw.Add('n');
            }
            int s_endcoordinat = Int32.Parse(startcoordinat.Substring(1)) + (size - 1);
            if (s_endcoordinat <= 10)
            {
                nesw.Add('s');
            }
            int e_endcoordinat = Array.IndexOf(bykv, startcoordinat.Substring(0, 1)[0]) + (size - 1);
            if (e_endcoordinat < bykv.Length)
            {
                nesw.Add('e');
            }
            int w_endcoordinat = Array.IndexOf(bykv, startcoordinat.Substring(0, 1)[0]) - (size - 1);
            if (w_endcoordinat >= 0)
            {
                nesw.Add('w');
            }
            Random rand = new Random();
            int randomindex = rand.Next(0, nesw.Count);
            return nesw[randomindex];

        }
        private Boat createboat(int size)
        {
            string[] coordinat = new string[size];


            Random random = new Random();
            int chislo = random.Next(1, 10);

            int randomindex = random.Next(0, 9);
            char randomChar = bykv[randomindex];



            string startcoordinat = "" + randomChar + chislo;
            coordinat[0] = startcoordinat;
            if (size > 1)
            {
                char naprav = compass(startcoordinat, size);

                switch (naprav)
                {
                    case 'n':
                        for (int i = 1; i < coordinat.Length; i++)
                        {
                            coordinat[i] = "" + randomChar + (chislo - i);

                        }
                        break;
                    case 'e':
                        for (int i = 1; i < coordinat.Length; i++)
                        {
                            coordinat[i] = "" + bykv[randomindex + i] + chislo;
                        }
                        break;
                    case 's':
                        for (int i = 1; i < coordinat.Length; i++)
                        {
                            coordinat[i] = "" + randomChar + (chislo + i);
                        }
                        break;
                    case 'w':
                        for (int i = 1; i < coordinat.Length; i++)
                        {
                            coordinat[i] = "" + bykv[randomindex - i] + chislo;
                        }
                        break;
                }
            }

            Boat randboat = new Boat(coordinat);

            return randboat;
        }
        private bool intersec(Boat boat, List<Boat> boats)
        {
            foreach (Boat boat2 in boats)
            {
                for (int i = 0; i < boat.Cord().Length; i++)

                {
                    for (int j = 0; j < boat2.Cord().Length; j++)
                    {
                        if (boat.Cord()[i] == boat2.Cord()[j])
                        {
                            return true;
                        }
                    }
                }
            }
            return false;

        }
        public string hod()
        {
            string hod = " ";
            while (hod == " " || historybot.Contains(hod))
            {
                Random random = new Random();
                int chislo = random.Next(0, 9);

                int randomindex = random.Next(0, 9);
                char randomChar = bykv[randomindex];
                hod = "" + randomChar + chislo;

            }

            historybot.Add(hod);
            return hod;
        }
    }
    internal class Boat
    {
        string[] coordinat;
        int life;
        public Boat(string[] coordinatboat)
        {
            coordinat = coordinatboat;
            life = coordinat.Length;
        }
        public string[] Cord()
        { return coordinat; }
        public int Damage()
        { life--; return life; }
        public bool IsAlive()
        {
            if (life > 0)
                return true;
            else
                return false;
        }
    }
}
