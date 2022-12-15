using UnityEngine;
using Game.Buildings;
namespace Game.Player
{
    [RequireComponent(typeof(PlayerInventory))]
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerBuilding : MonoBehaviour
    {
        PlayerInventory inventory;
        PlayerInput input;
        public Vector2Int PlacePosition { get; private set; }
        public bool ValidPlacement { get; private set; } = false;
        public BuildingRotation Rotation { get; private set; }
        BuildingType placeType;
        void Start()
        {
            inventory = GetComponent<PlayerInventory>();
            input = GetComponent<PlayerInput>();
            input.OnRotate += OnRotate;
            input.OnUse += OnUse;
        }
        void OnRotate()
        {
            Rotation = (BuildingRotation)(((int)Rotation + 90) % 360);
        }
        void OnUse()
        {
            if (!ValidPlacement) return;
            if (placeType == null) return;
            inventory.TakeItem(new Items.ItemStack(inventory.SelectedStack.id, 1));
            Building.Place(PlacePosition, placeType, Rotation);
        }
        void Update()
        {
            BuildingType type = null;
            if (!inventory.SelectedStack.IsEmpty && BuildingManager.Instance.BuildingTypes.TryGetValue(inventory.SelectedStack.id, out type))
            {
                ValidPlacement = Building.IsValidPosition(PlacePosition, type) && !input.Ctrl;
                placeType = type;
            }
            else ValidPlacement = false;
            placeType = type;
            PlacePosition = BuildingManager.Instance.Grid.ToGrid(input.WorldCursorPosition);
        }
    }
}