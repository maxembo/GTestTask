using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Weapons
{
    public class WeaponSwitcher : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _weapons = new List<GameObject>();

        private PlayerInput _input;
        private GameObject _currentWeapon;
        private int _currentWeaponIndex = 0;

        private InputAction InputSwitchWeapon => _input.Gameplay.SwitchWeapon;
        private void Awake() => _input = new PlayerInput();

        private void Start() => _currentWeapon = _weapons[0];

        private void SwitchWeapon()
        {
            _currentWeapon.SetActive(false);

            _currentWeaponIndex = (_currentWeaponIndex + 1) % _weapons.Count;

            _currentWeapon = _weapons[_currentWeaponIndex];
            _currentWeapon.SetActive(true);
        }

        private void OnEnable()
        {
            InputSwitchWeapon.performed += _ => SwitchWeapon();
            _input.Enable();
        }

        private void OnDisable()
        {
            InputSwitchWeapon.performed -= _ => SwitchWeapon();
            _input.Disable();
        }
    }
}
