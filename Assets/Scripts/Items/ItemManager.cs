using UnityEngine;
using System.Collections.Generic;
namespace Game.Items
{
    public static class ItemManager
    {
        public static Dictionary<string, ItemType> Items { get; private set; }
        static GameObject droppedItemPrefab;
        public static ItemType Item(string id)
        {
            return Items[id];
        }
        public static DroppedItem SpawnDrop(Vector2 position, ItemStack stack)
        {
            DroppedItem droppedItem = Object.Instantiate(droppedItemPrefab, position, Quaternion.identity).GetComponent<DroppedItem>();
            droppedItem.Stack = stack;
            return droppedItem;
        }
        static ItemManager()
        {
            Items = new Dictionary<string, ItemType>();
            ItemType[] types = Resources.LoadAll<ItemType>("Items");
            foreach (var type in types)
            {
                Items[type.id] = type;
            }
            droppedItemPrefab = Resources.Load<GameObject>("Prefabs/DroppedItem");
        }
    }
}