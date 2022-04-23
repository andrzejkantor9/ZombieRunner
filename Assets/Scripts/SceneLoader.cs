using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
#region CACHE

    [Header("CACHE")]
    [SerializeField] 
    UnityEngine.UI.Button _playAgainButton;
    [SerializeField]
    UnityEngine.UI.Button _quitButton;
    
#endregion
    ////////////////////////////////////////////////////////////////////////

    void Awake() 
    {
        // _playAgainButton.onClick.AddListener(delegate {ReloadGame();});        
        // _playAgainButton.onClick.AddListener(ReloadGame);        
        _playAgainButton.onClick.AddListener(() => ReloadGame());        
        _quitButton.onClick.AddListener(() => QuitGame());   
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().path);
        // Time.timeScale = 1f;
        // GetComponent<StarterAssets.FirstPersonController>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void QuitGame()
    {
#if !UNITY_EDITOR
        Application.Quit();
#else
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
