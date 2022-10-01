using System;
using TMPro;
using UnityEngine;

namespace Custom.Logic.Enemy
{
    public class EnemyNameSetUp : MonoBehaviour
    {
        [SerializeField] private string _name;
        private TMP_Text _text;
        [SerializeField] private bool _useName;

        private void Awake()
        {
            _text = GetComponentInChildren<TMP_Text>();
            _text.text = _name;
            if (!_useName)
            {
                _text.enabled = false;
            }
        }
    }
}
