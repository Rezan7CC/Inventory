﻿using UnityEngine.Networking;

namespace Networking
{
    class Client : IApplication
    {
        NetworkManager networkManager = null;
        NetworkClient networkClient = null;

        public override void Setup(NetworkManager networkManager)
        {
            this.networkManager = networkManager;

            networkClient = new NetworkClient();
            networkClient.RegisterHandler(MsgType.Connect, OnConnected);
            networkClient.Connect(networkManager.serverIp, networkManager.serverPort);
        }

        void OnConnected(NetworkMessage connectedMsg)
        {
           
        }
    }
}
