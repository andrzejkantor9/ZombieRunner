using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//todo check nested regions
namespace ExampleScripts
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

        public enum CoolEnum : uint
        {
            FineEnum = 1,
            AwesomeEnum = 20,
            TheBestEnum = 52
        }

#region TODO in inter project files
    //change colors materials to change property block based on scriptable object instead of making many materials
    //make script that copy pastes component values to other component
#endregion
#region Checklist - should i use that
    //assembly definition
    //scriptable object / pure c#
    //events
    //inheritance / prefab variants
    //design patterns, managers, components instance manager
    //tooltips / comments
    //addresables, assetBundles, resources
    //make that asset reusable / project independent
    //object pool, caching
    //terrain tools package, pro builder
#endregion
#region Drag&Drop, Resources, Addressables, AssetBundles
#endregion
#region Actions, Events, Funcs, Delegates
        //unity event - has serialization, can be used in editor, has overhead
            //unity event gets freed when script holding it is destroyed
        //system.action is the same as delegate, made for easier use
            //action nor func allow out or ref
            //func returns a value, action does not

        [SerializeField]
        private UnityEngine.Events.UnityEvent _pickupEvent;
        // _pickupEvent.Invoke();
        // UnityEngine.Assertions.Assert.IsFalse(_pickupEvent.GetPersistentEventCount() ==0, $"_pickupEvent has no entries, Object: {gameObject.name}, Script: {GetType().ToString()} ");
        //AddListener

        public delegate void ZoomDelegate(bool isZoomPressed);
		public static event ZoomDelegate OnZoomChangedStatic = delegate{};
		public event ZoomDelegate OnZoomChanged = delegate{};
        //{
            //ZoomDelegate OnZoomChangedLocal = delegate{}; - can be local too
        //}
        //instance.OnZoomChanged <+/-=> <function with the same signature>;
        //________________________________________
        // public event EventHandler Foo
        // {
        //     add
        //     {
        //         // Subscription logic here
        //     }
        //     remove
        //     {
        //         // Unsubscription logic here
        //     }
        // }

        System.Action<bool> zoomAction;
        //zoomAction = <function with the same signature>;
        //*public static event Action OnAnyBubblePopped;
        //OnAnyBubblePopped?.Invoke();
#endregion
#region MonoBehaviour, ScriptableObject, Pure C#
    ////pure c#
        //pure logic or structs not exposed to editor
        //without editor functionality, overhead
        //interfaces, tests, abstract classes
    //scriptable object
        //static serialized data - unity scructs, exposed to editor and saved by editor
    //mono behaviours
        //data bound to instances / runtime
        //functionality driven by engine and driving game
#endregion
#region Inheritance vs composition
    //inheritance for implementation of base class / variance of behaviour
    //composition for adding new functionality
#endregion
        //////////////////////////////////////////////////////////////////////////////////////
        //If you have a lot of text, put it in a file. Don’t put it in fields for editing in the inspector. 
            //Make it easy to change without having to open the Unity editor, and especially without having to save the scene.
        //private new string name; - hiding explicit

        /// <summary>
        /// Setup on awake
        /// </summary>
        /// <param name="isAlive">No params here</param>
        private bool Initialize(bool isAlive)
        {
            return true;
        }
        /// ?return? <>

    }

}