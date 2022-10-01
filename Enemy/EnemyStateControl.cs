using System;
using System.Collections;
using UnityEngine;

namespace Custom.Logic.Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyStateControl : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        private const string DEATH_VFX_PATH = "VFX/GenericDeath";
       [SerializeField] private Rigidbody[] _rigidbodies;
       [SerializeField] private Animator _animator;
       [SerializeField] private Material _deathMaterial;
       [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
       private EnemyCounter _enemyCounter;
       private float _deathTimer = 1f;
       private bool _dead;
       [SerializeField] private GameObject _gun;
       [SerializeField] private bool _takeDamageOnFall = true;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            foreach (var variable in _rigidbodies)
            {
                variable.isKinematic = true;
            }

            _animator.enabled = true;
            _enemyCounter = FindObjectOfType<EnemyCounter>();
            _enemyCounter.AddEnemy(this);
        }

        public void Die()
        {
            if(_dead)
                return;
            _gun.SetActive(false);
            _skinnedMeshRenderer.material = _deathMaterial;
            _animator.enabled = false;
            foreach (var variable in _rigidbodies)
            {
                variable.isKinematic = false;
            }

            StartCoroutine(DieCorutiner());
        }
        

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (_rigidbody2D.velocity.magnitude > 1F&&_takeDamageOnFall)
            {
                 Die();
            }
        }
        [NaughtyAttributes.Button()]
        private void GetRigidbodys()
        {
            _rigidbodies = GetComponentsInChildren<Rigidbody>();
        }

        private IEnumerator DieCorutiner()
        {
            _dead = true;
            yield return new WaitForSeconds(_deathTimer);
            Instantiate(Resources.Load(DEATH_VFX_PATH) as GameObject,transform.position,Quaternion.identity);
            _enemyCounter.MinusEnemy();
            Destroy(gameObject);
        }
    }
}
