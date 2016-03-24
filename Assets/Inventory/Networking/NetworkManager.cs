using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Items;

namespace Networking
{
    public struct NetworkMessageType
    {
        public static short AddItem = MsgType.Highest + 1;
        public static short AddItems = MsgType.Highest + 2;
    }

    public class DataMessage : MessageBase
    {
        public object data;
    }

    public class DataArrayMessage : MessageBase
    {
        public object[] data;
        public int arraySize;
    }

    public class NetworkManager : MonoBehaviour
    {
        public int serverPort = 8888;
        public string serverIp = "127.0.0.1";

        IApplication application = null;

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