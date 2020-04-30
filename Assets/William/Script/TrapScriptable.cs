
using UnityEngine;

[CreateAssetMenu(fileName = "Trap", menuName = "Trap/Make New Trap", order = 2)]
public class TrapScriptable : ScriptableObject
{
    [SerializeField] GameObject TrapPrefab = null;

    public void SpawnTrap(Transform DropLocation)
    {
        Instantiate(TrapPrefab, DropLocation.position, Quaternion.identity);
    }
}
