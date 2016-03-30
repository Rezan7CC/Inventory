using UnityEngine;
using System.Collections;

namespace UserInterface
{
    public class StretchableButton : MonoBehaviour
    {
        public UIStretch stretch;
        public float stretchScale = 1.0f;


        public void ApplyScale()
        {
            if (stretch.style == UIStretch.Style.Horizontal)
                stretch.relativeSize.x *= stretchScale;
            else
                stretch.relativeSize.y *= stretchScale;
        }
    }
}
