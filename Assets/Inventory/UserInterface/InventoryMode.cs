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

        public ItemMode itemMode;

        void Start()
        {
            SetInventoryPanelActive(true);
            SetDeveloperPanelActive(false);
        }

        public void OnInventoryClick()
        {
            SetInventoryPanelActive(true);
            SetDeveloperPanelActive(false);

            EnableItemButtons(true);
            itemMode.OnWeaponsClick();
        }

        public void OnDeveloperClick()
        {
            SetInventoryPanelActive(false);
            SetDeveloperPanelActive(true);

            EnableItemButtons(false);
        }

        void EnableItemButtons(bool enabled)
        {
            itemMode.weaponsButton.UpdateColor(enabled, true);
            itemMode.armorsButton.UpdateColor(enabled, true);
            itemMode.consumablesButton.UpdateColor(enabled, true);

            ((Collider)itemMode.weaponsButton.GetComponent<Collider>()).enabled = enabled;
            ((Collider)itemMode.armorsButton.GetComponent<Collider>()).enabled = enabled;
            ((Collider)itemMode.consumablesButton.GetComponent<Collider>()).enabled = enabled;
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