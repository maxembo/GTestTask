using System;
using UnityEngine;

namespace Configs
{
    [Serializable]
    public class GunConfig
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private GameObject spawnPoint;
        [SerializeField, Range(0, 20)] private float reload;
        [SerializeField] private Animator animator;

        public GameObject BulletPrefab => bulletPrefab;
        public Transform SpawnPoint => spawnPoint.transform;
        public float Reload => reload;
        public Animator Animator => animator;
    }
}
