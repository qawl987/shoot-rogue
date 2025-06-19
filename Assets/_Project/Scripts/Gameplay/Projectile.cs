using UnityEngine;

namespace _Project.Scripts.Gameplay
{
    public class Projectile : MonoBehaviour
    {
        // --- 參數設定 ---
        [SerializeField] private float speed = 20f;
        [SerializeField] private float lifetime = 3f; // 子彈的存活時間（秒）
        // TODO: 未來可以加入傷害值
        // [SerializeField] private int damage = 10;

        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            // --- 設定初始速度和生命週期 ---
            // 我們在 PlayerAttack 中生成子彈時，已經設定了它的旋轉角度。
            // 所以，子彈的「右邊」(transform.right) 就是它的前進方向。
            // 我們在 Start() 中給予它一個初速度，之後就不用再管它。
            _rb.linearVelocity = transform.right * speed;

            // 在指定的生命週期時間後，自動摧毀這個子彈物件。
            // 這是非常重要的效能優化，避免場上充滿看不見的子彈。
            Destroy(gameObject, lifetime);
        }

        // --- 碰撞處理 ---
        // 因為我們在 Prefab 上設定 Collider 2D 為 Is Trigger，
        // 所以我們需要用 OnTriggerEnter2D 來偵測碰撞。
        private void OnTriggerEnter2D(Collider2D other)
        {
            // other 參數就是我們碰到的那個物件的 Collider。

            // TODO: 在未來，我們會在這裡檢查碰到的是否為敵人。
            // if (other.CompareTag("Enemy"))
            // {
            //     // 對敵人造成傷害
            //     // other.GetComponent<EnemyHealth>().TakeDamage(damage);
            // }

            Debug.Log($"Projectile hit: {other.name}"); // 在 Console 印出碰到的物件名稱，方便除錯

            // 不論碰到什麼，子彈都應該自我毀爾。
            // 注意：這行程式碼會立刻執行，所以它必須是這個方法的最後一步。
            Destroy(gameObject);
        }
    }
}

