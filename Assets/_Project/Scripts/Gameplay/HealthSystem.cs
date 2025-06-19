using UnityEngine;
using System;
namespace _Project.Scripts.Gameplay
{
    public class HealthSystem : MonoBehaviour
    {
        private int _maxHealth;
        private int _currentHealth;

        // 定義一個當物件死亡時觸發的事件，這對於解耦非常重要！
        public event Action OnDeath;
        public void Configure(int health)
        {
            _maxHealth = health;
        }
        private void Start()
        {
            _currentHealth = _maxHealth;
        }
        // 提供一個公開的方法，讓外部（例如子彈）可以對這個物件造成傷害
        public void TakeDamage(int damageAmount)
        {
            // 扣除傷害值，並確保血量不會低於 0
            _currentHealth -= damageAmount;
            _currentHealth = Mathf.Max(_currentHealth, 0);

            Debug.Log($"{gameObject.name} took {damageAmount} damage, current health: {_currentHealth}");

            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Debug.Log($"{gameObject.name} has died.");

            // 觸發 OnDeath 事件，通知所有訂閱者「我死了」
            // 其他腳本（例如 EnemyAI）可以訂閱這個事件來處理死亡後的行為（例如自我毀滅）
            OnDeath?.Invoke();
        }
    }
}


