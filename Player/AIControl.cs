using System;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;

namespace Custom.Logic.Player
{
    [RequireComponent(typeof(PlayerGunController))]
    public class AIControl : MonoBehaviour
    {
        [SerializeField] private bool _enemy;
        public bool UsePlayerAbility;
        [SerializeField] private AITaskData _aiTaskData;
        private PlayerGunController _playerGunController;
        private PlayerRagdollControl _playerRagdollControl;
        public bool Enemy => _enemy;
        private void Start()
        {
            _playerGunController = GetComponent<PlayerGunController>();
            _playerRagdollControl = GetComponent<PlayerRagdollControl>();

        }

        [NaughtyAttributes.Button()]
        public void StartTasks()
        {
            StartCoroutine(DoTasks());
        }

        public void SetUpEnemy()
        {
            if (!UsePlayerAbility)
            {
                _playerGunController.enabled = false;
                _playerRagdollControl.enabled = false;
            }
            else
            {
                _playerGunController.enabled = true;
                _playerRagdollControl.enabled = true;
            }
        }

        private IEnumerator DoTasks()
        {
            if (!UsePlayerAbility)
            {
               
                yield return null;
            }
          
            foreach (var task in _aiTaskData.AITasks)
            {
                if (task.Shoot)
                {
                    _playerGunController.SetShootDirection(task.Direction);
                    _playerGunController.Shoot();
                }

                if (task.Wait)
                {
                    yield return new WaitForSecondsRealtime(task.Time);
                }
                else
                {
                    yield return null;
                }
                
            }
        }
    }

    [Serializable]
    public class AITask
    {
        public bool Shoot;
        [ShowIf("Shoot"),SerializeField] private Vector2 _direction;
        public bool Wait;
        [ShowIf("Wait"),SerializeField] private float _time=0;
        public Vector2 Direction => _direction;
        public float Time => _time;
    }
}
