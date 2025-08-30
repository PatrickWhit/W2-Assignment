namespace W1_assignment_template
{
    public class Character
    {
        public string Name { get; set; }
        public string CharClass { get; set; }
        public string Lvl { get; set; }
        public string Hp { get; set; }
        public List<string> Equipment { get; set; }


        public Character()
        {

        }

        public Character(string name, string charClass, string lvl, string hp, List<string> equipment)
        {
            Name = name;
            CharClass = charClass;
            Lvl = lvl;
            Hp = hp;
            Equipment = equipment;
        }
    }
}
