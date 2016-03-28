using UnityEngine;
using System.Collections;

namespace UserInterface
{
    [ExecuteInEditMode]
    public class UniformScale : MonoBehaviour
    {
        public float updateInterval = 0.1f;
        Coroutine updateCoroutine = null;

        void Start()
        {
            if (updateCoroutine == null)
                StartCoroutine(SetUniformScale());    
        }

        IEnumerator SetUniformScale()
        {
            while(true)
            {
                Vector3 parentScale = new Vector3(transform.lossyScale.x / transform.localScale.x,
                                                  transform.lossyScale.y / transform.localScale.y,
                                                  transform.lossyScale.z / transform.localScale.z);

                Vector3 tempScale = new Vector3(transform.localScale.x / parentScale.x,
                                                transform.localScale.y / parentScale.y,
                                                transform.localScale.z / parentScale.z);

                transform.localScale = tempScale;
                yield return new WaitForSeconds(updateInterval);
            }
        }

    }
}
