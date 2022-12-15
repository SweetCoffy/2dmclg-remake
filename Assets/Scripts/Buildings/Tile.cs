using UnityEngine;
namespace Game.Buildings
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] bool m_Solid;
        public bool IsSolid => m_Solid;
    }
}