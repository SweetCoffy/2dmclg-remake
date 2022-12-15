using UnityEngine;
using UnityEngine.UI;
using Game.Items;
using Game.Player;
namespace Game.UI
{
    public class Inventory : MonoBehaviour
    {
        public GameObject itemPrefab;
        public ItemContainer target;
        public PlayerInventory PlayerInventory { get; private set; }
        InventoryItem[] items;
        public RectTransform container;
        public int swap = -1;
        void Start()
        {
            PlayerInventory = target as PlayerInventory;
            items = new InventoryItem[target.capacity];
            for (int i = 0; i < target.capacity; i++)
            {
                var it = items[i] = Instantiate(itemPrefab, container).GetComponent<InventoryItem>();
                it.inv = this;
                it.slot = i;
            }
        }
    }
}