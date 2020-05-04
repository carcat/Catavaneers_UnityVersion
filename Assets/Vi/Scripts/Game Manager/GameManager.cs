using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float startDelay = 0;
    public float quitDelay = 0;
    private bool doneOnce = false;

    // Make this the one instance managing pooled objects throughout levels
    #region SINGLETON
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }


    private void Awake()
    {
        if (instance && instance != this)
        {
            Destroy(gameObject);
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    private void Start()
    {
        StartCoroutine(StartDelay());
    }

    private void Update()
    {
        if (SpawnManager.HasFinishedSpawning && SpawnManager.EnemiesAlive <= 0 && !doneOnce)
        {
            StartCoroutine(QuitDelay());
            doneOnce = true;
        }
    }

    private IEnumerator StartDelay()
    {
        SpawnManager.CanSpawn = false;
        yield return new WaitForSeconds(startDelay);
        SpawnManager.CanSpawn = true;
    }

    private IEnumerator QuitDelay()
    {
        yield return new WaitForSeconds(quitDelay);
        QuitGame();
    }

    private static void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
