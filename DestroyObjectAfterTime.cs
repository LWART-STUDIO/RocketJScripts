using System;
using System.Collections;
using UnityEngine;

namespace Custom.Logic
{
    public class DestroyObjectAfterTime : MonoBehaviour
    {
        [SerializeField] private float _time = 3f;

        private void Start()
        {
            StartCoroutine(WaitAndDestroy());
        }

        private IEnumerator WaitAndDestroy()
        {
            yield return new WaitForSecondsRealtime(_time);
            Destroy(gameObject);
        }
    }
}
