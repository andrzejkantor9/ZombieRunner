using System.Collections;
using UnityEngine;

namespace ZombieRunner.World
{
    public class PickupAmmo : Pickup
    {
        [Header("PROPERTIES")]
        
        [SerializeField]
        private PickupAmmoData _pickupAmmoData;

        #region CACHE
        [SerializeField][HideInInspector]
        private MeshRenderer _meshRenderer;
        [SerializeField][HideInInspector]
        private MeshFilter _meshFilter;
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        protected override void OnPickup()
        {
            base.OnPickup();
            
            FindObjectOfType<Weapons.Ammo>().IncreaseCurrentAmmo(_pickupAmmoData.ammoType, _pickupAmmoData.ammoAmount);
        }

        private void Awake() 
        {
            AssertCache();
        }

        private void OnValidate() 
        {            
            _meshFilter = GetComponentInChildren<MeshFilter>();
            _meshRenderer = GetComponentInChildren<MeshRenderer>();

            if(!_meshFilter || !_meshRenderer)
            {
                UnityEngine.Assertions.Assert.IsTrue(false, $"missing MeshRenderer or MeshFilter, Object: {gameObject.name}, Script: {GetType().ToString()} ");
                return;
            }

            if(!_pickupAmmoData)
            {
                _meshFilter.mesh = null;
                _meshRenderer.material = null;
                return;
            }

            if(!_pickupAmmoData.pickupMeshPrefab) return;

            CapsuleCollider capsuleCol = GetComponent<CapsuleCollider>();
            MeshRenderer meshRend = _pickupAmmoData.pickupMeshPrefab.GetComponent<MeshRenderer>();
            MeshFilter meshFilt = _pickupAmmoData.pickupMeshPrefab.GetComponent<MeshFilter>();

            if(!capsuleCol || !meshRend || !meshFilt) return;

            _meshFilter.mesh = meshFilt.sharedMesh;
            _meshRenderer.material = meshRend.sharedMaterial;

            capsuleCol.height = meshRend.bounds.size.y;
            capsuleCol.center = meshRend.bounds.center;
            capsuleCol.radius = meshRend.bounds.extents.x > meshRend.bounds.extents.z ? 
            meshRend.bounds.extents.x : meshRend.bounds.extents.z;
        }

        private void AssertCache()
        {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
            UnityEngine.Assertions.Assert.IsNotNull(_pickupAmmoData, $"_pickupAmmoData is null, Object: {gameObject.name}, Script: {GetType().ToString()} ");
#endif
        }
    }
}
