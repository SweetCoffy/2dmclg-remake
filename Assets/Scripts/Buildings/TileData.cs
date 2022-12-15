using UnityEngine;
namespace Game.Buildings
{
    public struct TileData
    {
        public Building building;
        public bool IsEmpty => building == null;
    }
}