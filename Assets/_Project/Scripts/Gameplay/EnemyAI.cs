// Filename: EnemyAI.cs
// Location: Assets/_Project/Scripts/Gameplay/

using _Project.Scripts.Data;
using UnityEngine;

namespace _Project.Scripts.Gameplay
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private EnemyData enemyData;
        private Rigidbody2D _rb;
        private HealthSystem _healthSystem;
        private Transform _playerTransform; // 儲存玩家的位置

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _healthSystem = GetComponent<HealthSystem>();
            if (enemyData != null)
            {
                GetComponent<SpriteRenderer>().sprite = enemyData.sprite;
                _healthSystem.Configure(enemyData.maxHealth);
            }

            // 訂閱 HealthSystem 的 OnDeath 事件
            _healthSystem.OnDeath += HandleDeath;
        }
    
        // 當物件被摧毀時，好習慣是取消訂閱，避免記憶體洩漏
        private void OnDestroy()
        {
            _healthSystem.OnDeath -= HandleDeath;
        }

        private void Start()
        {
            // 尋找場景中的玩家物件
            // FindObjectOfType 比較耗效能，只在 Start 中呼叫一次是可以接受的
            var player = FindFirstObjectByType<PlayerController>();
            if (player != null)
            {
                _playerTransform = player.transform;
                return;
            }

            if (enemyData != null)
                return;
            enabled = false;
        }

        private void FixedUpdate()
        {
            // 使用 enemyData 中的 moveSpeed
            Vector2 direction = (_playerTransform.position - transform.position).normalized;
            _rb.linearVelocity = direction * enemyData.moveSpeed;
        }

        private void HandleDeath()
        {
            // 收到來自 HealthSystem 的死亡通知後，執行死亡行為
            // 對於最基礎的敵人來說，死亡就是從場上消失
            Destroy(gameObject);
        }
    }
}

