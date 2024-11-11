using System;
using System.Collections.Generic;

class Program
{
     static Dictionary<int, (string firstName, string lastName, DateTime birthDate,float liquidAccountBalance,float giroAccountBalance,
         float prepaidAccountBalance)> users = new Dictionary<int, (string, string, DateTime, float, float, float)>();


    static void Main()
    {
        users.Add(0, (
           "Josip", "Jovic", new DateTime(1980, 5, 27),
           100.0f,
           0.0f,
           0.0f
       ));
        users.Add(1, (
         "Ivan", "Kopic", new DateTime(2020, 1, 1),
         100.0f,
         0.0f,
         0.0f
     ));
        users.Add(2, (
         "Ana", "Oric", new DateTime(1980, 10, 8),
         100.0f,
         0.0f,
         0.0f
     ));
        users.Add(3, (
         "Toni", "Anikov", new DateTime(1980, 3, 20),
         100.0f,
         0.0f,
         0.0f
     ));
        bool mainMenuRunning = true;
        while (mainMenuRunning)
        {
            Console.Clear();
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
                        BankAccountMenu();
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
            Console.WriteLine("1 - Unos novog korisnika\n2 - Brisanje korisnika\n3 - Uredivanje korisnika\n4 - Pregled korisnika\n5 - Povratak");
            var isInputCorrect = int.TryParse(Console.ReadLine(), out var inputForUserMenu);
            switch (inputForUserMenu)
            {
                case 1:
                    {
                        Console.Clear();
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

    static void UserCreation()
    {
        while (true)
        {
            Console.Clear();
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
                Console.ReadKey();
                continue;
            }
            else
            {
                if (birthDate > DateTime.Now)
                {
                    Console.WriteLine("Datum rođenja ne može biti u budućnosti");
                    Console.ReadKey();
                    continue;
                }
                users.Add(id, (firstName, lastName, birthDate,100.00f,0.00f,0.00f));
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
                        Console.ReadKey();
                    }
                }
            }
        }
    }

    static void UserMenuDeleting()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("a) Po id_u\nb) Po imenu i prezimenu\nc) Povratak");
            var isInputCorrect = char.TryParse(Console.ReadLine(), out var inputForUserMenuDeleting);
            switch (inputForUserMenuDeleting)
            {
                case 'a':
                    {
                        Console.Clear();
                        Console.WriteLine("Unesite id korisnika kojeg zelite izbrisati");
                        var isIdCorrect = int.TryParse(Console.ReadLine(), out var id);
                        if (isIdCorrect)
                        {
                            if (users.ContainsKey(id))
                            {
                                users.Remove(id);
                                Console.WriteLine("Korisnik uspješno izbrisan");
                            }
                            else
                            {
                                Console.WriteLine("Korisnik s tim id-om ne postoji");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Krivi unos, molimo pokusajte ponovo");
                        }
                        Console.ReadKey();
                        break;
                    }
                case 'b':
                    {
                        Console.Clear();
                        Console.Write("Unesite ime korsinika kojeg zelite izbrisati:");
                        var firstNameOfUserToDelete = Console.ReadLine();
                        Console.Write("Unesite prezime korsinika kojeg zelite izbrisati:");
                        var lastNameOfUserToDelete = Console.ReadLine();

                        var usersToDelete = new KeyValuePair<int, (string firstName, string lastName, DateTime birthDate, float liquidAccountBalance, float giroAccountBalance,
                        float prepaidAccountBalance)>();
                        foreach (var user in users)
                        {
                            if (user.Value.firstName == firstNameOfUserToDelete && user.Value.lastName == lastNameOfUserToDelete)
                            {
                                usersToDelete = user;
                            }
                        }
                        if (usersToDelete.Key != 0)
                        {
                            users.Remove(usersToDelete.Key);
                            Console.WriteLine("Korisnik uspješno izbrisan");
                        }
                        else
                        {
                            Console.WriteLine("Korisnik s tim imenom i prezimenom ne postoji");
                        }
                        Console.ReadKey();
                        break;
                    }
                case 'c':
                    {
                        Console.Clear();
                        return;
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

    static void UserMenuEditing()
    {
        //pogledaj kako prominit da ti moze izac iz menija ako korisnik kad unese krivi unos i ne zeli vise unosit
            Console.Clear();
            Console.WriteLine("a) Po id_u\nb) Povratak");
            var isInputCorrect = char.TryParse(Console.ReadLine(), out var inputForUserMenuEditing);
            switch (inputForUserMenuEditing)
            {
                case 'a':
                    {
                        Console.Clear();
                        while (true)
                        {
                            Console.Clear();
                            Console.Write("Upisite id osobe koju zelite izmjeniti:");
                            var isIdCorrect = int.TryParse(Console.ReadLine(), out var id);
                            if (isIdCorrect)
                            {
                                if (users.ContainsKey(id))
                                {
                                    Console.WriteLine("Unesite novo ime korisnika");
                                    var newFirstName = Console.ReadLine();
                                    Console.WriteLine("Unesite novo prezime korisnika");
                                    var newLastName = Console.ReadLine();
                                    Console.WriteLine("Unesite novi datum rođenja korisnika");
                                    var validbirthDate = DateTime.TryParse(Console.ReadLine(), out var newBirthDate);
                                    if (!validbirthDate)
                                    {
                                        Console.WriteLine("Krivi unos, molimo pokušajte ponovo");
                                        Console.ReadKey();
                                        continue;
                                    }
                                    else
                                    {
                                        if (newBirthDate > DateTime.Now)
                                        {
                                            Console.WriteLine("Datum rođenja ne može biti u budućnosti");
                                            Console.ReadKey();
                                            continue;
                                        }
                                        users[id] = (newFirstName, newLastName, newBirthDate, users[id].liquidAccountBalance, users[id].giroAccountBalance, users[id].prepaidAccountBalance);
                                        Console.WriteLine("Korisnik uspješno izmjenjen");
                                        break;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Korisnik s tim id-om ne postoji");
                                    Console.ReadKey();
                                }
                            }
                            else
                            {
                                Console.WriteLine("Krivi unos, molimo pokusajte ponovo");
                                Console.ReadKey();
                                return;
                            }
                        }
                        Console.ReadKey();
                        break;

                    }
                case 'b':
                    {
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
        while (true)
        {
            Console.Clear();
            Console.WriteLine("a) Ispis svih korisnika abecedno po prezimenu\nb) Svih onih koji imaju više od 30 godina\nc) Svih onih koji imaju barem jedan račun u minusu\nd) Povratak");
            var isInputCorrect = char.TryParse(Console.ReadLine(), out var inputForUserMenuOverview);
            switch (inputForUserMenuOverview)
            {
                case 'a':
                    {
                        Console.Clear();
                        foreach (var user in users.OrderBy(user => user.Value.lastName))
                        {
                            Console.WriteLine($"{user.Key} - {user.Value.firstName} - {user.Value.lastName} - {user.Value.birthDate}");
                        }
                        Console.ReadKey();
                        break;
                    }

                case 'b':
                    {
                        Console.Clear();
                        foreach (var user in users)
                        {
                            if (DateTime.Now.Year - user.Value.birthDate.Year > 30)
                            {
                                Console.WriteLine($"{user.Key} - {user.Value.firstName} - {user.Value.lastName} - {user.Value.birthDate}");
                            }
                        }
                        Console.ReadKey();
                        break;
                    }
                case 'c':
                    {
                        Console.Clear();
                        foreach (var user in users)
                        {
                            if (user.Value.liquidAccountBalance < 0 || user.Value.giroAccountBalance < 0 || user.Value.prepaidAccountBalance < 0)
                            {
                                Console.WriteLine($"{user.Key} - {user.Value.firstName} - {user.Value.lastName} - {user.Value.birthDate}");
                            }
                        }
                        Console.ReadKey();
                        break;
                    }
                case 'd':
                    {
                        Console.Clear();
                        return;
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

    
    static void BankAccountMenu()
    {
            Console.Clear();
            Console.Write("Upisite ime korsinika čijim računima želite upravljati:");
            var firstNameOfUser = Console.ReadLine();
            Console.Write("Upisite ime korsinika čijim računima želite upravljati:");
            var lastNameOfUser = Console.ReadLine();
            var foundTheInputedUser = false;
            foreach (var user in users)
            {
                if (user.Value.firstName == firstNameOfUser && user.Value.lastName == lastNameOfUser)
                {
                    foundTheInputedUser = true;
                    bool accountMenuRunning = true;
                    while (accountMenuRunning)
                    {
                        Console.Clear();
                        Console.WriteLine("Odaberite račun u kojem zelite raditi promjene:\n1 - Tekući\n2 - Žiro\n3 - Prepaid\n4 - Povratak");
                        var isInputCorrect = int.TryParse(Console.ReadLine(), out var inputForBankAccountMenu);
                        switch (inputForBankAccountMenu)
                        {
                            case 1:
                                {
                                    Console.Clear();
                                    Console.WriteLine("1");
                                    Console.ReadKey();
                                    break;
                                }
                            case 2:
                                {
                                    Console.Clear();
                                    Console.WriteLine("2");
                                    Console.ReadKey();
                                    break;
                                }
                            case 3:
                                {
                                    Console.Clear();
                                    Console.WriteLine("3");
                                    Console.ReadKey();
                                    break;
                                }
                            case 4:
                                {
                                    accountMenuRunning = false;
                                    break;
                                }
                            default:
                                {
                                    Console.WriteLine("Krivi unos, molimo pokusajte ponovo");
                                    Console.ReadKey();
                                    break;
                                }
                        }
                    }
                }
            }
            if (!foundTheInputedUser)
            {
                Console.WriteLine("Korisnik s upisanim imenom i prezimenom ne postoji u našem sustavu, molimo pokušajte ponovo.");
                Console.ReadKey();
            }
        
    }

    static void BankAccountMenuFunctions()
    {
        Console.Clear();
        Console.WriteLine("1 - Unos nove transakcije\n");
    }

    static void EnteringNewTransaction()
    {
        Console.Clear();
        Console.WriteLine();
    }
}
