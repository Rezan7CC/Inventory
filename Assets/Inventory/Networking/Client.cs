using UnityEngine.Networking;
using System.Collections.Generic;
using Items;

namespace Networking
{
    class Client : IApplication
    {
        NetworkManager networkManager = null;
        NetworkClient networkClient = null;

        List<Item> items = new List<Item>();

        /// <summary> Setup application as client </summary>
        public override void Setup(NetworkManager networkManager)
        {
            this.networkManager = networkManager;

            networkClient = new NetworkClient();
            networkClient.RegisterHandler(MsgType.Connect, OnConnected);
            networkClient.RegisterHandler(NetworkMessageType.AddItem, OnAddItem);
            networkClient.RegisterHandler(NetworkMessageType.AddItems, OnAddItems);
            networkClient.Connect(networkManager.serverIp, networkManager.serverPort);
        }

        /// <summary> Event for new connection to server </summary
        void OnConnected(NetworkMessage connectedMsg)
        {
           
        }

        /// <summary> Event for single item packet message </summary>
        void OnAddItem(NetworkMessage addItemMsg)
        {
            DataMessage dataMsg = addItemMsg.ReadMessage<DataMessage>();
            if (dataMsg.data == null)
                return;

            Item item = (Item)dataMsg.data;
            if(item != null)
            {
                items.Add(item);
            }
        }

        /// <summary> Event for multiple items packet message </summary>
        void OnAddItems(NetworkMessage addItemsMsg)
        {
            DataArrayMessage dataArrayMsg = addItemsMsg.ReadMessage<DataArrayMessage>();
            foreach(object itemObj in dataArrayMsg.data)
            {
                if (itemObj == null)
                    continue;

                Item item = (Item)itemObj;
                if (item == null)
                    continue;

                items.Add(item);
            }
        }
    }
}
