namespace Items
{
    [System.Serializable]
    public class Item
    {
        public Item() { }
        public Item(string name)
        {
            this.name = name;
        }
        public string name;
    }
}
