using UnityEngine;
#if SERVER && UNITY_EDITOR
using UnityEditor;
#endif
using Database;
using Items;
using Networking;
using System;
using System.Collections;

namespace Utility
{
    public class Debugging : MonoBehaviour
    {
        private static bool eventsConnected = false;

        public static void Print(object message)
        {
            Debug.Log(message);
        }

        public static void PrintScreen(string message)
        {
            #if NGUI
            NGUIDebug.Log(message);
            #endif
        }

        public static void ConnectEvents()
        {
            if (eventsConnected)
                return;

            Server server = NetworkManager.GetServer();
            Client client = NetworkManager.GetClient();

            if (server != null)
            {
                server.OnSetUp += OnServerSetUp;
            }
            if (client != null)
            {
                client.OnSetUp += OnClientSetUp;
            }
        }

        static void OnServerSetUp(object source, EventArgs args)
        {
            Debugging.PrintScreen("Setting up server");
        }

        static void OnClientSetUp(object source, EventArgs args)
        {
            Debugging.PrintScreen("Setting up client");
        }

#if SERVER && UNITY_EDITOR
        [MenuItem("Inventory.db/ResetTables")]
        public static void ResetTables()
        {
            DatabaseManager.Connect();
            DatabaseManager.DropTable("Weapons");
            DatabaseManager.DropTable("Armors");
            DatabaseManager.DropTable("Consumables");

            DatabaseManager.CheckTable("Weapons");
            DatabaseManager.CheckTable("Armors");
            DatabaseManager.CheckTable("Consumables");
            DatabaseManager.Disconnect();
        }

        [MenuItem("Inventory.db/PrintTables")]
        public static void PrintTables()
        {
            DatabaseManager.Connect();
            DatabaseManager.CheckTable("Weapons");
            DatabaseManager.CheckTable("Armors");
            DatabaseManager.CheckTable("Consumables");

            DatabaseManager.PrintTable("Weapons");
            DatabaseManager.PrintTable("Armors");
            DatabaseManager.PrintTable("Consumables");
            DatabaseManager.Disconnect();
        }

        [MenuItem("Inventory.db/AddTestData")]
        public static void AddTestData()
        {
            DatabaseManager.Connect();
            DatabaseManager.CheckTable("Weapons");
            DatabaseManager.CheckTable("Armors");
            DatabaseManager.CheckTable("Consumables");

            DatabaseManager.AddWeapon(new Weapon("TestWeapon01", 10, 11, 12));
            DatabaseManager.AddWeapon(new Weapon("TestWeapon02", 13, 14, 15));
            DatabaseManager.AddWeapon(new Weapon("TestWeapon03", 16, 17, 18));

            DatabaseManager.AddArmor(new Armor("TestArmor01", 19, 20));
            DatabaseManager.AddArmor(new Armor("TestArmor02", 21, 22));
            DatabaseManager.AddArmor(new Armor("TestArmor03", 23, 24));

            DatabaseManager.AddConsumable(new Consumable("TestConsumable01"));
            DatabaseManager.AddConsumable(new Consumable("TestConsumable02"));
            DatabaseManager.AddConsumable(new Consumable("TestConsumable03"));
            DatabaseManager.Disconnect();
        }
#endif
    }
}