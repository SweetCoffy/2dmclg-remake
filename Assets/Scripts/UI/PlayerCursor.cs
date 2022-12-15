using UnityEngine;
using Game.Player;
using Game.Buildings;
using Game.DataStructures;
namespace Game.UI
{
    public class PlayerCursor : MonoBehaviour
    {
        [SerializeField] SpriteRenderer m_Renderer;
        [SerializeField] Color m_DefaultColor = Color.white;
        [SerializeField] Color m_InvalidColor = Color.red;
        PlayerInput input;
        PlayerBuilding building;
        void Start()
        {
            input = PlayerCore.Local.Input;
            building = PlayerCore.Local.Building;
        }
        void Update()
        {
            Vector2Int gridPos = building.PlacePosition;
            transform.position = (Vector2)gridPos;
            transform.rotation = Quaternion.Euler(0, 0, (int)building.Rotation);
            m_Renderer.color = building.ValidPlacement ? m_DefaultColor : m_InvalidColor;
        }
    }
}