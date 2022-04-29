using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Equipment
{
    ///Requires \"Player\" tag, requires to be inherited from by a proper pickup
    public class Pickup : MonoBehaviour
    {

    #region Events
        // [SerializeField]
        // bool _assignEventInInspector = false;
        // [SerializeField]
        protected UnityEngine.Events.UnityEvent _pickupEvent = new UnityEngine.Events.UnityEvent();
    #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void OnTriggerEnter(Collider other) 
        {
            if (other.gameObject.CompareTag(MyDebug.Debug.PLAYER_TAG))
            {
                MyDebug.Debug.Log($"{gameObject.name} picked up!");
                _pickupEvent.Invoke();

                Destroy(gameObject); 
            }            
        }
    }
}