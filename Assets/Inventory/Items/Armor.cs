namespace Items
{
    public class Armor : Item
    {
        public Armor(string name, float protection, float mobility)
        {
            this.name = name; this.protection = protection; this.mobility = mobility;
        }
        public float protection;
        public float mobility;
    }
}
