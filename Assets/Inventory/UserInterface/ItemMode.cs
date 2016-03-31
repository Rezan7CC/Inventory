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

            UIGrid tempGrid = weaponsGrid.GetComponent<UIGrid>() as UIGrid;
            tempGrid.repositionNow = true;
            ((Transform)draggablePanel.GetComponent<Transform>()).localPosition = Vector3.zero;
        }

        public void OnArmorsClick()
        {
            SetWeaponsActive(false);
            SetArmorsActive(true);
            SetConsumablesActive(false);

            UIGrid tempGrid = armorsGrid.GetComponent<UIGrid>() as UIGrid;
            tempGrid.repositionNow = true;
            ((Transform)draggablePanel.GetComponent<Transform>()).localPosition = Vector3.zero;
        }

        public void OnConsumablesClick()
        {
            SetWeaponsActive(false);
            SetArmorsActive(false);
            SetConsumablesActive(true);

            UIGrid tempGrid = consumablesGrid.GetComponent<UIGrid>() as UIGrid;
            tempGrid.repositionNow = true;
            ((Transform)draggablePanel.GetComponent<Transform>()).localPosition = Vector3.zero;
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