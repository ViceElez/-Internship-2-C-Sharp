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
                        Console.WriteLine("unos novog kor");
                        Console.ReadKey();
                        Console.Clear();
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



}
