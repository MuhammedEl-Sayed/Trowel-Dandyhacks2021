using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearedLevel : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] children;
    void Start()
    {
  
    }
    public int numberOfEnemies(){
        return GameObject.FindObjectsOfType<EnemyBehavior>().Length;
    }
    // Update is called once per frame
    bool first = true;
    void Update()
    {

        if(numberOfEnemies() <= 0 && first){
            first = false;
            Debug.Log("how many" + numberOfEnemies() + " " + first);
            GameObject.Find("Canvas").GetComponent<Perserve>().RandomPowerups();
            
            children[Random.Range(0, 3)].SetActive(true);
        }
    }
   public void CloseArrows(){
       first = true;
        for(int i = 0; i < 3; i++){
            
            children[i].SetActive(false);
        }

    }
}
