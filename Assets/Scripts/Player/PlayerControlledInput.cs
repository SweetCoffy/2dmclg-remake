using UnityEngine;
namespace Game.Player
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerControlledInput : PlayerInput
    {
        [SerializeField] string m_MovementHorizontal = "Horizontal";
        [SerializeField] string m_MovementVertical = "Vertical";
        [SerializeField] string m_Hotbar = "Mouse ScrollWheel";
        [SerializeField] string m_Ctrl = "Modifier";
        [SerializeField] string m_Rotate = "Rotate";
        [SerializeField] string m_Use = "Fire1";
        [SerializeField] string m_Drop = "Drop";
        [SerializeField] Transform m_RelativeTo;
        PlayerMovement playerMovement;
        Camera mainCamera;
        public override Vector2 WorldCursorPosition
        {
            get => (Vector2)m_RelativeTo.position + LocalCursorPosition;
        }
        public override Vector2 LocalCursorPosition { get; protected set; }
        public override Vector2 Movement { get; protected set; }
        public override float Hotbar { get; protected set; }
        public override bool Ctrl { get; protected set; }
        public override System.Action OnRotate { get; set; }
        public override System.Action OnUse { get; set; }
        public override System.Action OnDrop { get; set; }
        void OnValidate()
        {
            if (m_RelativeTo == null) m_RelativeTo = transform;
        }
        void Start()
        {
            playerMovement = GetComponent<PlayerMovement>();
            mainCamera = Camera.main;
        }
        public void Update()
        {
            Movement = new Vector2(Input.GetAxisRaw(m_MovementHorizontal), Input.GetAxisRaw(m_MovementVertical));
            LocalCursorPosition = (mainCamera.ScreenToWorldPoint(Input.mousePosition) - m_RelativeTo.transform.position);
            Hotbar = Input.GetAxisRaw(m_Hotbar);
            Ctrl = Input.GetButton(m_Ctrl);
            if (Input.GetButtonDown(m_Rotate) && OnRotate != null) OnRotate();
            if (Input.GetButtonDown(m_Use) && OnUse != null) OnUse();
            if (Input.GetButtonDown(m_Drop) && OnDrop != null) OnDrop();

        }
    }
}