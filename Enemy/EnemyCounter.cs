using System;
using System.Collections.Generic;
using UnityEngine;

namespace Custom.Logic.Enemy
{
    public class EnemyCounter : MonoBehaviour
    {
        private int _numberOfEnemys;
        private List<EnemyStateControl> _enemyStateControl=new List<EnemyStateControl>();
        public int NumberOfEnemys => _numberOfEnemys;


        public void MinusEnemy(int count=1)
        {
            _numberOfEnemys -= count;
        }

        public void AddEnemy(EnemyStateControl enemyStateControl)
        {
            _enemyStateControl.Add(enemyStateControl);
            _numberOfEnemys++;
        }
        
    }
}
