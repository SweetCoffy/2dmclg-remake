using UnityEngine;
using Game.Items;
using Game.Buildings;
namespace Game.Player
{
    public class PlayerInventory : ItemContainer
    {
        [SerializeField] protected int m_HotbarLength;
        [SerializeField] protected float m_YeetVelocity = 7.5f;
        public int SelectedSlot { get; private set; }
        public ItemStack SelectedStack
        {
            get => Items[SelectedSlot];
        }
        protected PlayerInput input;
        public override void Start()
        {
            base.Start();
            input = GetComponent<PlayerInput>();
            input.OnDrop += OnDrop;
            input.OnUse += OnUse;
        }
        void OnDrop()
        {
            ItemStack stack = SelectedStack;
            if (stack.IsEmpty) return;
            if (!input.Ctrl) stack.count = 1;
            DroppedItem dropped = ItemManager.SpawnDrop(transform.position, stack);
            TakeItem(stack);
            dropped.rb.velocity = input.LocalCursorPosition.normalized * m_YeetVelocity;
        }
        void OnUse()
        {
            ItemStack stack = SelectedStack;
            if (stack.IsEmpty) return;
            if (stack.Info is PickaxeItemType pickaxe)
            {
                var grid = BuildingManager.Instance.Grid;
                var position = grid.ToGrid(input.WorldCursorPosition);
                if (!grid.InGrid(position)) return;
                TileData target = grid[position];
                if (!target.building) return;
                float damage = pickaxe.m_Damage * (1 + ((pickaxe.m_Tier - target.building.RequiredTier) / 2));
                if (damage < 0) return;
                target.building.TakeDamage(target.building.MaxHealth / 100 * damage);
            }
        }
        public void Update()
        {
            if (input.Hotbar != 0 && !input.Ctrl) SelectedSlot += (int)Mathf.Sign(input.Hotbar);
            if (SelectedSlot >= m_HotbarLength) SelectedSlot = 0;
            if (SelectedSlot < 0) SelectedSlot = m_HotbarLength - 1;
        }
    }
}