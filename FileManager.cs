namespace W1_assignment_template
{
    public class FileManager
    {
        public string[]? FileContents { get; set; }

        private string _fileName = "input.csv";

        public FileManager()
        {

        }

        public void Read()
        {
            FileContents = File.ReadAllLines(_fileName);
        }

        public void Write()
        {
            using (StreamWriter writer = new StreamWriter(_fileName, true))
            {

            }
        }
    }
}
