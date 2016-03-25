namespace Items
{
    [System.Serializable]
    public class Weapon : Item
    {
        public Weapon(){}
        public Weapon(string name, float damage, float speed, float critChance)
        {
            this.name = name; this.damage = damage; this.speed = speed; this.critChance = critChance;
        }
        public float damage;
        public float speed;
        public float critChance;
    }
}