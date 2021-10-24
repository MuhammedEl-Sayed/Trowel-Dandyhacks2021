using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomItems : MonoBehaviour
{
    private GameObject tilemapObj;
    private Grid grid;
    public GameObject[] randomItems;
    private int randomArray;
    private List<GameObject> gennedStuff = new List<GameObject>();
    Vector3 worldMin;
    Vector3 worldMax;
    void Start()
    {
        tilemapObj = GameObject.Find("Floor");
        Bounds bounds = tilemapObj.GetComponent<Tilemap>().localBounds;
        worldMin = tilemapObj.transform.TransformPoint(bounds.min);
        worldMax = tilemapObj.transform.TransformPoint(bounds.max);
        randomArray = randomItems.Length;
        int numberOfRandomItems = Random.Range(7, 15);


        for (int i = 0; i < numberOfRandomItems; i++)
        {
            GameObject obj = Instantiate(randomItems[Random.Range(0, randomArray - 1)], new Vector3(Random.Range(worldMin.x+1, worldMax.x-3), Random.Range(worldMin.y+1, worldMax.y-3), 0), Quaternion.identity);
            obj.GetComponent<SpriteRenderer>().sortingOrder = 1;
            gennedStuff.Add(obj);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void NewGen(){
        foreach(GameObject g in gennedStuff){
            Destroy(g);
        }
                tilemapObj = GameObject.Find("Floor");
        Bounds bounds = tilemapObj.GetComponent<Tilemap>().localBounds;
        worldMin = tilemapObj.transform.TransformPoint(bounds.min);
        worldMax = tilemapObj.transform.TransformPoint(bounds.max);
        randomArray = randomItems.Length;
        int numberOfRandomItems = Random.Range(7, 15);


        for (int i = 0; i < numberOfRandomItems; i++)
        {
            GameObject obj = Instantiate(randomItems[Random.Range(0, randomArray - 1)], new Vector3(Random.Range(worldMin.x+1, worldMax.x-3), Random.Range(worldMin.y+1, worldMax.y-3), 0), Quaternion.identity);
            obj.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
    }
}
