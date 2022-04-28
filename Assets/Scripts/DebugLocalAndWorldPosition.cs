using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyDebug
{
    public class DebugLocalAndWorldPosition : MonoBehaviour
    {
        private void Update() 
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            string positionInfo;
            positionInfo = $"Local position: {transform.localPosition.ToString()}" + 
            $", World position: {transform.position.ToString()}, gameObject: {gameObject.name}";

            CustomDebug.Log(positionInfo);
#endif            
        }
    }
}


