using System;
using System.Collections;
using Custom.Logic.Enemy;
using Engine;
using Engine.DI;
using Main.Level;
using UnityEngine;

namespace Custom.Logic
{
    public class GameStateControl : MonoBehaviour
    {
        private PlayerGunController _playerGunController;
        private EnemyCounter _enemyCounter;
        private bool _gameDone;
        private float _timeToEndGame;
        
        private void Start()
        {
            _playerGunController = FindObjectOfType<LevelBulletSetUp>().PlayerGunController;
            _enemyCounter = FindObjectOfType<EnemyCounter>();
            
        }

        private void Update()
        {
            if(_gameDone)
                return;
            if (_playerGunController.CurrentNumberOfShots <= 0)
            {
                _gameDone = true;
                StartCoroutine(WaitBulletDestroy());
            }
            else if(_enemyCounter.NumberOfEnemys<=0)
            {
                _gameDone = true;
                StartCoroutine(WaitBulletDestroy());
            }
        }

        private IEnumerator WaitBulletDestroy()
        {
            yield return new WaitForSeconds(3f);
            if(_enemyCounter.NumberOfEnemys<=0)
            {
                DIContainer.AsSingle<IMakeCompleted>().MakeCompleted();
            }
            else
            {
                DIContainer.AsSingle<IMakeFailed>().MakeFailed(0,"EmptyBullets");
            }
        }
    }
}
