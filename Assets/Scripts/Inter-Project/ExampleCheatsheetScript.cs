using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//todo check nested regions

namespace InterProject
{
    public class ExampleCheatsheetScript : MonoBehaviour
    {
        [Space(10)] [Header("PROPERTIES")]
        [SerializeField] [Range(0,1)] [Tooltip("to display in inspector")]
        float _speed = 1f; 
        const string FRIENDLY_TAG = "Friendly";

        [System.Serializable]
        private struct AmmoSlot
        {
            int amount;
        }

        [SerializeField]
        private UnityEngine.Events.UnityEvent _pickupEvent;

		public delegate void ZoomDelegate(bool isZoomPressed);
		public static event ZoomDelegate OnZoomChangedStatic = delegate{};
		public event ZoomDelegate OnZoomChanged = delegate{};
        //instance.OnZoomChanged <+/-=> <function with the same signature>;

        //////////////////////////////////////////////////////////////////////////////////////
        //place one or more assembly definition

        /// <summary>
        /// Setup on awake
        /// </summary>
        /// <param name="isAlive">No params here</param>
        private bool Initialize(bool isAlive)
        {
            if(_pickupEvent == null)
                _pickupEvent = new UnityEngine.Events.UnityEvent();
            _pickupEvent.Invoke();
            UnityEngine.Assertions.Assert.IsFalse(_pickupEvent.GetPersistentEventCount() ==0, $"_pickupEvent has no entries, Object: {gameObject.name}, Script: {GetType().ToString()} ");

            return true;
        }
        /// ?return? <>

    }

}