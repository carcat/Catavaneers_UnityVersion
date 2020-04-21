using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Health : MonoBehaviour
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

    private void TestTakeDamage()
    {
        if (timeElapsed > nextDamageTime)
        {
            nextDamageTime = timeElapsed + 1f;
            TakeDamage(damageTakenPerSecond);
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(0, currentHealth);

        if (currentHealth == 0)
        {
            Spawner.EnemiesAlive--;
            print(gameObject.name + " has died");
            objectPooler.SetInactive(gameObject);
        }
    }
}

[CustomEditor(typeof(Health))]
public class MyScriptEditor : Editor
{
    override public void OnInspectorGUI()
    {
        var myScript = target as Health;

        myScript.startHealth = EditorGUILayout.FloatField("Start Health", myScript.startHealth);
        myScript.debug = GUILayout.Toggle(myScript.debug, "Debug");

        if (myScript.debug)
            myScript.damageTakenPerSecond = EditorGUILayout.FloatField("Damage Taken Per Second", myScript.damageTakenPerSecond);
    }
}