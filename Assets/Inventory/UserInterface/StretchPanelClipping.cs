using UnityEngine;
using System.Collections;

namespace UserInterface
{
    [ExecuteInEditMode]
    public class StretchPanelClipping : MonoBehaviour
    {
        public UIPanel panel;
        public Camera uiCamera;
        /// <summary> In seconds </summary>
        public float updateInterval = 0.1f;
        public Vector2 offset = new Vector2(0, 0);

        Coroutine stretchCoroutine = null;

        void Start()
        {
            if(stretchCoroutine == null)
                stretchCoroutine = StartCoroutine(StretchClipRegion());
        }

        IEnumerator StretchClipRegion()
        {
            while(true)
            {
                Vector4 clipRange = new Vector4(panel.clipRange.x, panel.clipRange.y, uiCamera.pixelWidth + offset.x, uiCamera.pixelHeight + offset.y);
                panel.clipRange = clipRange;
                yield return new WaitForSeconds(updateInterval);
            }

        }
    }
}