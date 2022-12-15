using UnityEngine;
using System.Collections.Generic;
using Game.DataStructures;
using Game.Items;
namespace Game.Buildings
{
    public class BuildingManager : MonoBehaviour
    {
        [SerializeField] Transform m_BuildingParent;
        public static BuildingManager Instance { get; private set; }
        public Grid<TileData> Grid { get; private set; }
        public Dictionary<string, BuildingType> BuildingTypes { get; private set; }
        public int BuildingLayer { get; private set; }
        public Transform BuildingParent => m_BuildingParent;
        public void Awake()
        {
            BuildingLayer = LayerMask.NameToLayer("Building");
            if (Instance) return;
            Instance = this;
            Grid = new Grid<TileData>(256, 1024);
            BuildingTypes = new Dictionary<string, BuildingType>();
            foreach (var (k, v) in ItemManager.Items)
            {
                if (v.Place != null)
                {
                    Building building = v.Place.GetComponent<Building>();
                    BuildingType type = building.Type;
                    if (type != null) BuildingTypes[k] = type;
                }
            }
        }
        public Vector2Int Register(Building building)
        {
            Vector2Int pos = Vector2Int.RoundToInt((Vector2)building.transform.position);
            TileData data = Grid[pos];
            if (data.building != null)
            {
                Object.Destroy(data.building.gameObject);
            }
            data.building = building;
            Grid[pos] = data;
            return pos;
        }
        public void Unregister(Building building)
        {
            TileData data = Grid[building.Position];
            data.building = null;
            Grid[building.Position] = data;
        }
    }
}