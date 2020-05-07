using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerSelectController : MonoBehaviour
{

    public int PlayerID;
    public Image PlayerSelectReference;
    int SelectIndex;
    public List<Image> Selections = new List<Image>();
    public float selectdelay=1f;
    float timer=0;
    public string inputhorizontalaxis;
    public string inputacceptbutton;   //Submit/Interact
    public string inputbackbutton;
    public bool lockedin=false;
 
    // Start is called before the first frame update
    void Start()
    {
        
        SelectIndex = PlayerID;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.T)) //Debug for if you only have one controller
        {
            int readyplayers = 0;
            foreach (PlayerSelectController player in FindObjectsOfType<PlayerSelectController>())
            {
                Debug.Log("h");
                readyplayers++;
                
            }
            if (readyplayers >= 4)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }


       
        if (!lockedin)
        {
            if (Input.GetButtonDown(inputacceptbutton))
            {
             
                lockedin = true;
                
                
                
            }
            else if (Input.GetAxis(inputhorizontalaxis) > 0 && Time.time > timer)
            {
                MoveRight();
                timer = Time.time + selectdelay;
            }
            else if (Input.GetAxis(inputhorizontalaxis) < 0 && Time.time > timer)
            {
                MoveLeft();
                timer = Time.time + selectdelay;
            }
        }
        
    }

    void MoveLeft()
    {
        SelectIndex--;
        if (SelectIndex < 0) //Wrap around if select index is less than 0.
        {
            SelectIndex = 3;
        }

        PlayerSelectReference.transform.position = new Vector3(Selections[SelectIndex].transform.position.x, PlayerSelectReference.transform.position.y, PlayerSelectReference.transform.position.z);
            
    }
    void MoveRight()
    {
        SelectIndex++;
        if (SelectIndex > 3) //Wrap around if select index is less than 0.
        {
            SelectIndex = 0;
        }
        PlayerSelectReference.transform.position = new Vector3(Selections[SelectIndex].transform.position.x, PlayerSelectReference.transform.position.y, PlayerSelectReference.transform.position.z);
    }
}
