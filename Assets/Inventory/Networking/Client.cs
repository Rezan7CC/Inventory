using UnityEngine.Networking;
using System.Collections.Generic;
using System;
using Items;
using Utility;

namespace Networking
{
    public class Client : IApplication
    {
        public EventHandler OnSetUp;

        NetworkManager networkManager = null;
        NetworkClient networkClient = null;

        public List<Item> InventoryItems { get { return items; } }
        List<Item> items = new List<Item>();

        /// <summary> Setup application as client </summary>
        public override void Setup(NetworkManager networkManager)
        {
            this.networkManager = networkManager;

            #if DEBUG
            Debugging.ConnectEvents();
            if (OnSetUp != null)
                OnSetUp(this, EventArgs.Empty);
            #endif

            networkClient = new NetworkClient();
            networkClient.RegisterHandler(MsgType.Connect, OnConnected);
            networkClient.RegisterHandler(NetworkMessageType.AddItem, OnAddItem);
            networkClient.RegisterHandler(NetworkMessageType.AddItems, OnAddItems);
            networkClient.Connect(networkManager.serverIp, networkManager.serverPort);

            items.Add(new Weapon("Blade of Justice", 10, 10, 15));
            items.Add(new Armor("Armor of Balance", 100, 10));
            items.Add(new Consumable("Potion of Health"));
            networkManager.itemManager.CreateItems(items);

        }

        /// <summary> Event for new connection to server </summary
        void OnConnected(NetworkMessage connectedMsg)
        {
            #if DEBUG
            Debugging.PrintScreen("Connected to server");
            #endif
        }

        /// <summary> Event for single item packet message </summary>
        void OnAddItem(NetworkMessage addItemMsg)
        {
            ItemMessage itemMsg = addItemMsg.ReadMessage<ItemMessage>();
            if (itemMsg.item == null)
            {
                #if DEBUG
                Debugging.PrintScreen("Received item is not valid");
                #endif
                return;
            }

            #if DEBUG
            Debugging.PrintScreen("Received package with one item");
            #endif

            items.Add(itemMsg.item);

            networkManager.itemManager.CreateItems(items);
            networkManager.itemMode.ResetDraggablePanel();
        }

        /// <summary> Event for multiple items packet message </summary>
        void OnAddItems(NetworkMessage addItemsMsg)
        {
            ItemArrayMessage itemArrayMsg = addItemsMsg.ReadMessage<ItemArrayMessage>();

            #if DEBUG
            if (itemArrayMsg.items.Length <= 0)
                Debugging.PrintScreen("Received item package is not valid");
            else
                Debugging.PrintScreen("Received package with multiple items");
            #endif

            foreach (Item tempItem in itemArrayMsg.items)
            {
                if (tempItem == null)
                    continue;

                items.Add(tempItem);
            }

            networkManager.itemManager.CreateItems(items);
        }

        public void SendItem(Item item)
        {
            ItemMessage addItemMessage = new ItemMessage();
            addItemMessage.item = item;
            networkClient.Send(NetworkMessageType.AddItem, addItemMessage);
        }
    }
}
