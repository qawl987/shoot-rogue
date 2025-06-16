using UnityEngine;

using System; // 需要引用 System 才能使用 Action

// 我們不需要繼承 MonoBehaviour，因為這只是一個靜態的數據容器
// 也就不需要掛載到場景中的任何物件上
public static class GameEvents
{
    // 定義一個當遊戲狀態改變時觸發的事件
    // 它會傳遞新的遊戲狀態作為參數
    public static Action<GameState> OnGameStateChanged;

    // TODO: 隨著開發進程，我們會在這裡加入更多事件
    // 例如：
    // public static Action<EnemyData> OnEnemyDied;
    // public static Action<float> OnPlayerHealthChanged;
    // public static Action OnWeaponUpgraded;
}