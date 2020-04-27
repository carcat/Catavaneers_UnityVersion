using UnityEngine;
using UnityEditor;
using ObjectPooling;

public class HealthComp : MonoBehaviour
{
    public float startHealth = 100;
    public bool debug;
    public float damageTakenPerSecond;
    
    private float currentHealth = 0;
    private float nextDamageTime = 0;
    private float timeElapsed = 0;

    private static ObjectPooler objectPooler;

    private void Start()
    {
        objectPooler = FindObjectOfType<ObjectPooler>();
        currentHealth = startHealth;
    }

    private void Update()
    {
        if (debug)
            TestTakeDamage();

        timeElapsed += Time.deltaTime;
    }

    /// <summary>
    /// Subtracting health by a preset amount every second for debugging purpose
    /// </summary>
    private void TestTakeDamage()
    {
        if (timeElapsed > nextDamageTime)
        {
            nextDamageTime = timeElapsed + 1f;
            TakeDamage(damageTakenPerSecond);
        }
    }

    /// <summary>
    /// Subtract health by some amount
    /// </summary>
    /// <param name="amount"> The amount that will be subtracted from health </param>
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(0, currentHealth);

        if (currentHealth == 0)
        {
            SpawnManager.EnemiesAlive--;
            print(gameObject.name + " has died");
            objectPooler.SetInactive(gameObject);
        }
    }

    /// <summary>
    /// Add to health by some amount
    /// </summary>
    /// <param name="amount"> The amount that will be added to health </param>
    public void AddHealth(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, startHealth);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(HealthComp))]
public class MyScriptEditor : Editor
{
    override public void OnInspectorGUI()
    {
        var myScript = target as HealthComp;

        myScript.startHealth = EditorGUILayout.FloatField("Start Health", myScript.startHealth);
        myScript.debug = GUILayout.Toggle(myScript.debug, "Debug");

        if (myScript.debug)
            myScript.damageTakenPerSecond = EditorGUILayout.FloatField("Damage Taken Per Second", myScript.damageTakenPerSecond);
    }
}
#endif