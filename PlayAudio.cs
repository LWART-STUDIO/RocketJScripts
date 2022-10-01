using System;
using System.Linq;
using Engine.DI;
using Engine.Senser;
using UnityEngine;

namespace Custom.Logic
{
    public class PlayAudio : MonoBehaviour
    {
        private AudioSource _audioSource;

        private void Awake()
        {
            if(!DIContainer
                   .Collect<ISenser>()
                   .NonNull()
                   .OfType<Senser>()
                   .Last(info => info.type == SenserType.Audio).isEnable)
                return;
            _audioSource = GetComponent<AudioSource>();
            _audioSource.Play();
        }
    }
}
