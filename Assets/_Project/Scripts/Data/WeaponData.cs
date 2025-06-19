// Filename: WeaponData.cs
// Location: Assets/_Project/Scripts/Data/

using UnityEngine;

namespace _Project.Scripts.Data
{
    // [CreateAssetMenu] 屬性讓我們能直接在 Project 視窗中右鍵 Create 來建立這個類別的實例資產
    [CreateAssetMenu(fileName = "New WeaponData", menuName = "Data/Weapon Data")]
    public class WeaponData : ScriptableObject
    {
        [Header("Info")]
        public string weaponName;

        [Header("Shooting")]
        public float fireRate = 0.5f; // 每秒可以發射幾次
        public GameObject projectilePrefab; // 發射的子彈 Prefab

        [Header("Stats")]
        public int damage = 10;
    }
}
