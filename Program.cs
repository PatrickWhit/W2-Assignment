using System.Reflection.Emit;
using W1_assignment_template;

class Program
{
    static void DisplayMenu() // Displays the menu
    {
        Console.WriteLine("1. Display Characters");
        Console.WriteLine("2. Add Character");
        Console.WriteLine("3. Update Character");
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

            var updateCharacters = new List<Character>(); // initilization of new Character class to store new line of input.csv

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

                if (name == userInput && name != "Name") // if the name matches the one the user entered, then the new information gets added
                {
                    updateCharacters.Add(WriteCharacter(fileManager)); // adds updated line to new character list
                }
                else // if name doesn't match then no charnges are made
                {
                    var oldCharacter = new Character();

                    oldCharacter.name = name;
                    oldCharacter.charClass = charClass;
                    oldCharacter.lvl = int.Parse(lvl);
                    oldCharacter.hp = int.Parse(hp);

                    foreach (var eq in equipment.Split("|"))
                    {
                        oldCharacter.equipment.Add(eq);
                    }

                    updateCharacters.Add(oldCharacter);
                }
            }

            File.WriteAllText("input.csv", string.Empty); // delete all previous data in preparation to add updated data

            fileManager.ReWrite(updateCharacters);
        }
        else // if the user enters something other than 1, 2, or 3, then the program quits
        {
            Console.WriteLine("Invalid option selected");
        }
    }
}