namespace Items
{
    [System.Serializable]
    public class Armor : Item
    {
        public Armor() { }
        public Armor(string name, float protection, float mobility)
        {
            this.name = name; this.protection = protection; this.mobility = mobility;
        }
        public float protection;
        public float mobility;
    }
}
