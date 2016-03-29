using UnityEngine;
using System;
using System.Collections;

namespace UserInterface
{
    public class StretchComparer : IComparer
    {
        public int Compare(object object01, object object02)
        {
            StretchableButton stretchableButton01 = object01 as StretchableButton;
            StretchableButton stretchableButton02 = object02 as StretchableButton;
            float positionDiff = 0.0f;
            if (stretchableButton01.stretch.style == UIStretch.Style.Horizontal)
                positionDiff = stretchableButton01.stretch.transform.position.x - stretchableButton02.stretch.transform.position.x;
            else
                positionDiff = stretchableButton01.stretch.transform.position.y - stretchableButton02.stretch.transform.position.y;

            return (int)(100.0f * positionDiff);
        }
    }

    [ExecuteInEditMode]
    public class GridStretchShare : MonoBehaviour
    {
        public UIGrid grid;
        public Camera uiCamera;
        public bool runOnlyOnce = true;
        bool oneRunCompleted = false;

        void Start()
        {
            StretchableButton[] stretchableButtons = grid.GetComponentsInChildren<StretchableButton>();

            float stretchShareValue = 1.0f / stretchableButtons.Length;

            foreach (StretchableButton stretchableButton in stretchableButtons)
            {
                Vector2 relativeSize = stretchableButton.stretch.relativeSize;

                if (grid.arrangement == UIGrid.Arrangement.Horizontal)
                    relativeSize.x = stretchShareValue;
                else
                    relativeSize.y = stretchShareValue;
                stretchableButton.stretch.relativeSize = relativeSize;
            }
        }
        
        void LateUpdate()
        {
            if (runOnlyOnce && oneRunCompleted)
                return;

            StretchableButton[] stretchableButtons = grid.GetComponentsInChildren<StretchableButton>();
            if (stretchableButtons.Length <= 0)
                return;
            StretchComparer stretchComparer = new StretchComparer();
            Array.Sort(stretchableButtons, stretchComparer);

            float singleSize = 0.0f;
            float totalSize = 0.0f;
            float childPosition = 0.0f;
            if (grid.arrangement == UIGrid.Arrangement.Horizontal)
            {
                singleSize = stretchableButtons[0].stretch.transform.localScale.x;
                totalSize = stretchableButtons[0].stretch.transform.localScale.x * stretchableButtons.Length;
                grid.cellWidth = singleSize;
            }
            else
            {
                singleSize = stretchableButtons[0].stretch.transform.localScale.y;
                totalSize = stretchableButtons[0].stretch.transform.localScale.y * stretchableButtons.Length;
                grid.cellHeight = singleSize;
            }
            childPosition = totalSize * -0.5f + singleSize * 0.5f;

            foreach (StretchableButton strechableButton in stretchableButtons)
            {
                if (grid.arrangement == UIGrid.Arrangement.Horizontal)
                {
                    strechableButton.transform.localPosition = new Vector3(childPosition, strechableButton.transform.localPosition.y, 0);
                    childPosition += singleSize;
                }
                else
                {
                    strechableButton.transform.localPosition = new Vector3(strechableButton.transform.localPosition.x, childPosition, 0);
                    childPosition += singleSize;
                }
            }
        }
    }
}
