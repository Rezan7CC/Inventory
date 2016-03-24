using UnityEngine.Networking;
using Items;
using Database;
using System.Collections.Generic;

namespace Networking
{
    class Server : IApplication
    {
        NetworkManager networkManager = null;
        DatabaseManager databaseManager = new DatabaseManager();
        static int maxItemsPerPackage = 10;

        public override void Setup(NetworkManager networkManager)
        {
            this.networkManager = networkManager;

            DatabaseManager.Connect();
            NetworkServer.Listen(networkManager.serverPort);
            NetworkServer.RegisterHandler(MsgType.Connect, OnClientConnected);
            NetworkServer.RegisterHandler(NetworkMessageType.AddItem, OnAddItem);
        }

        void OnClientConnected(NetworkMessage clientConnectedMsg)
        {
            List<Weapon> weapons = DatabaseManager.GetWeapons();
            SendItems(clientConnectedMsg.conn.connectionId, weapons.ToArray(), weapons.Count);

            List<Armor> armors = DatabaseManager.GetArmors();
            SendItems(clientConnectedMsg.conn.connectionId, armors.ToArray(), armors.Count);

            List<Consumable> consumables = DatabaseManager.GetConsumable();
            SendItems(clientConnectedMsg.conn.connectionId, consumables.ToArray(), consumables.Count);
        }

        void OnAddItem(NetworkMessage addItemMsg)
        {
            DataMessage itemMessage = addItemMsg.ReadMessage<DataMessage>();
            Item item = (Item)itemMessage.data;

            RedirectItem(addItemMsg.conn.connectionId, item);
        }

        public void SendToAllExcept(int connectionId, short msgType, MessageBase message)
        {
            foreach(NetworkConnection connection in NetworkServer.connections)
            {
                if (connection.connectionId == connectionId)
                    continue;

                NetworkServer.SendToClient(connectionId, msgType, message);
            }
        }

        public void RedirectItem(int sourceConnectionID, Item item)
        {
            DataMessage addItemMessage = new DataMessage();
            addItemMessage.data = item;
            SendToAllExcept(sourceConnectionID, NetworkMessageType.AddItem, addItemMessage);
        }

        public void SendItems(int connectionId, Item[] items, int arraySize)
        {
            int i = 0;
            while(i < arraySize)
            {
                Item[] itemPackage = new Item[maxItemsPerPackage];

                for (int ii = 0; ii < maxItemsPerPackage; ii++, i++)
                {
                    if (i >= arraySize)
                        break;

                    itemPackage[ii] = items[i];
                }

                DataArrayMessage dataArrayMsg = new DataArrayMessage();
                dataArrayMsg.arraySize = maxItemsPerPackage;
                dataArrayMsg.data = itemPackage;
                NetworkServer.SendToClient(connectionId, NetworkMessageType.AddItems, dataArrayMsg);
            }
        }
    }
}
