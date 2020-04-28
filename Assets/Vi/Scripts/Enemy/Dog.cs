using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Dog : EnemyBase
    {
        override protected void Start()
        {
            base.Start();
            type = Type.Dog;
        }
        
        private void Update()
        {

        }
    }
}