using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Items
{
    [System.Serializable]
    public struct ItemStack
    {
        public string id;
        public int count;
        public static ItemStack operator -(ItemStack a, ItemStack b) => new ItemStack(a.id, a.count - b.count);
        public static ItemStack operator -(ItemStack a, int b) => new ItemStack(a.id, a.count - b);
        public static ItemStack operator +(ItemStack a, ItemStack b) => new ItemStack(a.id, a.count + b.count);
        public static ItemStack operator +(ItemStack a, int b) => new ItemStack(a.id, a.count + b);
        public ItemType Info
        {
            get => ItemManager.Item(this.id);
        }
        public bool IsEmpty
        {
            get => count <= 0 || id == string.Empty;
        }
        public static ItemStack Empty(string id)
        {
            return new ItemStack(id, 0);
        }
        public ItemStack(string id, int count)
        {
            this.id = id;
            this.count = count;
        }
    }

    public class ItemContainer : MonoBehaviour
    {
        public ItemStack[] items;
        public static int stackSize = 256;
        public int capacity = 16;
        public bool canRearrange = false;
        public bool filter = false;
        public bool allowInput = false;
        public bool allowOutput = false;
        public bool IsEmpty
        {
            get
            {
                return !items.Any(stack => !stack.IsEmpty);
            }
        }
        public virtual void Start()
        {
            items = new ItemStack[capacity];
            for (int i = 0; i < capacity; i++)
            {
                items[i] = new ItemStack(string.Empty, 0);
            }
        }
        public int GetItemIndex(string id)
        {
            for (int i = 0; i < capacity; i++)
            {
                var stack = items[i];
                if (stack.id == id && stack.count > 0) return i;
            }
            return -1;
        }
        public int FindFreeSlot()
        {
            if (filter) return -1;
            for (int i = 0; i < capacity; i++)
            {
                var stack = items[i];
                if (stack.count <= 0 || stack.id == string.Empty) return i;
            }
            return -1;
        }
        public ItemStack TakeItem(ItemStack stack)
        {
            int idx = GetItemIndex(stack.id);
            if (idx == -1) return ItemStack.Empty(stack.id);
            var storedStack = items[idx];
            if (storedStack.count < stack.count) return ItemStack.Empty(stack.id);
            items[idx] = storedStack - stack.count;
            return stack;
        }
        public ItemStack TakeFirstItem(int amt)
        {
            for (int i = 0; i < capacity; i++)
            {
                ItemStack stack = items[i];
                if (stack.IsEmpty) continue;
                ItemStack take = new ItemStack(stack.id, Mathf.Min(stack.count, amt));
                items[i] = stack - take;
                return take;
            }
            return ItemStack.Empty(string.Empty);
        }
        public ItemStack AddItem(ItemStack stack)
        {
            int idx = GetItemIndex(stack.id);
            if (idx == -1)
            {
                idx = FindFreeSlot();
                if (idx == -1) return stack;
                items[idx] = ItemStack.Empty(stack.id);
            }
            var storedStack = items[idx];
            if (storedStack.count + stack.count > stackSize) return stack;
            items[idx] = storedStack + stack.count;
            return ItemStack.Empty(stack.id);
        }
        public bool HasItem(string id, int count = 1)
        {
            int idx = GetItemIndex(id);
            return idx != -1 && items[idx].count >= count;
        }
        public void Swap(int a, int b)
        {
            if (!canRearrange) return;
            ItemStack temp = items[a];
            items[a] = items[b];
            items[b] = temp;
        }
    }
}
