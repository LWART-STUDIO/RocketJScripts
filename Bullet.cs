using System;
using System.Linq;
using Custom.Logic.Enemy;
using DTerrain;
using Engine.DI;
using Engine.Senser;
using UnityEngine;

namespace Custom.Logic
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private AudioClip _pushAudio;
        [SerializeField] private AudioClip _hitSound;
        private ClickAndDestroy[] _clickAndDestroy;
        private Rigidbody2D _rigidbody;
        [SerializeField] private bool _enemyBullet;
        
        
        

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _clickAndDestroy = FindObjectsOfType<ClickAndDestroy>();
            if(!DIContainer
                   .Collect<ISenser>()
                   .NonNull()
                   .OfType<Senser>()
                   .Last(info => info.type == SenserType.Audio).isEnable)
                return; 
            GameObject audioObject = new GameObject();
            AudioSource audioSource= audioObject.AddComponent<AudioSource>();
            audioSource.clip = _pushAudio;
            audioSource.Play();
            audioObject.AddComponent<DestroyObjectAfterTime>();
        }

        private void Update()
        {
            transform.up=-_rigidbody.velocity;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (!_enemyBullet)
            {
                if (col.gameObject.TryGetComponent(out EnemyStateControl enemyStateControl))
                {
                    enemyStateControl.Die();
                    if(!DIContainer
                           .Collect<ISenser>()
                           .NonNull()
                           .OfType<Senser>()
                           .Last(info => info.type == SenserType.Audio).isEnable)
                        return; 
                    GameObject audioObject = new GameObject();
                    AudioSource audioSource= audioObject.AddComponent<AudioSource>();
                    audioSource.clip = _hitSound;
                    audioSource.Play();
                    audioObject.AddComponent<DestroyObjectAfterTime>();
                }
            }
            
            foreach (var variable in _clickAndDestroy)
            {
                variable.Collide(transform.position);
            }

            if (col.gameObject.TryGetComponent(out Rigidbody2D rigidbody2d))
            {
                rigidbody2d.AddForceAtPosition(_rigidbody.velocity*2,col.contacts[0].point);
            }
         //   Destroy(gameObject);
        }
      
    }
}
