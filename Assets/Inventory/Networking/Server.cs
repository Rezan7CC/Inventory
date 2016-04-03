using UnityEngine.Networking;
using Items;
using Database;
using Utility;
using System.Collections.Generic;

namespace Networking
{
    class Server : IApplication
    {
        NetworkManager networkManager = null;
        DatabaseManager databaseManager = new DatabaseManager();
        static int maxItemsPerPackage = 10;

        /// <summary> Setup application as server </summary>
        public override void Setup(NetworkManager networkManager)
        {
            this.networkManager = networkManager;

            #if DEBUG
            Debugging.PrintScreen("Setting up server");
            #endif

            DatabaseManager.Connect();
            NetworkServer.Listen(networkManager.serverPort);
            NetworkServer.RegisterHandler(MsgType.Connect, OnClientConnected);
            NetworkServer.RegisterHandler(NetworkMessageType.AddItem, OnAddItem);
        }

        /// <summary> Event for new connection with a client </summary>
        void OnClientConnected(NetworkMessage clientConnectedMsg)
        {

            #if DEBUG
            Debugging.PrintScreen("Client connected");
            #endif

            List<Weapon> weapons = DatabaseManager.GetWeapons();
            SendItems(clientConnectedMsg.conn.connectionId, weapons.ToArray(), weapons.Count);

            List<Armor> armors = DatabaseManager.GetArmors();
            SendItems(clientConnectedMsg.conn.connectionId, armors.ToArray(), armors.Count);

            List<Consumable> consumables = DatabaseManager.GetConsumable();
            SendItems(clientConnectedMsg.conn.connectionId, consumables.ToArray(), consumables.Count);
        }

        /// <summary> Event for AddItem message from client to server </summary>
        void OnAddItem(NetworkMessage addItemMsg)
        {
            #if DEBUG
            Debugging.PrintScreen("Got item from client");
            #endif

            ItemMessage itemMessage = addItemMsg.ReadMessage<ItemMessage>();
            Item item = itemMessage.item;
            if (item == null)
                return;

            DatabaseManager.AddItem(item);

            RedirectItem(addItemMsg.conn.connectionId, item);
        }

        /// <summary> Send a message to all clients except one specified client </summary>
        public void SendToAllExcept(int connectionId, short msgType, MessageBase message)
        {
            foreach(NetworkConnection connection in NetworkServer.connections)
            {
                if (connection == null || connection.connectionId == connectionId)
                    continue;

                NetworkServer.SendToClient(connectionId, msgType, message);
            }
        }

        /// <summary> Redirect item from client to all other connected clients </summary>
        public void RedirectItem(int sourceConnectionID, Item item)
        {
            ItemMessage addItemMessage = new ItemMessage();
            addItemMessage.item = item;
            SendToAllExcept(sourceConnectionID, NetworkMessageType.AddItem, addItemMessage);
        }

        /// <summary> Convert item array into network packages and send them to a client </summary>
        public void SendItems(int connectionId, Item[] items, int arraySize)
        {
            /// Create packages while there are unpackaged items left
            int i = 0;
            while(i < arraySize)
            {
                /// Create a package with the max size of maxItemsPerPackage and send it to client
                int itemsToSendCount = arraySize - i;
                int packageSize = itemsToSendCount > maxItemsPerPackage ? maxItemsPerPackage : itemsToSendCount;
                Item[] itemPackage = new Item[packageSize];

                for (int ii = 0; ii < packageSize; ii++, i++)
                {
                    if (i >= arraySize)
                    {
                        #if DEBUG
                        Debugging.PrintScreen("Error while converting items into packages");
                        #endif
                        break;
                    }

                    itemPackage[ii] = items[i];
                }

                #if DEBUG
                foreach (Item item in itemPackage)
                {
                    if(item == null)
                        Debugging.PrintScreen("Item in package is null which will cause a serialization error");
                }
                #endif

                ItemArrayMessage itemArrayMsg = new ItemArrayMessage();
                itemArrayMsg.items = itemPackage;
              
                NetworkServer.SendToClient(connectionId, NetworkMessageType.AddItems, itemArrayMsg);
            }

            #if DEBUG
            Debugging.PrintScreen("Sent items to client");
            #endif
        }
    }
}
