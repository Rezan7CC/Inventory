using UnityEngine;
using System.Collections;

namespace UserInterface
{
    [ExecuteInEditMode]
    public class ScaleToColliderSize : MonoBehaviour
    {
        public Transform scaleSource;
        public BoxCollider colliderTarget;
        public bool runOnlyOnce = true;
        bool oneRunCompleted = false;

        void LateUpdate()
        {
            if (runOnlyOnce && oneRunCompleted)
                return;

            colliderTarget.size = scaleSource.localScale;
            oneRunCompleted = true;
        }
    }
}