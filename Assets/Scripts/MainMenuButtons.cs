using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public bool start;
    public bool quit;
    public bool load;
    public InputHandling inputHandling;
    bool insideButton = false;
    void Start()
    {
        if (!ES3.FileExists("save.es3") && load)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.gray;


        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            insideButton = true;
            if (Input.GetMouseButtonDown(0))
            {
                DoButton();
                Debug.Log("asdas");
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
             insideButton = false;
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    void DoButton()
    {
        if (start)
        {
            SceneManager.LoadScene("Level 01");
        }
        if (quit)
        {
       
        }
        if (load)
        {
            //do load
        }
    }
    public void StartGame(){
                if (insideButton && start)
        {
            SceneManager.LoadScene("Level 01");
        }
    }
      public  void QuitGame(){
                if (insideButton && quit)
        {
                 Application.Quit();
        }
    }
}
