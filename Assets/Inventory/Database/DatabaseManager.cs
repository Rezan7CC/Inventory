using Items;
using Utility;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using Mono.Data.SqliteClient;
using UnityEngine;

namespace Database
{
    public class DatabaseManager
    {
        public static string dbFilename = "Inventory.db";
        public static bool IsConnected { get { return IsConnected; } }
        private static bool isConnected = false;

        private static SqliteConnection dbConnection;

        private static string weaponColumns = "(id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, name text, damage float, speed float, critChance float)";
        private static string armorColumns = "(id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, name text, protection float, mobility float)";
        private static string consumableColumns = "(id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, name text)";

        public static void Connect()
        {
            if (isConnected)
                return;

            dbConnection = new SqliteConnection("URI=file:" + Application.dataPath + "/" + dbFilename + ";");
            dbConnection.Open();
        }

        public static void Disconnect()
        {
            if (!isConnected)
                return;

            dbConnection.Close();
            isConnected = false;
        }

        public static void AddItem(Item item)
        {
            Weapon weapon = (Weapon)item;
            if (weapon != null)
            {
                AddWeapon(weapon);
                return;
            }

            Armor armor = (Armor)item;
            if(armor != null)
            {
                AddArmor(armor);
                return;
            }

            Consumable consumable = (Consumable)item;
            if (consumable != null)
            {
                AddConsumable(consumable);
                return;
            }
        }

        public static void AddWeapon(Weapon weapon)
        {
            CheckTable("Weapons");
            SqliteCommand sqlCommand = new SqliteCommand("insert into Weapons (name, damage, speed, critChance) values "
                                                       + "('" + weapon.name + "', " + weapon.damage + ", "
                                                       + weapon.speed + ", " + weapon.critChance + ")", dbConnection);
            sqlCommand.ExecuteNonQuery();
        }

        public static void AddArmor(Armor armor)
        {
            CheckTable("Armors");
            SqliteCommand sqlCommand = new SqliteCommand("insert into Armors (name, protection, mobility) values "
                                                       + "('" + armor.name + "', " + armor.protection + ", "
                                                       + armor.mobility + ")", dbConnection);
            sqlCommand.ExecuteNonQuery();
        }

        public static void AddConsumable(Consumable consumable)
        {
            CheckTable("Consumables");
            SqliteCommand sqlCommand = new SqliteCommand("insert into Consumables (name) values "
                                                       + "('" + consumable.name + "')", dbConnection);
            sqlCommand.ExecuteNonQuery();
        }

        public static void CheckTable(string tableName)
        {
            string columns = "";
            switch(tableName)
            {
                case "Weapons":
                {
                    columns = weaponColumns;
                    break;
                }
                case "Armors":
                {
                    columns = armorColumns;
                    break;
                }
                case "Consumables":
                {
                    columns = consumableColumns;
                    break;
                }
            }
            SqliteCommand sqlCommand = new SqliteCommand("create table if not exists " + tableName + columns + ";", dbConnection);
            sqlCommand.ExecuteNonQuery();
        }

        public static void DropTable(string tableName)
        {
            SqliteCommand sqlCommand = new SqliteCommand("drop table if exists " + tableName, dbConnection);
            sqlCommand.ExecuteNonQuery();
        }

        public static void PrintTable(string tableName)
        {
            SqliteCommand sqlCommand = new SqliteCommand("select * from " + tableName, dbConnection);

            SqliteDataReader dataReader = sqlCommand.ExecuteReader();
            Debugging.Print("Table: " + tableName);
            while (dataReader.Read())
            {
                string row = "";
                for(int i = 0; i < dataReader.FieldCount; i++)
                {
                    row += dataReader.GetName(i) + ": " + dataReader.GetString(i) + " | ";
                }
                Debugging.Print(row);
            }
        }

        public static List<Weapon> GetWeapons()
        {
            List<Weapon> weapons = new List<Weapon>();

            SqliteCommand sqlCommand = new SqliteCommand("select * from Weapons", dbConnection);

            SqliteDataReader dataReader = sqlCommand.ExecuteReader();
            while (dataReader.Read())
            {
                Weapon weapon = new Weapon();
                weapon.name = dataReader.GetString(1);
                weapon.damage = dataReader.GetFloat(2);
                weapon.speed = dataReader.GetFloat(3);
                weapon.critChance = dataReader.GetFloat(4);
                weapons.Add(weapon);    
            }
            return weapons;
        }

        public static List<Armor> GetArmors()
        {
            List<Armor> armors = new List<Armor>();

            SqliteCommand sqlCommand = new SqliteCommand("select * from Armors", dbConnection);

            SqliteDataReader dataReader = sqlCommand.ExecuteReader();
            while (dataReader.Read())
            {
                Armor armor = new Armor();
                armor.name = dataReader.GetString(1);
                armor.protection = dataReader.GetFloat(2);
                armor.mobility = dataReader.GetFloat(3);
                armors.Add(armor);
            }

            return armors;
        }

        public static List<Consumable> GetConsumable()
        {
            List<Consumable> consumables = new List<Consumable>();

            SqliteCommand sqlCommand = new SqliteCommand("select * from Consumables", dbConnection);

            SqliteDataReader dataReader = sqlCommand.ExecuteReader();
            while (dataReader.Read())
            {
                Consumable consumable = new Consumable();
                consumable.name = dataReader.GetString(1);
                consumables.Add(consumable);
            }

            return consumables;
        }
    }
}
