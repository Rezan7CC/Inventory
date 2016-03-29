using UnityEngine;
using System.Collections;

namespace UserInterface
{
    public class ItemProperties : MonoBehaviour
    {
        [System.Serializable]
        public class ItemPortrait
        {
            public UIAtlas spriteAtlas;
            public string spriteName;
        }

        [System.Serializable]
        public class ItemProperty
        {
            public UILabel propertyNameLabel;
            public UILabel propertyValueLabel;

            [HideInInspector]
            public string propertyName;
            [HideInInspector]
            public string propertyValue;
        }

        public UILabel itemNameLabel;
        [HideInInspector]
        public string itemName;

        public UISprite portraitSprite;
        public ItemPortrait[] itemPortraits = new ItemPortrait[3];
        [HideInInspector]
        public int selectedItemPortrait;

        public ItemProperty[] properties = new ItemProperty[4];

        public void Apply()
        {
            itemNameLabel.text = itemName;

            portraitSprite.atlas = itemPortraits[selectedItemPortrait].spriteAtlas;
            portraitSprite.spriteName = itemPortraits[selectedItemPortrait].spriteName;

            foreach(ItemProperty property in properties)
            {
                property.propertyNameLabel.text = property.propertyName;
                property.propertyValueLabel.text = property.propertyValue;
            }
        }

    }
}
