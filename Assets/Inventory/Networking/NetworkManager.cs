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

    public class ItemMessage : MessageBase
    {
        public Item item;
    }

    public class ItemArrayMessage : MessageBase
    {
        public Item[] items;
    }

    public class NetworkManager : MonoBehaviour
    {
        public int serverPort = 8888;
        public string serverIp = "127.0.0.1";

        public IApplication NetworkApplication { get { return application; } }
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