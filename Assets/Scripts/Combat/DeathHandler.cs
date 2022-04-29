using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
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

            FindObjectOfType<Equipment.WeaponSwitcher>().enabled = false;
            // Time.timeScale = Mathf.Epsilon;
            Enemy.EnemyAI[] allEnemies = FindObjectsOfType<Enemy.EnemyAI>();
            foreach(Enemy.EnemyAI enemy in allEnemies)
            {
                enemy.enabled = false;
            }

            Destroy(gameObject);
        }
    }
}
