using UnityEngine;
using System.Collections;

namespace UserInterface
{
    [ExecuteInEditMode]
    public class ScaleToOffset : MonoBehaviour
    {
        public Transform scaleSource;
        public Transform offsetTarget;
        public Vector3 offsetFactor = new Vector3(0, 0, 0);
        public bool runOnlyOnce = true;
        bool oneRunCompleted = false;
        public bool resetPosition = false;

        void LateUpdate()
        {
            if (runOnlyOnce && oneRunCompleted)
                return;

            if (resetPosition)
                offsetTarget.transform.localPosition = Vector3.zero;

            Vector3 tempScale = scaleSource.transform.localScale;
            tempScale.x *= offsetFactor.x;
            tempScale.y *= offsetFactor.y;
            tempScale.z *= offsetFactor.z;

            offsetTarget.transform.localPosition += tempScale;

            oneRunCompleted = true;
        }
    }
}
