using UnityEngine;
using System.Collections;

namespace UserInterface
{
    public class InventoryMode : MonoBehaviour
    {
        public UIButton inventoryButton;
        public UIButton developerButton;

        public GameObject inventoryPanel;
        public GameObject developerPanel;

        public Color activeColor;
        public Color inactiveColor;

        void Start()
        {
            SetInventoryPanelActive(true);
            SetDeveloperPanelActive(false);
        }

        public void OnInventoryClick()
        {
            SetInventoryPanelActive(true);
            SetDeveloperPanelActive(false);
        }

        public void OnDeveloperClick()
        {
            SetInventoryPanelActive(false);
            SetDeveloperPanelActive(true);
        }

        void SetInventoryPanelActive(bool active)
        {
            inventoryPanel.SetActive(active);
            inventoryButton.defaultColor = active ? activeColor : inactiveColor;
            inventoryButton.UpdateColor(true, true);
        }


        void SetDeveloperPanelActive(bool active)
        {
            developerPanel.SetActive(active);
            developerButton.defaultColor = active ? activeColor : inactiveColor;
            developerButton.UpdateColor(true, true);
        }
    }
}