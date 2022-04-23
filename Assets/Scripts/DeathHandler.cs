using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
#region CACHE

    [Header("CACHE")]
    [SerializeField]
    Canvas _gameOverCanvas;

#endregion

    ///////////////////////////////////////////////////////////////////////////

    void Start() 
    {        
        _gameOverCanvas.enabled = false;
    }

    public void HandleDeath()
    {
        _gameOverCanvas.enabled = true;
        // Time.timeScale = 0f;
        // GetComponent<StarterAssets.FirstPersonController>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
