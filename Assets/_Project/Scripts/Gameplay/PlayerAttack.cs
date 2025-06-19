// Filename: PlayerAttack.cs
// Location: Assets/_Project/Scripts/Gameplay/

using _Project.Scripts.Data;
using UnityEngine;
using UnityEngine.InputSystem; // 引用新的 Input System 命名空間

namespace _Project.Scripts.Gameplay
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private WeaponData startingWeapon;
        [SerializeField] private Transform firePoint; // FirePoint 仍然需要
        private WeaponData _currentWeapon;
        
        private PlayerInputActions _playerInputActions;
        private Camera _mainCamera; // 為了計算滑鼠的世界座標
        private float _lastFireTime;

        private void Awake()
        {
            _currentWeapon = startingWeapon;
            _playerInputActions = new PlayerInputActions();
            _mainCamera = Camera.main; // 獲取場景中的主攝影機
        }

        private void OnEnable()
        {
            _playerInputActions.Player.Enable();
            _playerInputActions.Player.Fire.performed += OnFire;
        }

        private void OnDisable()
        {
            _playerInputActions.Player.Disable();
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
            if (_currentWeapon == null) return;

            // --- 射速控制 ---
            // Time.time 是從遊戲開始到現在的總時間
            // 如果距離上次開火的時間小於武器設定的間隔，就不開火
            if (Time.time < _lastFireTime + (1f / _currentWeapon.fireRate))
            {
                return;
            }

            _lastFireTime = Time.time; // 更新上次開火時間
            // --- 生成並設定子彈 ---
            // 從當前武器數據中獲取子彈 Prefab
            var projectileGo = Instantiate(_currentWeapon.projectilePrefab, firePoint.position, firePoint.rotation);

            // 獲取子彈的 Projectile 腳本
            var projectile = projectileGo.GetComponent<Projectile>();
            if (projectile != null)
            {
                // 將武器的傷害值傳遞給子彈
                projectile.SetDamage(_currentWeapon.damage);
            }
        }
    }
}

