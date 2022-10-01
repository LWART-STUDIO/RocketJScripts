using System;
using UnityEngine;

namespace Custom.Logic
{
    public class LevelBulletSetUp : MonoBehaviour
    {
        public PlayerGunController PlayerGunController;
        [SerializeField] private bool _infinityBullets;
        [SerializeField] private int _numberOfBullets;
        public bool InfinityBullets => _infinityBullets;
        public int NumberOfBullets=>_numberOfBullets;


       
    }
}
