using UnityEngine;
using UnityEditor;
using ObjectPooling;
using UnityEngine.UI;
using System;
using AI;

public enum CharacterClass { Player, Enemy, Caravan, Obj };
public enum DifficultyLevel { Normal = 4, IronCat = 10, Catapocalypse = 25};

public class HealthComp : MonoBehaviour
{
    [SerializeField] private DifficultyLevel gameDifficulty = DifficultyLevel.Normal;
    public CharacterClass myClass;
    public int startHealth = 100;
    public bool debug;
    public int damageTakenPerSecond;
    
    [SerializeField]
    private int currentHealth = 0;
    private float nextDamageTime = 0;
    private float timeElapsed = 0;
    private bool is_Dead = false;
    [SerializeField]
    private bool is_Regenerating = false;
    [SerializeField]
    float dmg_percentage;


    public Slider health_slider = null;

    private static ObjectPooler objectPooler;


    private void Start()
    {
        if (myClass == CharacterClass.Enemy)
        {
            objectPooler = FindObjectOfType<ObjectPooler>();
        }else if(myClass == CharacterClass.Caravan)
        {

        }
        if (health_slider)
        {
            health_slider.maxValue = startHealth;
        }
        currentHealth = startHealth;
        DisplayHealth();
        
    }

    private void Update()
    {
        if (debug)
            TestTakeDamage();

        timeElapsed += Time.deltaTime;
        if (myClass == CharacterClass.Caravan && is_Regenerating) {
            dmg_percentage = currentHealth % (startHealth / (int)gameDifficulty);
            if (dmg_percentage == 0)
            {
                is_Regenerating = false;
            }
            else { AddHealth(1); }
        }
    
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
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(0, currentHealth);
        DisplayHealth();

        if (currentHealth == 0)
        {
            Dead();
        }
    }

    /// <summary>
    /// Do stuff when dead
    /// </summary>
    private void Dead()
    {
        is_Dead = true;

        switch (myClass)
        {
            case CharacterClass.Player:
            case CharacterClass.Caravan:
            case CharacterClass.Obj:
                gameObject.SetActive(false);
                break;
            case CharacterClass.Enemy:
                objectPooler.SetInactive(gameObject);
                break;
        }
    }

    /// <summary>
    /// Add to health by some amount
    /// </summary>
    /// <param name="amount"> The amount that will be added to health </param>
    public void AddHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, startHealth);
        DisplayHealth();
    }

    /// <summary>
    /// returns currentHealth amount
    /// </summary>
    public float GetCurHealth()
    {
        return currentHealth;
    }

    /// <summary>
    /// Returns if character is dead
    /// </summary>
    public bool IsDead()
    {
        return is_Dead;
    }

    /// <summary>
    /// sets regeneration to true. to be used for the caravan
    /// </summary>
    public void SetIsWaveComplete(bool is_wave_complete)
    {
        is_Regenerating = is_wave_complete;
    }

    /// <summary>
    /// Displays health on the health slider
    /// </summary>
    private void DisplayHealth()
    {
        if(health_slider)
        health_slider.value = currentHealth;
    }
}

//#if UNITY_EDITOR
//[CustomEditor(typeof(HealthComp))]
//public class MyScriptEditor : Editor
//{
//    override public void OnInspectorGUI()
//    {
//        var myScript = target as HealthComp;

//        myScript.startHealth = EditorGUILayout.FloatField("Start Health", myScript.startHealth);
//        myScript.debug = GUILayout.Toggle(myScript.debug, "Debug");

//        if (myScript.debug)
//            myScript.damageTakenPerSecond = EditorGUILayout.FloatField("Damage Taken Per Second", myScript.damageTakenPerSecond);

//        myScript.myClass = (CharacterClass)EditorGUILayout.EnumFlagsField(myScript.myClass);

//    }
//}
//#endif