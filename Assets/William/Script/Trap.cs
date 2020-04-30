using UnityEngine;

[CreateAssetMenu(fileName = "Trap", menuName = "Trap/Make New Weapon", order = 2)]

public class Trap : ScriptableObject
{
    [SerializeField] GameObject TrapPrefab = null;

    [SerializeField] float Damage = 0;

    // Update is called once per frame
    void Update()
    {
        
    }
}
