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
                        UserMenu();
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
                     Console.WriteLine("Krivi unos, molimo pokusajte ponovo");
                     Console.ReadKey();
                     Console.Clear();    
                     break;
                    }
            }
        }

    }

    static void UserMenu()
    {
        bool userMenuRunning = true;
        while (userMenuRunning)
        {
            Console.Clear();
            Console.WriteLine("1 - Unos novog korisnika\n2 - Brisanje korisnika\n3 - Uredivanje korisnika\n4 - Pregled korisnika\n5 - Vracanje na glavni meni");
            var isInputCorrect = int.TryParse(Console.ReadLine(), out var inputForUserMenu);
            switch (inputForUserMenu)
            {
                case 1:
                    {
                        UserCreation();
                        break;
                    }

                case 2:
                    {
                        Console.Clear();
                        UserMenuDeleting();
                        break;
                    }
                case 3:
                    {
                        Console.Clear();
                        UserMenuEditing();
                        break;
                    }
                case 4:
                    {
                        Console.Clear();
                        UserMenuOverview();
                        break;
                    }
                case 5:
                    {
                        userMenuRunning = false;
                        Console.Clear();
                        break;
                    }
                default:
                    { 
                        Console.WriteLine("Krivi unos, molimo pokusajte ponovo");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    }
            }
        }
    }

    static void UserMenuDeleting()
    {
        Console.WriteLine("a) Po id_u\nb) Po imenu i prezimenu\nc) Povratak");
        var isInputCorrect = char.TryParse(Console.ReadLine(), out var inputForUserMenuDeleting);
        switch(inputForUserMenuDeleting)
        {
            case 'a':
                {
                    Console.Clear();
                    Console.WriteLine("po id-u");
                    Console.ReadKey();
                    break;
                }
            case 'b':
                {
                    Console.Clear();
                    Console.WriteLine("po imenu i prezimenu");
                    Console.ReadKey();
                    break;
                }
            case 'c':
                {
                    isInputCorrect = false;
                    Console.Clear();
                    break;
                }
            default:
                {
                    Console.WriteLine("Krivi unos, molimo pokusajte ponovo");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                }
        }
    }

    static void UserMenuEditing()
    {
        Console.WriteLine("a) Po id_u\nb) Povratak");
        var isInputCorrect = char.TryParse(Console.ReadLine(), out var inputForUserMenuEditing);
        switch (inputForUserMenuEditing)
        {
            case 'a':
                {
                    Console.Clear();
                    Console.WriteLine("po id-u");
                    Console.ReadKey();
                    break;
                }
            case 'b':
                {
                    isInputCorrect = false;
                    Console.Clear();
                    break;
                }
            default:
                {
                    Console.WriteLine("Krivi unos, molimo pokusajte ponovo");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                }
        } 
    }

    static void UserMenuOverview()
    {
        Console.WriteLine("a) Ispis svih korisnika abecedno po prezimenu\nb) Svih onih koji imaju više od 30 godina\nc) Svih onih koji imaju barem jedan račun u minusu\nd) Povratak");
        var isInputCorrect = char.TryParse(Console.ReadLine(), out var inputForUserMenuOverview);
        switch(inputForUserMenuOverview)
        {
            case 'a':
                {
                    Console.Clear();
                    Console.WriteLine("po prezimenu");
                    Console.ReadKey();
                    break;
                }
            case 'b':
                {
                    Console.Clear();
                    Console.WriteLine("stariji od 30");
                    Console.ReadKey();
                    break;
                }
            case 'c':
                {
                    Console.Clear();
                    Console.WriteLine("minus");
                    Console.ReadKey();
                    break;
                }
            case 'd':
                {
                    isInputCorrect = false;
                    Console.Clear();
                    break;
                }
            default:
                {
                    Console.WriteLine("Krivi unos, molimo pokusajte ponovo");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                }
        }


    }

    static void UserCreation()
    {
        Console.Clear();
        Dictionary<int, (string firstName, string lastName, DateTime birthDate)> users = new Dictionary<int, (string, string, DateTime)>();
        while (true)
        {
            Console.WriteLine("Unesite ime korisnika");
            var firstName = Console.ReadLine();
            Console.WriteLine("Unesite prezime korisnika");
            var lastName = Console.ReadLine();
            Console.WriteLine("Unesite datum rođenja korisnika");
            var validbirthDate = DateTime.TryParse(Console.ReadLine(), out var birthDate);
            var id = users.Count + 1;
            if (!validbirthDate)
            {
                Console.WriteLine("Krivi unos, molimo pokušajte ponovo");
                continue;
            }
            else
            {
                if (birthDate > DateTime.Now)
                {
                    Console.WriteLine("Datum rođenja ne može biti u budućnosti");
                    continue;
                }
                users.Add(id, (firstName, lastName, birthDate));
                Console.WriteLine("Korisnik uspješno dodan");
                while (true)
                {
                    Console.WriteLine("Želite li dodati još korisnika? (da/ne)");
                    var answer = Console.ReadLine().ToLower();
                    if (answer == "da")
                    {
                        break;
                    }
                    else if (answer == "ne")
                    {
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Krivi unos, molimo pokušajte ponovo");
                    }
                }
            }
        }
    }
}
