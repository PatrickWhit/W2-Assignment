namespace W1_assignment_template
{
    public class FileManager
    {
        public string[]? FileContents { get; set; }

        private string _fileName = "input.csv";

        public FileManager()
        {

        }

        public void Read() // reads from input.csv
        {
            FileContents = File.ReadAllLines(_fileName);
        }

        public void Write(Character newCharacter)
        {
            var equipString = string.Join("|", newCharacter.equipment); // turns the equipment List into a single string

            var newCharString = $"{newCharacter.name}, {newCharacter.charClass}," +
                                $" {newCharacter.lvl}, {newCharacter.hp}, {equipString}"; // take the character and converts to a string

            using (StreamWriter writer = new StreamWriter(_fileName, true)) // writes the string to inout.csv
            {
                writer.WriteLine(newCharString);
            }
        }

        public void ReWrite(List<Character> updateCharacters)
        {
            foreach (var character in updateCharacters)
            {
                var equipString = string.Join("|", character.equipment); // turns the equipment List into a single string

                var CharString = $"{character.name}, {character.charClass}," +
                                    $" {character.lvl}, {character.hp}, {equipString}"; // take the character and converts to a string

                using (StreamWriter writer = new StreamWriter(_fileName, true)) // writes the string to inout.csv
                {
                    writer.WriteLine(CharString);
                }
            }
        }
    }
}
