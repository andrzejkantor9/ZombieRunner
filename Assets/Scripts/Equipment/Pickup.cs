using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Equipment
{
    ///Requires \"Player\" tag, requires to be inherited from by a proper pickup
    public class Pickup : MonoBehaviour
    {

    #region Events
        //wanted to do option to set OnPickup either in editor or in code
        // [SerializeField]
        // bool _assignEventInInspector = false;
        // [SerializeField]
        // protected UnityEngine.Events.UnityEvent OnPickup = new UnityEngine.Events.UnityEvent();
    #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void OnTriggerEnter(Collider other) 
        {
            if (other.gameObject.CompareTag(MyDebug.Debug.PLAYER_TAG))
            {
                MyDebug.Debug.Log($"{gameObject.name} picked up!");
                // OnPickup.Invoke();
                OnPickup();
            }            
        }

        protected virtual void OnPickup()
        {
            Destroy(gameObject, .01f); 
        }
    }
}