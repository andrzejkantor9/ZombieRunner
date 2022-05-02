using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieRunner.World
{
    [CreateAssetMenu(fileName = "NewPickupAmmo", menuName = "ScriptableObjects/PickupAmmo")]
    public class PickupAmmoData : ScriptableObject
    {
        [SerializeField]
        private int _ammoAmount;
        [SerializeField]
        private Weapons.AmmoType _ammoType;
        [SerializeField]
        private MeshRenderer _pickupMeshPrefab;

        public int ammoAmount {get {return _ammoAmount;}}
        public Weapons.AmmoType ammoType {get {return _ammoType;}}
        public MeshRenderer pickupMeshPrefab {get {return _pickupMeshPrefab;}}
    }
}
