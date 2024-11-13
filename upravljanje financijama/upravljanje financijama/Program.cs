using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Security.Principal;
using System.Transactions;

class Program
{
    static Dictionary<int, (string firstName, string lastName, DateTime birthDate,
   Dictionary<string, float> accounts)> users = new Dictionary<int, (string, string, DateTime, Dictionary<string, float>)>();

    static Dictionary<int, Dictionary<string, List<(int transactionId, float transactionAmount,
        string transactionDescription, string transactionType, string transactionCategory, DateTime transactionTime)>>> transactions =
        new Dictionary<int, Dictionary<string, List<(int, float, string, string, string, DateTime)>>>();


    static void Main()
    {
        users.Add(0, (
           "Josip", "Jovic", new DateTime(1980, 5, 27),
           new Dictionary<string, float> { { "Tekući", 100.0f }, { "Žiro", 0.0f }, { "Prepaid", 0.0f } }
       ));
        users.Add(1, (
            "Ivan", "Kopic", new DateTime(2020, 1, 1),
            new Dictionary<string, float> { { "Tekući", 100.0f }, { "Žiro", 0.0f }, { "Prepaid", 0.0f } }
        ));
        users.Add(2, (
            "Ana", "Oric", new DateTime(1980, 10, 8),
            new Dictionary<string, float> { { "Tekući", 100.0f }, { "Žiro", 0.0f }, { "Prepaid", 0.0f } }
        ));
        users.Add(3, (
            "Toni", "Anikov", new DateTime(1980, 3, 20),
            new Dictionary<string, float> { { "Tekući", 100.0f }, { "Žiro", 0.0f }, { "Prepaid", 0.0f } }
        ));

        transactions.Add(0, new Dictionary<string, List<(int, float, string, string, string, DateTime)>> {
    { "Tekući", new List<(int, float, string, string, string, DateTime)> {
        (1, 150.0f, "Birthday gift", "Prihod", "Poklon", new DateTime(2024, 1, 15)),
        (2, 75.0f, "Groceries", "Rashod", "Hrana", new DateTime(2024, 2, 10))
    }},
    { "Žiro", new List<(int, float, string, string, string, DateTime)> {
        (3, 1200.0f, "Monthly salary", "Prihod", "Placa", new DateTime(2024, 3, 1))
    }},
    { "Prepaid", new List<(int, float, string, string, string, DateTime)> {
        (4, 20.0f, "Gym membership", "Rashod", "Sport", new DateTime(2024, 4, 5))
    }}
});

        transactions.Add(1, new Dictionary<string, List<(int, float, string, string, string, DateTime)>> {
    { "Tekući", new List<(int, float, string, string, string, DateTime)> {
        (5, 60.0f, "Sports equipment", "Rashod", "Sport", new DateTime(2024, 5, 15))
    }},
    { "Žiro", new List<(int, float, string, string, string, DateTime)> {
        (6, 900.0f, "Monthly salary", "Prihod", "Placa", new DateTime(2024, 5, 20))
    }},
    { "Prepaid", new List<(int, float, string, string, string, DateTime)> {
        (7, 30.0f, "Transportation fees", "Rashod", "Prijevoz", new DateTime(2024, 5, 22))
    }}
});

        transactions.Add(2, new Dictionary<string, List<(int, float, string, string, string, DateTime)>> {
    { "Tekući", new List<(int, float, string, string, string, DateTime)> {
        (8, 90.0f, "Restaurant dinner", "Rashod", "Hrana", new DateTime(2024, 6, 10))
    }},
    { "Žiro", new List<(int, float, string, string, string, DateTime)> {
        (9, 1050.0f, "Government subsidy", "Prihod", "Poticaj", new DateTime(2024, 6, 25))
    }},
    { "Prepaid", new List<(int, float, string, string, string, DateTime)> {
        (10, 45.0f, "Cosmetics purchase", "Rashod", "Kozmetika", new DateTime(2024, 6, 15))
    }}
});

        transactions.Add(3, new Dictionary<string, List<(int, float, string, string, string, DateTime)>> {
    { "Tekući", new List<(int, float, string, string, string, DateTime)> {
        (11, 120.0f, "Fuel", "Rashod", "Prijevoz", new DateTime(2024, 7, 5))
    }},
    { "Žiro", new List<(int, float, string, string, string, DateTime)> {
        (12, 1150.0f, "Salary payment", "Prihod", "Placa", new DateTime(2024, 7, 30))
    }},
    { "Prepaid", new List<(int, float, string, string, string, DateTime)> {
        (13, 25.0f, "Thank you gift", "Prihod", "Zahvala", new DateTime(2024, 7, 12))
    }}
});

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
                users.Add(id, (firstName, lastName, birthDate, new Dictionary<string, float> { { "liquidAccountBalance", 100.0f }, { "giroAccountBalance", 0.0f }, { "prepaidAccountBalance", 0.0f } }));
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

                        var usersToDelete = new KeyValuePair<int, (string, string, DateTime, Dictionary<string, float>)>();
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
        while (true) {
            Console.Clear();
            Console.WriteLine("a) Po id_u\nb) Povratak");
            var isInputCorrect = char.TryParse(Console.ReadLine(), out var inputForUserMenuEditing);
            switch (inputForUserMenuEditing)
            {
                case 'a':
                    {
                        Console.Clear();
                        Console.Clear();
                        Console.Write("Upisite id osobe koju zelite izmjeniti:");
                        var isIdCorrect = int.TryParse(Console.ReadLine(), out var id);
                        if (isIdCorrect)
                        {
                            if (users.ContainsKey(id))
                            {
                                var user = users[id];
                                Console.WriteLine("Unesite novo ime korisnika(ili Enter ako ne zelite mijenjati):");
                                var newFirstName = Console.ReadLine();
                                if (!string.IsNullOrWhiteSpace(newFirstName))
                                {
                                    user.firstName = newFirstName;
                                }

                                Console.WriteLine("Unesite novo prezime korisnika(ili Enter ako ne zelite mijenjati):");
                                var newLastName = Console.ReadLine();
                                if (!string.IsNullOrWhiteSpace(newLastName))
                                {
                                    user.lastName = newLastName;
                                }

                                Console.WriteLine("Unesite novi datum(ili Enter ako ne zelite mijenjati): ");
                                var newDateTime = Console.ReadLine();
                                while (true)
                                {
                                    if (!string.IsNullOrWhiteSpace(newDateTime))
                                    {
                                        var isDateTimeCorrect = DateTime.TryParse(newDateTime, out var newDateTimeBirth);
                                        if (isDateTimeCorrect)
                                        {
                                            if (newDateTimeBirth > DateTime.Now)
                                            {
                                                Console.WriteLine("Datum rođenja ne može biti u budućnosti");
                                                Console.ReadKey();
                                            }
                                            user.birthDate = newDateTimeBirth;
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Krivi unos, molimo pokusajte ponovo.");
                                            Console.WriteLine("Unesite novi datum(ili Enter ako ne zelite mijenjati): ");
                                            newDateTime = Console.ReadLine();
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                users[id] = user;
                                Console.WriteLine("Korisnik uspjesno izmjenjen.");

                            }
                            else
                            {
                                Console.WriteLine("Korisnik s tim id-om ne postoji");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Krivi unos, molimo pokusajte ponovo");
                            Console.ReadKey();
                            break; ;
                        }
                        Console.ReadKey();
                        break;

                    }
                case 'b':
                    {
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
                            if (user.Value.accounts["liquidAccountBalance"] < 0 || user.Value.accounts["giroAccountBalance"] < 0 || user.Value.accounts["prepaidAccountBalance"] < 0)
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
                                BankAccountMenuFunctions(firstNameOfUser, lastNameOfUser, "Tekući");
                                break;
                            }
                        case 2:
                            {
                                Console.Clear(); 
                                BankAccountMenuFunctions(firstNameOfUser, lastNameOfUser, "Žiro");
                                break;
                            }
                        case 3:
                            {
                                Console.Clear();
                                BankAccountMenuFunctions(firstNameOfUser, lastNameOfUser, "Prepaid");
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

    static void BankAccountMenuFunctions(string firstNameOfInputedUser, string lastNameOfInputedUser, string inputForBankAccountMenu)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("1 - Unos nove transakcije\n2 - Brisanje transakcije\n3 - Uređivanje transakcije\n4 - Pregled transakcija\n5 - Financijsko izvješće\n6 - Povratak");
            var isInputCorrect = int.TryParse(Console.ReadLine(), out var inputForBankAccountMenuFunctions);
            int IdOfInputedUser = findIdUsingName(firstNameOfInputedUser, lastNameOfInputedUser);
            switch (inputForBankAccountMenuFunctions)
            {
                case 1:
                    {
                        EnteringNewTransaction(IdOfInputedUser, inputForBankAccountMenu);
                        break;
                    }
                case 2:
                    {
                        Console.Clear();
                        DeletingTransactions(IdOfInputedUser, inputForBankAccountMenu);
                        break;
                    }
                case 3:
                    {
                        Console.Clear();
                        EditingTransactions(IdOfInputedUser, inputForBankAccountMenu);
                        break;
                    }
                case 4:
                    {
                        Console.Clear();
                        OverviewOfTransactions(IdOfInputedUser, inputForBankAccountMenu);
                        break;
                    }
                case 5:
                    {
                        Console.Clear();
                        FinancialSummary(IdOfInputedUser, inputForBankAccountMenu);
                        break;
                    }
                case 6:
                    {
                        return;
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
    static int findIdUsingName(string firstName, string lastName)
    {
        foreach (var user in users)
        {
            if (user.Value.firstName == firstName && user.Value.lastName == lastName)
            {
                return user.Key;
            }
        }
        return -1;
    }

    static void EnteringNewTransaction(int IdOfInputedUser, string inputForBankAccountMenu)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("a) Trenutno izvršena transakcija (po defaultu trenutni datum i vrijeme)\nb) Ranije izvršena transakcija (potrebno je upisati datum i vrijeme)\nc) Povratak");
            var isInputCorrect = char.TryParse(Console.ReadLine(), out var inputForEnteringNewTransaction);
            switch (inputForEnteringNewTransaction)
            {
                case 'a':
                    {
                        Console.Clear();
                        SubFunctionEnteringNewTransaction(DateTime.Now, IdOfInputedUser, inputForBankAccountMenu);
                        break;
                    }

                case 'b':
                    {
                        Console.Clear();
                        Console.Write("Upisite datum vaše transakcije:");
                        var isValidTransactionDate = DateTime.TryParse(Console.ReadLine(), out var transactionDate);
                        if (!isValidTransactionDate)
                        {
                            Console.WriteLine("Krivi unos, molimo pokušajte ponovo");
                            Console.ReadKey();
                            continue;
                        }
                        else
                        {
                            if (transactionDate > DateTime.Now)
                            {
                                Console.WriteLine("Transakcija ne može biti u budućnosti, pokusajte ponovo:");

                                Console.ReadKey();
                                continue;
                            }

                            SubFunctionEnteringNewTransaction(transactionDate, IdOfInputedUser, inputForBankAccountMenu);
                        }
                        break;
                    }
                case 'c':
                    {
                        return;
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
    static void SubFunctionEnteringNewTransaction(DateTime inputedDateTime, int IdOfInputedUser, string inputForBankAccountMenu)
    {
        Console.Write("Upišite iznos transakcije:");
        var isTransactionAmountCorrect = float.TryParse(Console.ReadLine(), out var transactionAmount);
        if (isTransactionAmountCorrect && transactionAmount >= 0)
        {
            Console.Write("Upišite opis transakcije:");
            var transactionDescription = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(transactionDescription))
            {
                transactionDescription = "Standardna transakcija";
            }
            Console.Write("Upišite tip transakcije (prihod/rashod):");
            var transactionType = Console.ReadLine().ToLower();
            while (true)
            {
                if (transactionType != "prihod" && transactionType != "rashod")
                {
                    Console.WriteLine("Krivi unos, molimo pokušajte ponovo.");
                    Console.Write("Upišite tip transakcije (prihod/rashod):");
                    transactionType = Console.ReadLine().ToLower();
                    continue;
                }
                else
                {
                    break;
                }

            }
            var transactionCategory = string.Empty;

            if (transactionType == "prihod")
            {
                Console.Write("Upišite kategoriju transakcije (placa/poklon/poticaj/zahvala):");
                transactionCategory = Console.ReadLine().ToLower();
                while (true)
                {
                    if (transactionCategory != "placa" && transactionCategory != "poklon" && transactionCategory != "poticaj" && transactionCategory != "zahvala")
                    {
                        Console.WriteLine("Krivi unos, molimo pokušajte ponovo.");
                        Console.Write("Upišite kategoriju transakcije (placa/poklon/poticaj/zahvala):");
                        transactionCategory = Console.ReadLine().ToLower();
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                Console.Write("Upišite kategoriju transakcije (hrana/sport/prijevoz/kozmetika):");
                transactionCategory = Console.ReadLine().ToLower();
                while (true)
                {
                    if (transactionCategory != "hrana" && transactionCategory != "prijevoz" && transactionCategory != "kozmetika" && transactionCategory != "sport")
                    {
                        Console.WriteLine("Krivi unos, molimo pokušajte ponovo");
                        Console.Write("Upišite kategoriju transakcije (hrana/sport/prijevoz/kozmetika):");
                        transactionCategory = Console.ReadLine().ToLower();
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }

            }
            var transactionTime = inputedDateTime;
            if (!transactions.ContainsKey(IdOfInputedUser))
            {
                transactions[IdOfInputedUser] = new Dictionary<string, List<(int, float, string, string, string, DateTime)>>();
            }
            if (!transactions[IdOfInputedUser].ContainsKey(inputForBankAccountMenu))
            {
                transactions[IdOfInputedUser][inputForBankAccountMenu] = new List<(int, float, string, string, string, DateTime)>();
            }
            var transactionId = transactions[IdOfInputedUser][inputForBankAccountMenu].Count + 1;
            var transaction = (transactionId, transactionAmount, transactionDescription, transactionType, transactionCategory, transactionTime);
            transactions[IdOfInputedUser][inputForBankAccountMenu].Add(transaction);
            Console.WriteLine("Transakcija je uspješna!");
            Console.ReadKey();
        }


        else
        {
            Console.WriteLine("Nevaljan iznos transakcije, pokušajte ponovo.");
            Console.ReadKey();
        }
    }
    static void DeletingTransactions(int IdOfInputedUser, string inputForBankAccountMenu)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("a) Po id-u\nb) Ispod unesenog iznosa\nc) Iznad unesenog iznosa \nd) Svih prihoda\n" +
                "e) Svih rashoda\nf) Svih transakcija za odabranu kategoriju\ng) Povratak");
            var isInputCorrect = char.TryParse(Console.ReadLine(), out var inputForDeletingTransaction);
            switch (inputForDeletingTransaction)
            {
                case 'a':
                    {
                        Console.Write("Unesite id transakcije koju zelite izbrisati:");
                        var isAmountCorrect = float.TryParse(Console.ReadLine(), out var idToDelete);
                        bool foundTransactionToDelete = false;
                        if (isAmountCorrect)
                        {
                            foreach (var account in transactions[IdOfInputedUser])
                            {
                                if (account.Key == inputForBankAccountMenu)
                                {
                                    var transactionsToDelete = account.Value.Where(transaction => transaction.transactionId == idToDelete).ToList();

                                    foreach (var transaction in transactionsToDelete)
                                    {
                                        Console.WriteLine($"{transaction.transactionId} - {transaction.transactionAmount} - {transaction.transactionDescription} - {transaction.transactionType} - {transaction.transactionCategory} - {transaction.transactionTime}");
                                        Console.WriteLine("Da li stvarno želite izbrisati ovu transakciju (da/ne)?");
                                        var answer = string.Empty;
                                        do
                                        {
                                            answer = Console.ReadLine().ToLower();
                                            if (answer == "da")
                                            {
                                                account.Value.Remove(transaction);
                                                Console.WriteLine("Transakcija uspješno izbrisana");
                                                foundTransactionToDelete = true;
                                            }
                                            else if (answer == "ne")
                                            {
                                                Console.WriteLine("Transakcija nije izbrisana");
                                                foundTransactionToDelete = true;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Morate upisati da ili ne");
                                                continue;
                                            }
                                        } while (answer != "da" && answer != "ne");
                                    }
                                }
                            }
                            if (!foundTransactionToDelete)
                            {
                                Console.WriteLine($"Nema transakcija s id-om: {idToDelete}.");
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
                        Console.Write("Unesite iznos ispod kojeg zelite izbrisati:");
                        var isAmountCorrect = float.TryParse(Console.ReadLine(), out var amountUnderWhichIsDeleted);
                        bool foundTransactionToDelete = false;
                        if (isAmountCorrect && amountUnderWhichIsDeleted >= 0)
                        {
                            foreach (var account in transactions[IdOfInputedUser])
                            {
                                if (account.Key == inputForBankAccountMenu)
                                {
                                    var transactionsToDelete = account.Value.Where(transaction => transaction.transactionAmount < amountUnderWhichIsDeleted).ToList();

                                    foreach (var transaction in transactionsToDelete)
                                    {
                                        Console.WriteLine($"{transaction.transactionId} - {transaction.transactionAmount} - {transaction.transactionDescription} - {transaction.transactionType} - {transaction.transactionCategory} - {transaction.transactionTime}");
                                        Console.WriteLine("Da li stvarno želite izbrisati ovu transakciju (da/ne)?");
                                        var answer = string.Empty;
                                        do
                                        {
                                            answer = Console.ReadLine().ToLower();
                                            if (answer == "da")
                                            {
                                                account.Value.Remove(transaction);
                                                Console.WriteLine("Transakcija uspješno izbrisana");
                                                foundTransactionToDelete = true;
                                            }
                                            else if (answer == "ne")
                                            {
                                                Console.WriteLine("Transakcija nije izbrisana");
                                                foundTransactionToDelete = true;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Morate upisati da ili ne");
                                                continue;
                                            }
                                        } while (answer != "da" && answer != "ne");
                                    }
                                }
                            }
                            if (!foundTransactionToDelete)
                            {
                                Console.WriteLine($"Nema transakcija ispod {amountUnderWhichIsDeleted:F2}.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Krivi unos, molimo pokusajte ponovo");
                        }


                        Console.ReadKey();
                        break;
                    }
                case 'c':
                    {
                        Console.Write("Unesite iznos iznad kojeg zelite izbrisati:");
                        var isAmountCorrect = float.TryParse(Console.ReadLine(), out var amountAboveWhichIsDeleted);
                        bool foundTransactionToDelete = false;
                        if (isAmountCorrect && amountAboveWhichIsDeleted >= 0)
                        {
                            foreach (var account in transactions[IdOfInputedUser])
                            {
                                if (account.Key == inputForBankAccountMenu)
                                {
                                    var transactionsToDelete = account.Value.Where(transaction => transaction.transactionAmount > amountAboveWhichIsDeleted).ToList();

                                    foreach (var transaction in transactionsToDelete)
                                    {
                                        Console.WriteLine($"{transaction.transactionId} - {transaction.transactionAmount} - {transaction.transactionDescription} - {transaction.transactionType} - {transaction.transactionCategory} - {transaction.transactionTime}");
                                        Console.WriteLine("Da li stvarno želite izbrisati ovu transakciju (da/ne)?");
                                        var answer = string.Empty;
                                        do
                                        {
                                            answer = Console.ReadLine().ToLower();
                                            if (answer == "da")
                                            {
                                                account.Value.Remove(transaction);
                                                Console.WriteLine("Transakcija uspješno izbrisana");
                                                foundTransactionToDelete = true;
                                            }
                                            else if (answer == "ne")
                                            {
                                                Console.WriteLine("Transakcija nije izbrisana");
                                                foundTransactionToDelete = true;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Morate upisati da ili ne");
                                                continue;
                                            }
                                        } while (answer != "da" && answer != "ne");
                                    }
                                }
                            }
                            if (!foundTransactionToDelete)
                            {
                                Console.WriteLine($"Nema transakcija ispod {amountAboveWhichIsDeleted:F2}.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Krivi unos, molimo pokusajte ponovo");
                        }
                        Console.ReadKey();
                        break;

                    }
                case 'd':
                    {
                        bool foundTransactionToDelete = false;

                        foreach (var account in transactions[IdOfInputedUser])
                        {
                            if (account.Key == inputForBankAccountMenu)
                            {
                                var transactionsToDelete = account.Value.Where(transaction => transaction.transactionType.ToLower() == "prihod").ToList();

                                foreach (var transaction in transactionsToDelete)
                                {
                                    Console.WriteLine($"{transaction.transactionId} - {transaction.transactionAmount} - {transaction.transactionDescription} - {transaction.transactionType} - {transaction.transactionCategory} - {transaction.transactionTime}");
                                    Console.WriteLine("Da li stvarno želite izbrisati ovu transakciju (da/ne)?");
                                    var answer = string.Empty;
                                    do
                                    {
                                        answer = Console.ReadLine().ToLower();
                                        if (answer == "da")
                                        {
                                            account.Value.Remove(transaction);
                                            Console.WriteLine("Transakcija uspješno izbrisana");
                                            foundTransactionToDelete = true;
                                        }
                                        else if (answer == "ne")
                                        {
                                            Console.WriteLine("Transakcija nije izbrisana");
                                            foundTransactionToDelete = true;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Morate upisati da ili ne");
                                            continue;
                                        }
                                    } while (answer != "da" && answer != "ne");
                                }
                            }
                        }

                        if (!foundTransactionToDelete)
                        {
                            Console.WriteLine("Nema transakcija za prihod.");
                        }

                        Console.ReadKey();
                        break;
                    }
                case 'e':
                    {
                        bool foundTransactionToDelete = false;

                        foreach (var account in transactions[IdOfInputedUser])
                        {
                            if (account.Key == inputForBankAccountMenu)
                            {
                                var transactionsToDelete = account.Value.Where(transaction => transaction.transactionType.ToLower() == "rashod").ToList();

                                foreach (var transaction in transactionsToDelete)
                                {
                                    Console.WriteLine($"{transaction.transactionId} - {transaction.transactionAmount} - {transaction.transactionDescription} - {transaction.transactionType} - {transaction.transactionCategory} - {transaction.transactionTime}");
                                    Console.WriteLine("Da li stvarno želite izbrisati ovu transakciju (da/ne)?");
                                    var answer = string.Empty;
                                    do
                                    {
                                        answer = Console.ReadLine().ToLower();
                                        if (answer == "da")
                                        {
                                            account.Value.Remove(transaction);
                                            Console.WriteLine("Transakcija uspješno izbrisana");
                                            foundTransactionToDelete = true;
                                        }
                                        else if (answer == "ne")
                                        {
                                            Console.WriteLine("Transakcija nije izbrisana");
                                            foundTransactionToDelete = true;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Morate upisati da ili ne");
                                            continue;
                                        }
                                    } while (answer != "da" && answer != "ne");
                                }
                            }
                        }

                        if (!foundTransactionToDelete)
                        {
                            Console.WriteLine("Nema transakcija za rashod.");
                        }

                        Console.ReadKey();
                        break;
                    }
                case 'f':
                    {
                        Console.Write("Unesite kategoriju čije transakcije želite obrisati (placa/poklon/poticaj/zahvala/hrana/sport/prijevoz/kozmetika): ");
                        var categoryToDelete = Console.ReadLine().ToLower();

                        bool foundTransactionToDelete = false;

                        foreach (var account in transactions[IdOfInputedUser])
                        {
                            if (account.Key == inputForBankAccountMenu)
                            {
                                var transactionsToDelete = account.Value.Where(transaction => transaction.transactionCategory.ToLower() == categoryToDelete).ToList();

                                foreach (var transaction in transactionsToDelete)
                                {
                                    Console.WriteLine($"{transaction.transactionId} - {transaction.transactionAmount} - {transaction.transactionDescription} - {transaction.transactionType} - {transaction.transactionCategory} - {transaction.transactionTime}");
                                    Console.WriteLine("Da li stvarno želite izbrisati ovu transakciju (da/ne)?");
                                    var answer = string.Empty;
                                    do
                                    {
                                        answer = Console.ReadLine().ToLower();
                                        if (answer == "da")
                                        {
                                            account.Value.Remove(transaction);
                                            Console.WriteLine("Transakcija uspješno izbrisana");
                                            foundTransactionToDelete = true;
                                        }
                                        else if (answer == "ne")
                                        {
                                            Console.WriteLine("Transakcija nije izbrisana");
                                            foundTransactionToDelete = true;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Morate upisati da ili ne");
                                            continue;
                                        }
                                    } while (answer != "da" && answer != "ne");
                                }
                            }
                        }

                        if (!foundTransactionToDelete)
                        {
                            Console.WriteLine("Nema transakcija za odabranu kategoriju.");
                        }

                        Console.ReadKey();
                        break;
                    }
                case 'g':
                    {
                        return;
                    }
                default:
                    {
                        Console.WriteLine("Krivi unos, molimo pokusajte ponovo");
                        Console.ReadKey();
                        continue;
                    }
            }
        }

    }

    static void EditingTransactions(int IdOfInputedUser, string inputForBankAccountMenu)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("a) Pod id-u\nb) Povratak");
            var isInputCorrect = char.TryParse(Console.ReadLine(), out var inputForEditingTransaction);
            switch (inputForEditingTransaction)
            {
                case 'a':
                    {
                        Console.Write("Unesite id transakcije koju zelite promijeniti: ");
                        var isIdCorrect = float.TryParse(Console.ReadLine(), out var idToEdit);
                        bool foundTransactionToEdit = false;

                        if (isIdCorrect)
                        {
                            var accountTransactions = transactions[IdOfInputedUser][inputForBankAccountMenu];
                            for (int i = 0; i < accountTransactions.Count; i++)
                            {
                                var transaction = accountTransactions[i];
                                if (accountTransactions[i].transactionId == idToEdit)
                                {
                                    foundTransactionToEdit = true;
                                    Console.WriteLine($"{accountTransactions[i].transactionId} - {accountTransactions[i].transactionAmount} - {accountTransactions[i].transactionDescription} - {accountTransactions[i].transactionType} - {accountTransactions[i].transactionCategory} - {accountTransactions[i].transactionTime}");

                                    Console.WriteLine("Unesite novi iznos(ili Enter ako ne zelite mijenjati): ");
                                    var newDescriptiom = Console.ReadLine();
                                    while (true)
                                    {
                                        if (!string.IsNullOrWhiteSpace(newDescriptiom))
                                        {
                                            var isAmountCorrectNew = float.TryParse(newDescriptiom, out var newAmountFloat);
                                            if (isAmountCorrectNew)
                                            {
                                                transaction.transactionAmount = newAmountFloat;
                                                break;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Krivi unos, molimo pokusajte ponovo.");
                                                Console.WriteLine("Unesite novi iznos(ili Enter ako ne zelite mijenjati): ");
                                                newDescriptiom = Console.ReadLine();
                                                continue;
                                            }
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }

                                    Console.WriteLine("Unesite novi opis(ili Enter ako ne zelite mijenjati): ");
                                    var newDescription = Console.ReadLine();
                                    if (!string.IsNullOrWhiteSpace(newDescription))
                                    {
                                        transaction.transactionDescription = newDescription;
                                    }

                                    Console.WriteLine("Unesite novi tip(prihod/rashod)(ili Enter ako ne zelite mijenjati): ");
                                    var newType = Console.ReadLine().ToLower();
                                    var newCategory = string.Empty;
                                    while (true)
                                    {
                                        if (!string.IsNullOrWhiteSpace(newType))
                                        {
                                            if (newType == "prihod")
                                            {
                                                transaction.transactionType = newType;
                                                Console.WriteLine("Unesite novu kategoriju(placa/poklon/poticaj/zahvala)(ili Enter ako ne zelite mijenjati): ");
                                                newCategory = Console.ReadLine().ToLower();
                                                while (true)
                                                {
                                                    if (!string.IsNullOrEmpty(newCategory))
                                                    {
                                                        if (newCategory == "placa" || newCategory == "poklon" || newCategory == "poticaj" || newCategory == "zahvala")
                                                        {
                                                            transaction.transactionCategory = newCategory;
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Krivi unos, molimo pokusajte ponovo.");
                                                            Console.WriteLine("Unesite novu kategoriju(placa/poklon/poticaj/zahvala/hrana/sport/prijevoz/kozmetika)(ili Enter ako ne zelite mijenjati): ");
                                                            newCategory = Console.ReadLine().ToLower();
                                                            continue;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        break;
                                                    }
                                                }
                                                break;
                                            }
                                            else if (newType == "rashod")
                                            {
                                                transaction.transactionType = newType;
                                                Console.WriteLine("Unesite novu kategoriju(hrana/sport/prijevoz/kozmetika)(ili Enter ako ne zelite mijenjati): ");
                                                newCategory = Console.ReadLine().ToLower();
                                                while (true)
                                                {
                                                    if (!string.IsNullOrEmpty(newCategory))
                                                    {
                                                        if (newCategory == "hrana" || newCategory == "sport" || newCategory == "prijevoz" || newCategory == "kozmetika")
                                                        {
                                                            transaction.transactionCategory = newCategory;
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Krivi unos, molimo pokusajte ponovo.");
                                                            Console.WriteLine("Unesite novu kategoriju(placa/poklon/poticaj/zahvala/hrana/sport/prijevoz/kozmetika)(ili Enter ako ne zelite mijenjati): ");
                                                            newCategory = Console.ReadLine().ToLower();
                                                            continue;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        break;
                                                    }
                                                }
                                                break;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Krivi unos, molimo pokusajte ponovo.");
                                                Console.WriteLine("Unesite novi tip(ili Enter ako ne zelite mijenjati): ");
                                                newType = Console.ReadLine().ToLower();
                                                continue;
                                            }
                                        }
                                        else
                                        {
                                            newType = transaction.transactionType;
                                            if (newType == "prihod")
                                            {
                                                Console.WriteLine("Unesite novu kategoriju(placa/poklon/poticaj/zahvala)(ili Enter ako ne zelite mijenjati): ");
                                                newCategory = Console.ReadLine().ToLower();
                                                while (true)
                                                {
                                                    if (!string.IsNullOrEmpty(newCategory))
                                                    {
                                                        if (newCategory == "placa" || newCategory == "poklon" || newCategory == "poticaj" || newCategory == "zahvala")
                                                        {
                                                            transaction.transactionCategory = newCategory;
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Krivi unos, molimo pokusajte ponovo.");
                                                            Console.WriteLine("Unesite novu kategoriju(placa/poklon/poticaj/zahvala)(ili Enter ako ne zelite mijenjati): ");
                                                            newCategory = Console.ReadLine().ToLower();
                                                            continue;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        break;
                                                    }
                                                }

                                                break;
                                            }

                                            else if (newType == "rashod")
                                            {
                                                Console.WriteLine("Unesite novu kategoriju(hrana/sport/prijevoz/kozmetika)(ili Enter ako ne zelite mijenjati): ");
                                                newCategory = Console.ReadLine().ToLower();
                                                while (true)
                                                {
                                                    if (!string.IsNullOrEmpty(newCategory))
                                                    {
                                                        if (newCategory == "hrana" || newCategory == "sport" || newCategory == "prijevoz" || newCategory == "kozmetika")
                                                        {
                                                            transaction.transactionCategory = newCategory;
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Krivi unos, molimo pokusajte ponovo.");
                                                            Console.WriteLine("Unesite novu kategoriju(hrana/sport/prijevoz/kozmetika)(ili Enter ako ne zelite mijenjati): ");
                                                            newCategory = Console.ReadLine().ToLower();
                                                            continue;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        break;
                                                    }
                                                }
                                                break;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Krivi unos, molimo pokusajte ponovo.");
                                                Console.WriteLine("Unesite novi tip(prihod/rashod)(ili Enter ako ne zelite mijenjati): ");
                                                newType = Console.ReadLine().ToLower();
                                                continue;
                                            }
                                            break;
                                        }
                                    }

                                    Console.WriteLine("Unesite novi datum(ili Enter ako ne zelite mijenjati): ");
                                    var newDateTime = Console.ReadLine();
                                    while (true)
                                    {
                                        if (!string.IsNullOrWhiteSpace(newDateTime))
                                        {
                                            var isDateTimeCorrect = DateTime.TryParse(newDateTime, out var newDateTimeDateTime);
                                            if (isDateTimeCorrect)
                                            {
                                                transaction.transactionTime = newDateTimeDateTime;
                                                break;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Krivi unos, molimo pokusajte ponovo.");
                                                Console.WriteLine("Unesite novi datum(ili Enter ako ne zelite mijenjati): ");
                                                newDateTime = Console.ReadLine();
                                                continue;
                                            }
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    Console.WriteLine("Da li stvarno želite izmjeniti ovu transakciju (da/ne)?");
                                    var answer = string.Empty;
                                    do
                                    {
                                        answer = Console.ReadLine().ToLower();
                                        if (answer == "da")
                                        {
                                            foundTransactionToEdit = true;
                                            accountTransactions[i] = transaction;
                                            Console.WriteLine("Transakcija uspješno izmjenjena");
                                            Console.ReadKey();
                                            break;
                                        }
                                        else if (answer == "ne")
                                        {
                                            foundTransactionToEdit = true;
                                            Console.WriteLine("Transakcija nije izmjenjena.");
                                            Console.ReadKey();
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Morate upisati da ili ne");
                                            continue;
                                        }
                                    } while (answer != "da" && answer != "ne");
                                }
                            }
                            if (!foundTransactionToEdit)
                            {
                                Console.WriteLine($"Nema transakcija s id-om: {idToEdit}.");
                                Console.ReadKey();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Krivi unos, molimo pokusajte ponovo.");
                            Console.ReadKey();
                        }
                        break;
                 }

                case 'b':
                    {
                        return;
                    }
                default:
                    {
                        Console.WriteLine("Krivi unos, molimo pokusajte ponovo");
                        Console.ReadKey();
                        continue;
                    }
            }
        }
    }

    static void OverviewOfTransactions(int IdOfInputedUser, string inputForBankAccountMenu)
    {

    
        while (true)
        {
            Console.Clear();
            Console.WriteLine("a) Sve transakcije kako su spremljene\nb) Sve transakcije sortirane po iznosu uzlazno\n" +
                "c) Sve transakcije sortirane po iznosu silazno\nd) Sve transakcije sortirane po opisu abecedno\ne) Sve transakcije sortirane po datumu uzlazno\n" +
                "f) Sve transakcije sortirane po datumu silazno\ng) Svi prihodi\nh) Svi rashodi\ni) Sve transakcije za odabranu kategoriju\n" +
                "j) Sve transakcije za odabrani tip i kategoriju\nk) Povratak");
            var isInputCorrect = char.TryParse(Console.ReadLine(), out var inputForOverviewOfTransactions);
            switch (inputForOverviewOfTransactions)
            {
                case 'a':
                    {
                        bool foundTransation=false;
                        Console.Clear();
                        foreach (var account in transactions[IdOfInputedUser])
                        {
                            if (account.Key == inputForBankAccountMenu)
                            {
                                foreach (var transaction in account.Value)
                                {
                                    foundTransation = true;
                                    Console.WriteLine($"{transaction.transactionType} - {transaction.transactionAmount} - {transaction.transactionDescription} - {transaction.transactionCategory} - {transaction.transactionTime}");
                                }
                            }
                        }
                        if (!foundTransation)
                        {
                            Console.WriteLine("Nema transakcija");
                        }
                        Console.ReadKey();
                        break;
                    }
                case 'b':
                    {
                        Console.Clear();
                        bool foundTransation = false;
                        foreach (var account in transactions[IdOfInputedUser])
                        {
                            if (account.Key == inputForBankAccountMenu)
                            {
                                foreach (var transaction in account.Value.OrderBy(transaction => transaction.transactionAmount))
                                {
                                    foundTransation = true;
                                    Console.WriteLine($"{transaction.transactionType} - {transaction.transactionAmount} - {transaction.transactionDescription} - {transaction.transactionCategory} - {transaction.transactionTime}");
                                }
                            }
                        }
                        if (!foundTransation)
                        {
                            Console.WriteLine("Nema transakcija");
                        }
                        Console.ReadKey();
                        break;
                    }
                case 'c':
                    {
                        Console.Clear();
                        bool foundTransation = false;
                        foreach (var account in transactions[IdOfInputedUser])
                        {
                            if (account.Key == inputForBankAccountMenu)
                            {
                                foreach (var transaction in account.Value.OrderByDescending(transaction => transaction.transactionAmount))
                                {
                                    foundTransation = true;
                                    Console.WriteLine($"{transaction.transactionType} - {transaction.transactionAmount} - {transaction.transactionDescription} - {transaction.transactionCategory} - {transaction.transactionTime}");
                                }
                            }
                        }
                        if (!foundTransation)
                        {
                            Console.WriteLine("Nema transakcija");
                        }
                        Console.ReadKey();
                        break;
                    }
                case 'd':
                    {
                        Console.Clear();
                        bool foundTransation = false;
                        foreach (var account in transactions[IdOfInputedUser])
                        {
                            if (account.Key == inputForBankAccountMenu)
                            {
                                foreach (var transaction in account.Value.OrderBy(transaction => transaction.transactionDescription))
                                {
                                    foundTransation = true;
                                    Console.WriteLine($"{transaction.transactionType} - {transaction.transactionAmount} - {transaction.transactionDescription} - {transaction.transactionCategory} - {transaction.transactionTime}");
                                }
                            }
                        }
                        if (!foundTransation)
                        {
                            Console.WriteLine("Nema transakcija");
                        }
                        Console.ReadKey();
                        break;
                    }
                case 'e':
                    {
                        Console.Clear();
                        bool foundTransation = false;
                        foreach (var account in transactions[IdOfInputedUser])
                        {
                            if (account.Key == inputForBankAccountMenu)
                            {
                                foreach (var transaction in account.Value.OrderBy(transaction => transaction.transactionTime))
                                {
                                    foundTransation = true;
                                    Console.WriteLine($"{transaction.transactionType} - {transaction.transactionAmount} - {transaction.transactionDescription} - {transaction.transactionCategory} - {transaction.transactionTime}");
                                }
                            }
                        }
                        if (!foundTransation)
                        {
                            Console.WriteLine("Nema transakcija");
                        }
                        Console.ReadKey();
                        break;
                    }
                case 'f':
                    {
                        Console.Clear();
                        bool foundTransation = false;
                        foreach (var account in transactions[IdOfInputedUser])
                        {
                            if (account.Key == inputForBankAccountMenu)
                            {
                                foreach (var transaction in account.Value.OrderByDescending(transaction => transaction.transactionTime))
                                {
                                    foundTransation = true;
                                    Console.WriteLine($"{transaction.transactionType} - {transaction.transactionAmount} - {transaction.transactionDescription} - {transaction.transactionCategory} - {transaction.transactionTime}");
                                }
                            }
                        }
                        if (!foundTransation)
                        {
                            Console.WriteLine("Nema transakcija");
                        }
                        Console.ReadKey();
                        break;
                    }
                case 'g':
                    {
                        Console.Clear();
                        bool foundTransation = false;
                        foreach (var account in transactions[IdOfInputedUser])
                        {
                            if (account.Key == inputForBankAccountMenu)
                            {
                                foreach (var transaction in account.Value.Where(transaction => transaction.transactionType.ToLower() == "prihod"))
                                {
                                    foundTransation = true;
                                    Console.WriteLine($"{transaction.transactionType} - {transaction.transactionAmount} - {transaction.transactionDescription} - {transaction.transactionCategory} - {transaction.transactionTime}");
                                }
                            }
                        }
                        if (!foundTransation)
                        {
                            Console.WriteLine("Nema transakcija pod tipom: prihodi");
                        }
                        Console.ReadKey();
                        break;
                    }
                case 'h':
                    {
                        Console.Clear();
                        bool foundTransation = false;
                        foreach (var account in transactions[IdOfInputedUser])
                        {
                            if (account.Key == inputForBankAccountMenu)
                            {
                                foreach (var transaction in account.Value.Where(transaction => transaction.transactionType.ToLower() == "rashod"))
                                {
                                    foundTransation = true;
                                    Console.WriteLine($"{transaction.transactionType} - {transaction.transactionAmount} - {transaction.transactionDescription} - {transaction.transactionCategory} - {transaction.transactionTime}");
                                }
                            }
                        }
                        if (!foundTransation)
                        {
                            Console.WriteLine("Nema transakcija pod tipom: rashod");
                        }
                        Console.ReadKey();
                        break;
                    }
                case 'i':
                    {
                        Console.Clear();
                        bool foundTransation = false;
                        Console.Write("Unesite kategoriju čije transakcije želite vidjeti (placa/poklon/poticaj/zahvala/hrana/sport/prijevoz/kozmetika): ");
                        var categoryToSee = Console.ReadLine().ToLower();
                        if (categoryToSee == "placa" || categoryToSee == "poklon" || categoryToSee == "poticaj" || categoryToSee == "zahvala" || categoryToSee == "hrana" || categoryToSee == "sport" || categoryToSee == "prijevoz" || categoryToSee == "kozmetika")
                        {
                            foreach (var account in transactions[IdOfInputedUser])
                            {
                                if (account.Key == inputForBankAccountMenu)
                                {
                                    foreach (var transaction in account.Value.Where(transaction => transaction.transactionCategory.ToLower() == categoryToSee))
                                    {
                                        foundTransation = true;
                                        Console.WriteLine($"{transaction.transactionType} - {transaction.transactionAmount} - {transaction.transactionDescription} - {transaction.transactionCategory} - {transaction.transactionTime}");
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Krivi unos, molimo pokusajte ponovo");
                            Console.ReadKey();
                            break;
                        }
                        if (!foundTransation)
                        {
                            Console.WriteLine($"Nema transakcija pod kategorijom:{categoryToSee}");
                        }
                        
                        Console.ReadKey();
                        break;
                    }
                case 'j':
                    {
                        Console.Clear();
                        Console.Write("Unesite tip transakcije (prihod/rashod): ");
                        var typeToSee = Console.ReadLine().ToLower();
                        bool foundTransaction = false;
                        var categoryToSee = string.Empty;
                        if (typeToSee == "prihod")
                        {
                            Console.WriteLine("Odaberite kategoriju transakcije(placa/poklon/poticaj/zahvala): ");
                            categoryToSee = Console.ReadLine().ToLower();
                            if (categoryToSee == "placa" || categoryToSee == "poklon" || categoryToSee == "poticaj" || categoryToSee == "zahvala")
                            {
                                foreach (var account in transactions[IdOfInputedUser])
                                {
                                    if (account.Key == inputForBankAccountMenu)
                                    {
                                        foreach (var transaction in account.Value.Where(transaction => transaction.transactionType.ToLower() == typeToSee && transaction.transactionCategory.ToLower() == categoryToSee))
                                        {
                                            foundTransaction = true;
                                            Console.WriteLine($"{transaction.transactionType} - {transaction.transactionAmount} - {transaction.transactionDescription} - {transaction.transactionCategory} - {transaction.transactionTime}");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Krivi unos, molimo pokusajte ponovo");
                                Console.ReadKey();
                                break;
                            }
                        }
                        else if (typeToSee == "rashod")
                        {
                            Console.WriteLine("Odaberite kategoriju transakcije(hrana/sport/prijevoz/kozmetika): ");
                            categoryToSee = Console.ReadLine();
                            if (categoryToSee == "hrana" || categoryToSee == "sport" || categoryToSee == "prijevoz" || categoryToSee == "kozmetika")
                            {
                                foreach (var account in transactions[IdOfInputedUser])
                                {
                                    if (account.Key == inputForBankAccountMenu)
                                    {
                                        foreach (var transaction in account.Value.Where(transaction => transaction.transactionType.ToLower() == typeToSee && transaction.transactionCategory == categoryToSee))
                                        {
                                            foundTransaction = true;
                                            Console.WriteLine($"{transaction.transactionType} - {transaction.transactionAmount} - {transaction.transactionDescription} - {transaction.transactionCategory} - {transaction.transactionTime}");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Krivi unos, molimo pokusajte ponovo");
                                Console.ReadKey();
                                break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Krivi unos, molimo pokusajte ponovo");
                            Console.ReadKey();
                            break;
                        }
                        if (!foundTransaction)
                        {
                            Console.WriteLine($"Nema transakcija pod tipom {typeToSee} i kategorijom {categoryToSee}");
                        }
                        Console.ReadKey();
                        break;
                    }
                case 'k':
                    {
                        return;
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

    static void FinancialSummary(int IdOfInputedUser, string inputForBankAccountMenu)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("a) trenutno stanje računa\nb) Broj ukupnih transakcija\nc) Ukupan iznos prihoda i rashoda za odabrani mjesec i godinu\n" +
                "d) Postotak udjela rashoda za odabranu kategoriju\ne) Prosječni iznos transakcije za odabrani mjesec i godinu\n" +
                "f) Prosječni iznos transakcije za odabranu kategoriju\r\n");
            var isInputCorrect = char.TryParse(Console.ReadLine(), out var inputForFinancialSummary);
            switch (inputForFinancialSummary)
            {

            }
        }
    }
}
 
 

