using UnityEngine;
namespace Game.Player
{
    [RequireComponent(typeof(Camera))]
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] float m_FollowSpeed = 15;
        [SerializeField] float m_LookaheadSpeed = 15;
        [SerializeField] float m_MaxLookahead = 15;
        [SerializeField] float m_LookaheadMul = 0.5f;
        [SerializeField] float m_MinZoom = 1;
        [SerializeField] float m_MaxZoom = 4;
        [SerializeField] float m_DefaultSize = 5;
        float zoom = 0;
        Camera cam;
        PlayerCore target;
        Vector2 offset;
        Vector2 position;
        Vector2 targetOffset;
        void Start()
        {
            zoom = m_MinZoom;
            cam = GetComponent<Camera>();
            target = PlayerCore.Local;
        }
        void Update()
        {
            Vector2 deltaOffset = targetOffset - offset;
            float offsetSpeedMul = 0.125f + (Vector2.Distance(targetOffset, offset) / 4);
            offset += Vector2.ClampMagnitude(deltaOffset, m_LookaheadSpeed * Time.unscaledDeltaTime * offsetSpeedMul);

            Vector2 targetPosition = target.transform.position;
            Vector2 deltaPosition = targetPosition - position;
            float positionSpeedMul = 0.125f + (Vector2.Distance(targetPosition, position) / 4);
            position += Vector2.ClampMagnitude(deltaPosition, m_FollowSpeed * Time.unscaledDeltaTime * positionSpeedMul);

            transform.position = (Vector3)(position + offset) + (Vector3.forward * -5);

            if (target.Input.Ctrl)
            {
                targetOffset = Vector2.ClampMagnitude(target.Input.LocalCursorPosition * m_LookaheadMul, m_MaxLookahead);
                float step = (m_MaxZoom - m_MinZoom) / 4;
                zoom += target.Input.Hotbar * step;
                zoom = Mathf.Clamp(zoom, m_MinZoom, m_MaxZoom);
            }
            else targetOffset = Vector2.zero;

            cam.orthographicSize = m_DefaultSize / zoom;
        }
    }
}