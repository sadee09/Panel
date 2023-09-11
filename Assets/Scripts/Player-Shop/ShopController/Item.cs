using UnityEngine;
[System.Serializable]
    public class Item
    {
        public string name;
        public string price;
        public Sprite itemImage;

        public Item(string name, string price, Sprite itemImage)
        {
            this.name = name;
            this.price = price;
            this.itemImage = itemImage;
        }
    }





