// Filename: PlayerController.cs
// Location: Assets/_Project/Scripts/Gameplay/

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // [SerializeField] 讓我們可以在 Inspector 中調整這個私有變數

    private Rigidbody2D rb;
    private PlayerInputActions playerInputActions;
    private Vector2 moveInput;

    private void Awake()
    {
        // --- 初始化 ---
        // 獲取 Rigidbody2D 元件
        rb = GetComponent<Rigidbody2D>();

        // 實例化我們的 Input Actions 類
        playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        // 啟用 "Player" Action Map
        playerInputActions.Player.Enable();
    }

    private void OnDisable()
    {
        // 停用 "Player" Action Map
        playerInputActions.Player.Disable();
    }

    private void Update()
    {
        // --- 讀取輸入 ---
        // 每個 Update 幀都從 PlayerInputActions 讀取 "Move" 動作的 Vector2 值
        moveInput = playerInputActions.Player.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        // --- 套用物理移動 ---
        // 因為我們是透過物理來移動，所以要在 FixedUpdate 中進行
        // 將讀取到的輸入轉換為速度，並賦予 Rigidbody
        rb.linearVelocity = moveInput * moveSpeed;
    }
}