using UnityEngine.Networking;
using Items;
using Database;

namespace Networking
{
    class Server : IApplication
    {
        NetworkManager networkManager = null;
        DatabaseManager databaseManager = new DatabaseManager();

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
            
        }

        void OnAddItem(NetworkMessage addItemMsg)
        {
            DataMessage itemMessage = addItemMsg.ReadMessage<DataMessage>();
            Item item = (Item)itemMessage.data;
        }
    }
}
