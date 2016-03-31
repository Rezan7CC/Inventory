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
        public Vector2 positionOffsetFactor = new Vector2(1, 1);
        public Transform background;
        Coroutine stretchCoroutine = null;

        void Start()
        {
            if(stretchCoroutine == null)
                stretchCoroutine = StartCoroutine(StretchClipRegion());

            Vector4 clipRange = new Vector4(background.transform.localScale.x * positionOffsetFactor.x + offset.x * 0.5f,
                                            background.transform.localScale.y * positionOffsetFactor.y + offset.y * 0.5f,
                                            panel.clipRange.z, panel.clipRange.w);
            panel.clipRange = clipRange;
        }

        IEnumerator StretchClipRegion()
        {
            while(true)
            {
                Vector4 clipRange = new Vector4(panel.clipRange.x,
                                                panel.clipRange.y,
                                                background.transform.localScale.x - Mathf.Abs(offset.x), background.transform.localScale.y - Mathf.Abs(offset.y));
                                                panel.clipRange = clipRange;
                yield return new WaitForSeconds(updateInterval);
            }

        }
    }
}