// Filename: PlayerAttack.cs
// Location: Assets/_Project/Scripts/Gameplay/

using UnityEngine;
using UnityEngine.InputSystem; // 引用新的 Input System 命名空間

namespace _Project.Scripts.Gameplay
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private Transform firePoint;
        [SerializeField] private GameObject projectilePrefab;

        private PlayerInputActions _playerInputActions;
        private Camera _mainCamera; // 為了計算滑鼠的世界座標

        private void Awake()
        {
            _playerInputActions = new PlayerInputActions();
            _mainCamera = Camera.main; // 獲取場景中的主攝影機
        }

        private void OnEnable()
        {
            _playerInputActions.Player.Enable();
            // 訂閱 Fire 事件
            _playerInputActions.Player.Fire.performed += OnFire;
        }

        private void OnDisable()
        {
            _playerInputActions.Player.Disable();
            // 取消訂閱 Fire 事件
            _playerInputActions.Player.Fire.performed -= OnFire;
        }

        private void Update()
        {
            // --- 瞄準邏輯 ---
            HandleAiming();
        }

        private void HandleAiming()
        {
            // 1. 獲取滑鼠在螢幕上的位置
            var mouseScreenPosition = Mouse.current.position.ReadValue();

            // 2. 將螢幕位置轉換為世界座標
            var mouseWorldPosition = _mainCamera.ScreenToWorldPoint(mouseScreenPosition);
            mouseWorldPosition.z = 0; // 確保 z 軸為 0，因為我們在 2D

            // 3. 計算從玩家指向滑鼠的方向
            Vector2 aimDirection = (mouseWorldPosition - transform.position).normalized;

            // 4. 計算角度並旋轉玩家
            // Atan2 返回弧度，需要乘以 Rad2Deg 轉換為角度
            var angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        private void OnFire(InputAction.CallbackContext context)
        {
            // --- 射擊邏輯 ---
            Debug.Log("Fire! Pew pew!"); // 確認事件有被觸發

            // 在 FirePoint 的位置和旋轉角度，生成一個子彈 Prefab
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        }
    }
}

