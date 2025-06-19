// Filename: EnemyData.cs
// Location: Assets/_Project/Scripts/Data/

using UnityEngine;

namespace _Project.Scripts.Data
{
    [CreateAssetMenu(fileName = "New EnemyData", menuName = "Data/Enemy Data")]
    public class EnemyData : ScriptableObject
    {
        [Header("Info")]
        public string enemyName;
        public Sprite sprite; // 讓數據也能決定外觀

        [Header("Stats")]
        public int maxHealth = 30;
        public float moveSpeed = 3f;
    }
}
