using UnityEngine;
using System.Collections;

namespace UserInterface
{
    public class ItemMode : MonoBehaviour
    {
        public UIButton weaponsButton;
        public UIButton armorsButton;
        public UIButton consumablesButton;

        public GameObject weaponsGrid;
        public GameObject armorsGrid;
        public GameObject consumablesGrid;

        public Color activeColor;
        public Color inactiveColor;
        public GameObject draggablePanel;

        void Start()
        {
            SetWeaponsActive(true);
            SetArmorsActive(false);
            SetConsumablesActive(false);
        }

        public void OnWeaponsClick()
        {
            SetWeaponsActive(true);
            SetArmorsActive(false);
            SetConsumablesActive(false);

            ResetDraggablePanel();
        }

        public void OnArmorsClick()
        {
            SetWeaponsActive(false);
            SetArmorsActive(true);
            SetConsumablesActive(false);

            ResetDraggablePanel();
        }

        public void OnConsumablesClick()
        {
            SetWeaponsActive(false);
            SetArmorsActive(false);
            SetConsumablesActive(true);

            ResetDraggablePanel();
        }

        public void ResetDraggablePanel()
        {
            UIGrid tempGrid = armorsGrid.GetComponent<UIGrid>() as UIGrid;
            tempGrid.repositionNow = true;
            ((UIDraggablePanel)draggablePanel.GetComponent<UIDraggablePanel>()).ResetPosition();
            ((Transform)draggablePanel.GetComponent<Transform>()).localPosition = Vector3.zero;
            UIPanel panel = draggablePanel.GetComponent<UIPanel>() as UIPanel;
            Vector4 panelCullingRegion = panel.clipRange;
            panelCullingRegion.y = -55;
            panel.clipRange = panelCullingRegion;
        }

        void SetWeaponsActive(bool active)
        {
            weaponsGrid.SetActive(active);
            weaponsButton.defaultColor = active ? activeColor : inactiveColor;
            weaponsButton.UpdateColor(true, true);
        }

        void SetArmorsActive(bool active)
        {
            armorsGrid.SetActive(active);
            armorsButton.defaultColor = active ? activeColor : inactiveColor;
            armorsButton.UpdateColor(true, true);
        }

        void SetConsumablesActive(bool active)
        {
            consumablesGrid.SetActive(active);
            consumablesButton.defaultColor = active ? activeColor : inactiveColor;
            consumablesButton.UpdateColor(true, true);
        }
    }
}