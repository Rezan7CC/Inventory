using UnityEngine;
using System.Collections;
using Networking;

namespace Items
{
    public class ItemGenerator : MonoBehaviour
    {
        enum ItemType
        {
            EWeapon, EArmor, EConsumable
        };

        [System.Serializable]
        public class WeaponRanges
        {
            public float minDamage;
            public float maxDamage;

            public float minSpeed;
            public float maxSpeed;

            public float minCritChance;
            public float maxCritChance;
        }

        [System.Serializable]
        public class ArmorRanges
        {
            public float minProtection;
            public float maxProtection;

            public float minMobility;
            public float maxMobility;
        }

        public string[] weaponNameFirstNouns;
        public string[] armorNameFirstNouns;
        public string[] consumableNameFirstNouns;

        public string[] nameSecondNouns;

        public WeaponRanges weaponRanges;
        public ArmorRanges armorRanges;
        public NetworkManager networkManager;

        public void OnGenerateWeaponClick()
        {
            GenerateWeapon();
        }

        public void OnGenerateArmorClick()
        {
            GenerateArmor();
        }

        public void OnGenerateConsumableClick()
        {
            GenerateConsumable();
        }

        void GenerateWeapon()
        {
            Weapon generatedWeapon = new Weapon();
            generatedWeapon.name = GenerateItemName(ItemType.EWeapon);
            generatedWeapon.damage = Random.Range(weaponRanges.minDamage, weaponRanges.maxDamage);
            generatedWeapon.speed = Random.Range(weaponRanges.minSpeed, weaponRanges.maxSpeed);
            generatedWeapon.critChance = Random.Range(weaponRanges.minCritChance, weaponRanges.maxCritChance);
            AddGeneratedItem(generatedWeapon);
        }

        void GenerateArmor()
        {
            Armor generatedArmor = new Armor();
            generatedArmor.name = GenerateItemName(ItemType.EArmor);
            generatedArmor.protection = Random.Range(armorRanges.minProtection, armorRanges.maxProtection);
            generatedArmor.mobility = Random.Range(armorRanges.minMobility, armorRanges.maxMobility);
            AddGeneratedItem(generatedArmor);
        }

        void GenerateConsumable()
        {
            Consumable generatedConsumable = new Consumable();
            generatedConsumable.name = GenerateItemName(ItemType.EConsumable);
            Client client = (Client)networkManager.NetworkApplication;
            AddGeneratedItem(generatedConsumable);
        }

        void AddGeneratedItem(Item generatedItem)
        {
            Client client = (Client)networkManager.NetworkApplication;
            if (client == null)
                return;
            client.InventoryItems.Add(generatedItem);
            networkManager.itemManager.CreateItems(client.InventoryItems);
            client.SendItem(generatedItem);
        }

        string GenerateItemName(ItemType itemType)
        {
            string generatedName = "";
            switch(itemType)
            {
                case ItemType.EWeapon:
                    {
                        generatedName += weaponNameFirstNouns[Random.Range(0, weaponNameFirstNouns.Length)];
                        break;
                    }
                case ItemType.EArmor:
                    {
                        generatedName += armorNameFirstNouns[Random.Range(0, armorNameFirstNouns.Length)];
                        break;
                    }
                case ItemType.EConsumable:
                    {
                        generatedName += consumableNameFirstNouns[Random.Range(0, consumableNameFirstNouns.Length)];
                        break;
                    }
            }

            generatedName += " of ";
            generatedName += nameSecondNouns[Random.Range(0, nameSecondNouns.Length)];
            return generatedName;
        }
    }
}
