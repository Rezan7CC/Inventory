﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Items;

namespace UserInterface
{
    public class ItemManager : MonoBehaviour
    {
        public UIGrid itemGrid;
        public GameObject itemPrefab;

        void Start()
        {

        }

        void Update()
        {

        }

        public void CreateItems(IList<Item> items)
        {
            Transform[] childs = itemGrid.GetComponentsInChildren<Transform>();
            foreach(Transform child in childs)
            {
                if (child == itemGrid.transform)
                    continue;
                Destroy(child.gameObject);
            }

            foreach(Item item in items)
            {
                GameObject itemGameObject = Instantiate(itemPrefab) as GameObject;
                ItemProperties itemProperties = itemGameObject.GetComponent<ItemProperties>();
                SetItemProperties(itemProperties, item);
                itemGameObject.transform.parent = itemGrid.transform;
                itemGameObject.transform.position = Vector3.zero;
                itemGameObject.transform.localScale = Vector3.one;
                itemGameObject.transform.localRotation = Quaternion.identity;         
            }
            itemGrid.Reposition();
        }

        enum ItemType
        {
            EWeapon, EArmor, EConsumable, ENone
        };

        void SetItemProperties(ItemProperties itemProperties, Item item)
        {
            ItemType itemType = ItemType.ENone;

            Weapon weapon = item as Weapon; 
            Armor armor = item as Armor;
            Consumable consumable = item as Consumable;

            if (weapon != null)
                itemType = ItemType.EWeapon;
            else if (armor != null)
                itemType = ItemType.EArmor;
            else if (consumable != null)
                itemType = ItemType.EConsumable;

            itemProperties.itemName = item.name;

            switch (itemType)
            {
                case ItemType.EWeapon:
                    {
                        itemProperties.selectedItemPortrait = 0;
                        itemProperties.properties[0].propertyName = "Damage";
                        itemProperties.properties[0].propertyValue = weapon.damage.ToString();
                        itemProperties.properties[1].propertyName = "Speed";
                        itemProperties.properties[1].propertyValue = weapon.speed.ToString();
                        itemProperties.properties[2].propertyName = "Crit Chance";
                        itemProperties.properties[2].propertyValue = weapon.critChance.ToString();
                        break;
                    }
                case ItemType.EArmor:
                    {
                        itemProperties.selectedItemPortrait = 1;
                        itemProperties.properties[0].propertyName = "Protection";
                        itemProperties.properties[0].propertyValue = armor.protection.ToString();
                        itemProperties.properties[1].propertyName = "Mobility";
                        itemProperties.properties[1].propertyValue = armor.mobility.ToString();
                        break;
                    }
                case ItemType.EConsumable:
                    {
                        itemProperties.selectedItemPortrait = 2;
                        break;
                    }
            }
            itemProperties.Apply();
        }
    }
}