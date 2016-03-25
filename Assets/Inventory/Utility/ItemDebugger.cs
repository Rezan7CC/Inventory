using UnityEngine;
using System.Collections;
using Networking;

namespace Utility
{
    public class ItemDebugger : MonoBehaviour
    {
        /// <summary> In seconds </summary>
        public float printInterval = 5.0f;
        public NetworkManager networkManager;

        void Start()
        {
            #if CLIENT
            StartCoroutine(PrintItemCount());
            #endif
        }

        IEnumerator PrintItemCount()
        {
            while (true)
            {
                Client networkClient = (Client)networkManager.NetworkApplication;
                if (networkClient != null)
                {
                    Debugging.PrintScreen("Inventory items count: " + networkClient.InventoryItems.Count);
                }
                yield return new WaitForSeconds(printInterval);
            }
        }
    }
}