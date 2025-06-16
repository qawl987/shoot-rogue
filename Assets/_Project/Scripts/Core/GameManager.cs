using UnityEngine;

// 定義遊戲可能有的各種狀態。
// 將 enum 寫在 class 外面，這樣其他腳本也能方便地使用它。
public enum GameState
{
    MainMenu,       // 在主選單
    StartingLevel,  // 準備開始關卡 (例如顯示 "Level 1" 字樣)
    Playing,        // 遊戲進行中
    Paused,         // 遊戲暫停
    LevelComplete,  // 關卡完成
    GameOver,       // 玩家死亡，遊戲結束
    GameWon         // 玩家打敗最終 Boss，遊戲勝利
}

public class GameManager : MonoBehaviour
{
    // --- Singleton Pattern ---
    // 這是一個常用的設計模式，確保遊戲中永遠只有一個 GameManager 的實例
    // 並且提供一個全域的靜態屬性方便其他腳本快速存取
    public static GameManager Instance { get; private set; }

    // --- State Management ---
    // 用一個私有變數儲存當前的遊戲狀態
    private GameState _currentState;

    // 提供一個公開的屬性來獲取當前狀態，但設為私有 set，
    // 確保只有 GameManager 自己能改變狀態，符合單一職責原則。
    public GameState CurrentState
    {
        get { return _currentState; }
        private set { _currentState = value; }
    }

    private void Awake()
    {
        // Singleton 實作
        if (Instance != null && Instance != this)
        {
            // 如果場景中已經存在一個 GameManager，就把這個新的摧毀掉
            Destroy(gameObject);
        }
        else
        {
            // 否則，將這個實例設為我們的單例
            Instance = this;
            // 如果我們採用附加式場景載入，需要這行來確保 GameManager 不會被摧毀
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        // 遊戲一開始，我們通常在主選單
        // 注意：我們呼叫 UpdateGameState 方法，而不是直接設定 _currentState
        UpdateGameState(GameState.MainMenu);
    }

    // --- 核心狀態切換方法 ---
    public void UpdateGameState(GameState newState)
    {
        // 如果新的狀態跟目前一樣，就什麼都不做
        if (newState == CurrentState) return;

        CurrentState = newState;

        // 根據新的狀態執行對應的邏輯
        switch (newState)
        {
            case GameState.MainMenu:
                // TODO: 處理回到主選單的邏輯
                Time.timeScale = 1f; // 確保時間流動正常
                break;
            case GameState.StartingLevel:
                // TODO: 處理關卡開始前的準備
                break;
            case GameState.Playing:
                // 當進入遊戲狀態，確保時間是正常流動的
                Time.timeScale = 1f;
                break;
            case GameState.Paused:
                // 當進入暫停狀態，將時間暫停
                Time.timeScale = 0f;
                break;
            case GameState.LevelComplete:
                // TODO: 處理關卡完成的邏輯
                Time.timeScale = 0f; // 通常會暫停遊戲
                break;
            case GameState.GameOver:
                // TODO: 處理遊戲結束的邏輯
                Debug.Log("GAME OVER");
                Time.timeScale = 0f; // 暫停遊戲
                break;
            case GameState.GameWon:
                // TODO: 處理遊戲勝利的邏輯
                Time.timeScale = 0f;
                break;
        }

        // --- 發布事件 ---
        // 這是最關鍵的一步：向所有監聽者廣播「遊戲狀態已改變」
        GameEvents.OnGameStateChanged?.Invoke(newState);

        Debug.Log($"Game state changed to: {newState}");
    }

    // --- 提供一些公開方法給其他系統呼叫 ---
    // 例如，UI 按鈕可以呼叫這些方法
    public void StartNewGame()
    {
        // TODO: 在這裡加入載入第一個關卡的邏輯
        UpdateGameState(GameState.Playing);
    }

    public void PauseGame()
    {
        if (CurrentState == GameState.Playing)
        {
            UpdateGameState(GameState.Paused);
        }
    }

    public void ResumeGame()
    {
        if (CurrentState == GameState.Paused)
        {
            UpdateGameState(GameState.Playing);
        }
    }
}