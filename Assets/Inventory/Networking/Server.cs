using UnityEngine.Networking;

namespace Networking
{
    class Server : IApplication
    {
        NetworkManager networkManager;

        public override void Setup(NetworkManager networkManager)
        {
            this.networkManager = networkManager;

            NetworkServer.Listen(networkManager.serverPort);
            NetworkServer.RegisterHandler(MsgType.Connect, OnClientConnected);
        }

        void OnClientConnected(NetworkMessage clientConnectedMsg)
        {

        }
    }
}
