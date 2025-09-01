using System.Reflection.Emit;
using W1_assignment_template;

class Program
{
    static void DisplayMenu() // Displays the menu
    {
        Console.WriteLine("1. Display Characters");
        Console.WriteLine("2. Add Character");
        Console.WriteLine("3. Level Up Character");
    }

    static void ListCharStats(FileManager fileManager) // Reads all of the charatcers from input.csv
    {
        foreach (var line in fileManager.FileContents)
        {
            var cols = line.Split(",");
            var name = cols[0];

            if (name == "Name") // if "Name" is the character name, meaning program is looking at the header, then iteration is skipped
            {
                continue;
            }

            var charClass = cols[1];
            var lvl = cols[2];
            var hp = cols[3];
            var equipment = cols[4].Split("|");

            Console.WriteLine($"\nName: {name}");
            Console.WriteLine($"Class: {charClass}");
            Console.WriteLine($"Level: {lvl}");
            Console.WriteLine($"HP: {hp}");

            Console.WriteLine("Character Equipment:");
            foreach (var equip in equipment)
            {
                Console.WriteLine($"\t{equip}");
            }
            Console.WriteLine("------------------\n");
        }
    }

    static Character WriteCharacter(FileManager fileManager) // Add a new character to input.csv
    {
        var newCharacter = new Character();

        Console.Write("\nEnter your character's name: ");
        newCharacter.name = Console.ReadLine();

        Console.Write("Enter your character's class: ");
        newCharacter.charClass = Console.ReadLine();

        Console.Write("Enter your character's level: ");
        newCharacter.lvl = int.Parse(Console.ReadLine());

        Console.Write("Enter your character's health points: ");
        newCharacter.hp = int.Parse(Console.ReadLine());

        Console.Write("Enter your character's equipment (separate items with a '|'): ");
        string temp = Console.ReadLine();

        foreach (string t in temp.Split('|')) // adds string variables to equipment list
        {
            newCharacter.equipment.Add(t);
        }

        return newCharacter; // returns Character class
    }

    static void Main()
    {
        // Create a fileManager class and read all line from the file
        var fileManager = new FileManager();
        fileManager.Read();

        // Display the menu and prompt the user to enter an option
        DisplayMenu();
        Console.Write("Choose an option> ");
        var userInput = Console.ReadLine();

        if (userInput == "1") // list the pre-existing characters
        {
            ListCharStats(fileManager);
        }
        else if (userInput == "2") // Add a character to the list
        {
            fileManager.Write(WriteCharacter(fileManager));
        }
        else if (userInput == "3") // Update an existing character
        {
            Console.WriteLine("\nList of Characters: ");

            foreach (var line in fileManager.FileContents) // foreach loop lists the names of all the characters
            {
                var cols = line.Split(",");
                var name = cols[0];

                if (name == "Name") // if "Name" is the character name, meaning program is looking at the header, then iteration is skipped
                {
                    continue;
                }

                Console.WriteLine($"\t{name}");
            }

            List<string> tempFile = new List<string>(); // this list will store all of the lines of the csv file

            Console.Write("\nType in the name of the character you want to update> "); // user selects which character they want to update
            userInput = Console.ReadLine();

            foreach (var line in fileManager.FileContents)
            {
                var cols = line.Split(",");
                var name = cols[0];
                var charClass = cols[1];
                var lvl = cols[2];
                var hp = cols[3];
                var equipment = cols[4];

                var updateCharacter = new List<Character>(); // initilization of new Character class

                if (name == userInput && name != "Name") // if the name matches the one the user entered, then the new information gets added
                {
                    Console.Write("\nUpdate your character's name: ");
                    name = Console.ReadLine();

                    Console.Write("Update your character's class: ");
                    charClass = Console.ReadLine();

                    Console.Write("Update your character's level: ");
                    lvl = Console.ReadLine();

                    Console.Write("Update your character's health points: ");
                    hp = Console.ReadLine();

                    Console.Write("Update your character's equipment (separate items with a '|'): ");
                    equipment = Console.ReadLine();

                    tempFile.Add($"{name},{charClass},{lvl},{hp},{equipment}");
                }
                else
                {
                    tempFile.Add($"{name},{charClass},{lvl},{hp},{equipment}");
                }
            }

            File.WriteAllText("input.csv", string.Empty); // delete all previous data in preparation to add updated data

            using (StreamWriter writer = new StreamWriter("input.csv", true))
            {
                foreach (var fileLine in tempFile)
                {
                    writer.WriteLine($"{fileLine}");
                }
            }
        }
        else // if the user enters something other than 1, 2, or 3, then the program quits
        {
            Console.WriteLine("Invalid option selected");
        }
    }
}