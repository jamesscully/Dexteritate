using UnityEngine;

namespace UnityTemplateProjects.Inventory
{
    public class InventoryItem : MonoBehaviour, IfInventoryItem
    {

        public Sprite _image;
        public string _name;
        public int _id;
        
        public Sprite image
        {
            get { return _image; }
        }

        public string name
        {
            get { return _name; }
        }

        public int id
        {
            get { return _id; }
        }
        
        public void onPickup()
        {
            throw new System.NotImplementedException();
        }
    }
}