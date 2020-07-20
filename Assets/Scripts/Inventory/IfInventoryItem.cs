using UnityEngine;

namespace UnityTemplateProjects.Inventory
{
    public interface IfInventoryItem
    {
        Sprite image { get; }
        string name { get; }
        int id { get; }

        void onPickup();
    }
}