using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "WeaponConfig", menuName = "Configs/WeaponConfig")]
    public class WeaponConfig : ScriptableObject
    {
        [SerializeField] GunConfig gunConfig;
        [SerializeField] ShotgunConfig shotgunConfig;

        public GunConfig GunConfig => gunConfig;

        public ShotgunConfig ShotgunConfig => shotgunConfig;
    }
}
