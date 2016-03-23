using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

namespace Networking
{
    public class NetworkManager : MonoBehaviour
    {
        public int serverPort = 8888;
        public string serverIp = "127.0.0.1";

        IApplication application;

        void Awake()
        {
        #if SERVER
            application = new Server();
        #elif CLIENT
            application = new Client();
        #endif
            application.Setup(this);
        }
    }
}