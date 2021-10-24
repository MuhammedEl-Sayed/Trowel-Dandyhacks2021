using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Perserve : MonoBehaviour
{
    public GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
    }
    bool toggleMenu = false;
    public float abilitiesRng;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I)){
            RandomPowerups();
        }
                if(Input.GetKeyDown(KeyCode.O)){
            powerupParents.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            toggleMenu = !toggleMenu;
        }
        if (toggleMenu)
        {
            Time.timeScale = 0;
            menu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            menu.SetActive(false);
        }
    }
    public List<GameObject> icons;
    private Vector3[] positions = new Vector3[4];
    private int[] selectedIndex;
    GameObject p1, p2, p3;
    public void RandomPowerups()
    {
        powerupParents.SetActive(true);


        a3.Clear();
        int random = Random.Range(0, 4);
         p1 = icons[random];
        Debug.Log("r1" + random);
        random = Random.Range(0, 4);
         p2 = icons[random];
        Debug.Log("r2" + random);

        random = Random.Range(0,4);
         p3 = icons[random];
        while (p1 == p2 || p1 == p3 || p2 == p3)//While any pair matches
        {
            random = Random.Range(0, 4);
            p2 = icons[random];
            random = Random.Range(0, 4);
            p3 = icons[random];
        }
        Debug.Log("r3" + random);
        p1.SetActive(true);
        p2.SetActive(true);
        p3.SetActive(true);
        positions[0] = new Vector3(-10.0f, 0f, 0f);
        positions[1] = new Vector3(0.0f, 0f, 0f);
        positions[2] = new Vector3(10.0f, 0f, 0f);
        p1.transform.position = positions[0];
        float random2 = Random.Range(5, 10);
        a3.Add(p1.name, random2);
        p1.GetComponentInChildren<TextMeshProUGUI>().text = random2 + "% Increase in " + p1.name;
        p2.transform.position = positions[1];
        random2 = Random.Range(5, 10);
        a3.Add(p2.name, Random.Range(5, 10));
   p2.GetComponentInChildren<TextMeshProUGUI>().text = random2 + "% Increase in " + p2.name;
        p3.transform.position = positions[2];
        random2 = Random.Range(5, 10);
           p3.GetComponentInChildren<TextMeshProUGUI>().text = random2 + "% Increase in " + p3.name;
        a3.Add(p3.name, Random.Range(5, 10));

    }
    public Dictionary<string, float> a3 = new Dictionary<string, float>();

    public void Save()
    {
        ES3.Save("Player Data", GameObject.Find("Player").GetComponent<PlayerBehavior>().ExportPlayer());
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        this.gameObject.SetActive(false);
    }
    /*     public void EnablePowerups(bool mode)
        {
            for(int i = 0; i < powerupsenable.Length; i++){
                powerupsenable[i]
            }
        } */
    public GameObject powerupParents;
    public void SelectedPowerup(string type)
    {

        Debug.Log("hereasdsad");
        switch (type)
        {
            case ("Damage"):
                GameObject.Find("Player").GetComponent<PlayerBehavior>().powerups[type] = true;
                a3.TryGetValue("Damage", out abilitiesRng);
                break;
            case ("Reach"):
                GameObject.Find("Player").GetComponent<PlayerBehavior>().powerups[type] = true;
                a3.TryGetValue("Reach", out abilitiesRng);
                break;
            case ("Dash"):
                GameObject.Find("Player").GetComponent<PlayerBehavior>().powerups[type] = true;
                a3.TryGetValue("Dash", out abilitiesRng);
                break;
            case ("Dog"):
                GameObject.Find("Player").GetComponent<PlayerBehavior>().powerups[type] = true;
                a3.TryGetValue("Dog", out abilitiesRng);
                break;
            case ("Stamina"):
                GameObject.Find("Player").GetComponent<PlayerBehavior>().powerups[type] = true;
                a3.TryGetValue("Stamina", out abilitiesRng);
                break;

        }
                p1.SetActive(false);
        p2.SetActive(false);
        p3.SetActive(false);
        powerupParents.SetActive(false);
    }
}
