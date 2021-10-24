using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandling : MonoBehaviour
{
    // The keycodes you wan to check

    int GetKeysDownCount()
    {
        var keysDown = 0;
        if (Input.GetKeyDown(KeyCode.W))
            keysDown++;
        if (Input.GetKeyDown(KeyCode.A))
            keysDown++;
        if (Input.GetKeyDown(KeyCode.S))
            keysDown++;
        if (Input.GetKeyDown(KeyCode.D))
            keysDown++;

      
        return keysDown;
    }
    public string handleInput()
    {
        
        int keysDown = GetKeysDownCount();
          Debug.Log(keysDown);
        //Handling movement buttons first just because they have a unique case if more are pressed
        if (keysDown == 1)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                return "w";
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                return "a";
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                return "s";
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                return "d";
            }
        }
        if (keysDown == 2)
        {
            string combinedMovementKeys = null;
            if (Input.GetKeyDown(KeyCode.W))
            {
                combinedMovementKeys += "w";
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                combinedMovementKeys += "a";
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                combinedMovementKeys += "s";
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                combinedMovementKeys += "d";
            }
           // Debug.Log(combinedMovementKeys + "combined");
            return combinedMovementKeys;
        }
        if(Input.GetKeyDown(KeyCode.Q)){
            return "q";
        }
        if(Input.GetKeyDown(KeyCode.E)){
            return "e";
        }

        if(Input.GetKeyDown(KeyCode.E)){
            return "e";
        }
        if(Input.GetMouseButtonDown(0)){
            return "m1";
        }
        if(Input.GetMouseButtonDown(1)){
            return "m2";
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            return "esc";
        }
        if(Input.GetKeyDown(KeyCode.LeftShift)){
            return "lshift";
        }
        

        else{
            return null;
        }
    }

    IEnumerator Cooldown(float time)
    {

        yield return new WaitForSeconds(time);

    }
    public Vector3 handleMousePosition(){
        return Input.mousePosition;
    }
}
