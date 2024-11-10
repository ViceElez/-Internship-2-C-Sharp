using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {

        bool mainMenuRunning = true;
        while (mainMenuRunning)
        {
            Console.WriteLine("1 - Korisnici\n2 - Racuni\n3 - Izlaz iz aplikacije");
            var isInputCorrect = int.TryParse(Console.ReadLine(), out var inputForMainMenu);
            switch (inputForMainMenu)
            {
                case 1:
                    {
                        Console.Clear();
                        Console.WriteLine("kor");
                        break;
                    }

                case 2:
                    {
                        Console.Clear();
                        Console.WriteLine("rac");
                        break;
                    }
                case 3:
                    {
                        mainMenuRunning = false;
                        break;
                    }
                default:
                    {
                        Console.Clear();
                        Console.WriteLine("Krivi unos, molimo pokusajte ponovo");
                        break;
                    }
            }
        }

    }
}
