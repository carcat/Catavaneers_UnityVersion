using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSystem : MonoBehaviour
{
    [SerializeField] TrapScriptable CurrentTrap;
    [SerializeField] TrapScriptable Trap1;
    [SerializeField] TrapScriptable Trap2;
    [SerializeField] Transform TrapSpawnLocation;

    private void Update()
    {
        if(Trap1 != null)
        {
            CurrentTrap = Trap1;
        }
        else if (Trap1 == null && Trap2 != null)
        {
            CurrentTrap = Trap2;
        }
        else
        {
            CurrentTrap = null;
        }

        if(Input.GetKeyDown(KeyCode.JoystickButton4))
        {
            if (CurrentTrap == null) return;
            CurrentTrap.SpawnTrap(TrapSpawnLocation);
            if(CurrentTrap == Trap1)
            {
                Trap1 = null;
            }
            else if(CurrentTrap == Trap2)
            {
                Trap2 = null;
            }
        }
    }
}
