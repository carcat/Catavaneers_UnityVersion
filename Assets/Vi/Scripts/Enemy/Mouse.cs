using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Mouse : EnemyBase
    {
        override protected void Start()
        {
            base.Start();
            type = Type.Mouse;
        }
        
        void Update()
        {

        }
    }
}