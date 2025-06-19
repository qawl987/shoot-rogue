using UnityEngine;

// Filename: CameraFollow.cs
// Location: Assets/_Project/Scripts/Core/

namespace _Project.Scripts.Core
{
    public class CameraFollow : MonoBehaviour
    {
        // 要跟隨的目標物件，也就是我們的玩家
        [SerializeField] private Transform target;

        // 攝影機移動的平滑速度，值越小越平滑，但也越慢
        [SerializeField] private float smoothSpeed = 0.125f;

        // 攝影機相對於目標的偏移量
        // Z 軸設為 -10 是因為 2D 攝影機預設就在這個位置才能看到場景
        [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -10f);

        private void Awake()
        {
            // --- 在一開始就進行檢查 ---
            // 如果 target 沒有在 Inspector 中被指定
            if (target != null)
                return;

            Debug.LogError("CameraFollow: Target is not assigned! Disabling component.", this);
            enabled = false;
        }
        // --- 使用 LateUpdate ---
        // LateUpdate 是在所有 Update 方法執行完畢後才執行的。
        // 把攝影機的邏輯放在這裡，可以確保攝影機是在玩家已經完成移動後才去跟隨，
        // 能有效防止畫面抖動。
        private void LateUpdate()
        {
            // 現在，當程式能執行到這裡時，我們可以 100% 確定 target 不是 null。
            // 所以可以安心地移除原本的 null 檢查。

            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }

        // 這個公開方法可以讓其他腳本（例如 GameManager）在切換關卡後設定新的跟隨目標
        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
            // 當我們透過程式設定新目標時，也要重新啟用元件（如果它之前被停用了）
            if (target != null)
            {
                enabled = true;
            }
        }
    }
}

