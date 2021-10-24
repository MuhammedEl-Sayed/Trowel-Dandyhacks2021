using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Bob : MonoBehaviour
{
    public float timeToUp;
    public Vector3 startPos;
    public Vector3 endPos;
    private Transform myTransform;

    // Use this for initialization
    void Start()
    {
        myTransform = GetComponent<Transform>();
        if(x){
            startPos = myTransform.position;
            endPos = new Vector3(myTransform.position.x -2, myTransform.position.y, 0 );
        }
        else{
            startPos = myTransform.position;
            endPos = new Vector3(myTransform.position.x, myTransform.position.y -2, 0 );
        }
        StartCoroutine("BopUpDown");
    }
    bool x = true;
    IEnumerator BopUpDown()
    {
        float timer = 0;
        bool flip = false;
        while (true)
        {
            if(x){
                myTransform.position = Vector3.Lerp(myTransform.position, endPos, timer / timeToUp);

            }
            else{
            myTransform.position = Vector3.Lerp(myTransform.position, endPos, timer / timeToUp);

            }


            if (timer < timeToUp && !flip)
            {
                timer += Time.deltaTime;
            }
            else if (timer > 0 && flip)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                flip = !flip;
            }
            yield return null;
        }
    }
}
