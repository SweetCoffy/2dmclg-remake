using Game.Items;
using Game.Health;
using UnityEngine;
namespace Game.Buildings
{
    public enum BuildingRotation
    {
        Right = 0,
        Down = 90,
        Left = 180,
        Up = 270,
    }
    public class Building : ObjectHealth
    {
        [SerializeField] protected BuildingType m_BuildingType;
        [SerializeField] protected ItemStack[] m_Drops;
        [SerializeField] protected float m_RequiredTier = 1;
        public BuildingType Type
        {
            get => m_BuildingType;
            internal set => m_BuildingType = value;
        }
        public Vector2Int Position { get; private set; }
        public Vector2Int Facing { get; private set; }
        public BuildingRotation Rotation { get; private set; }
        public ItemContainer Container { get; private set; }
        public float RequiredTier => m_RequiredTier;
        public virtual void Awake()
        {
            SetRotation(Rotation);
        }
        public override void Start()
        {
            base.Start();
            Container = GetComponent<ItemContainer>();
        }
        public virtual void OnEnable()
        {
            Position = BuildingManager.Instance.Register(this);
            transform.position = (Vector2)Position;
        }
        public virtual void OnDisable()
        {
            BuildingManager.Instance.Unregister(this);
        }
        public void SetRotation(BuildingRotation rotation)
        {
            Rotation = rotation;
            Facing = Vector2Int.RoundToInt(Quaternion.Euler(0, 0, (float)rotation) * Vector2.right);
            transform.rotation = Quaternion.Euler(0, 0, (float)rotation);
        }

        public T AdjacentBuilding<T>(Vector2Int direction)
        where T : Building
        {
            return GetBuilding<T>(Position + direction);
        }

        public T FacingBuilding<T>()
        where T : Building
        {
            return AdjacentBuilding<T>(Facing);
        }

        public static T GetBuilding<T>(Vector2Int position)
        where T : Building
        {
            return BuildingManager.Instance.Grid[position].building as T;
        }

        public static bool IsValidPosition(Vector2Int position, BuildingType buildingType)
        {
            if (!BuildingManager.Instance.Grid.InGrid(position)) return false;
            TileData data = BuildingManager.Instance.Grid[position];
            if (data.IsEmpty) return true;
            if (data.building.Type == buildingType) return false;
            return buildingType.CanReplace(data.building.Type);
        }
        public static Building Place(Vector2Int position, BuildingType building, BuildingRotation rotation = BuildingRotation.Right)
        {
            Building placed = Instantiate(building.m_Prefab, (Vector2)position, Quaternion.identity, BuildingManager.Instance.BuildingParent).GetComponent<Building>();
            placed.Rotation = rotation;
            placed.SetRotation(rotation);
            placed.gameObject.layer = BuildingManager.Instance.BuildingLayer;
            return placed;
        }
        public override void Kill(IDamageSource source = null)
        {
            if (!IsAlive) return;
            if (Container)
            {
                foreach (var stack in Container.items)
                {
                    if (stack.IsEmpty) continue;
                    ItemManager.SpawnDrop(Position, stack);
                }
            }
            if (m_Drops != null)
            {
                foreach (var stack in m_Drops)
                {
                    ItemManager.SpawnDrop(Position, stack);
                }
            }
            base.Kill(source);
        }
    }
}