using System;
using System.Linq;
using Engine.DI;
using Engine.Senser;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

namespace Custom.Logic
{
    public class MainMusicControl : MonoBehaviour
    {
    
        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
           
        }

        private void Update()
        {
            if (!DIContainer
                    .Collect<ISenser>()
                    .NonNull()
                    .OfType<Senser>()
                    .Last(info => info.type == SenserType.Audio).isEnable)
            {
                _audioSource.mute = true;
            }
            else
            {
                _audioSource.mute = false;
            }
               
            
           
        }
    }
}
