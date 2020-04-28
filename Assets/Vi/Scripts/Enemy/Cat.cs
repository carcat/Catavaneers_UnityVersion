using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Cat : EnemyBase
    {
        override protected void Start()
        {
            base.Start();
            type = Type.Cat;
        }
        
        private void Update()
        {

        }
    }
}