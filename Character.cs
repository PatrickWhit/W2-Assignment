namespace W1_assignment_template
{
    public class Character
    {
        public string name { get; set; }
        public string charClass { get; set; }
        public int lvl { get; set; }
        public int hp { get; set; }
        public List<string> equipment { get; set; }


        public Character()
        {
            equipment = new List<string>();
        }

        public Character(string Name, string CharClass, int Lvl, int Hp, List<string> Equipment)
        {
            name = Name;
            charClass = CharClass;
            lvl = Lvl;
            hp = Hp;
            equipment = Equipment;
        }
    }
}
