﻿using System;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "PLayerConfig", menuName = "Configs/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField, Range(0, 20)] private float speed;

        public float Speed => speed;
    }
}
