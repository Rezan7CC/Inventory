using UnityEngine;
using System.Collections;

namespace UserInterface
{
    [ExecuteInEditMode]
    public class StretchBoxCollider : MonoBehaviour
    {
        public float updateInterval = 0.1f;
        public BoxCollider boxCollider;
        public float relativeSize = 0.9f;
        Coroutine stretchCoroutine;

        void Start()
        {
            if (stretchCoroutine == null)
                StartCoroutine(StretchCollider());
        }

        IEnumerator StretchCollider()
        {
            while(true)
            {
                Vector3 size = boxCollider.size;
                size.x = Camera.main.pixelWidth * relativeSize;
                boxCollider.size = size;
                yield return new WaitForSeconds(updateInterval);
            }
        }

    }
}