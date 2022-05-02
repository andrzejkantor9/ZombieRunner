using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieRunner.Combat
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

            FindObjectOfType<ZombieRunner.Weapons.WeaponSwitcher>().enabled = false;
            // Time.timeScale = Mathf.Epsilon;
            ZombieRunner.Enemy.EnemyAI[] allEnemies = FindObjectsOfType<ZombieRunner.Enemy.EnemyAI>();
            foreach(ZombieRunner.Enemy.EnemyAI enemy in allEnemies)
            {
                enemy.enabled = false;
            }

            Destroy(gameObject);
        }
    }
}
