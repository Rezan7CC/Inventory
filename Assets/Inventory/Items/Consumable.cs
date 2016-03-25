namespace Items
{
    [System.Serializable]
    public class Consumable : Item
    {
        public Consumable() { }
        public Consumable(string name)
        {
            this.name = name;
        }
    }
}
