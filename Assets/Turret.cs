using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Turret : MonoBehaviour
{


    private bool getPlayerPos = true;
    private float time = 0f;
    private int counter = 0;
    private int removeCounter = 0;
    public float angleMax = -180;
    public float angleMin = -275;
    public float timeToShoot;
    public bool follow = true;
    public bool removePepto = false;
    public float timeToEndStream = 2;
    private List<GameObject> peptoList = new List<GameObject>();
    public GameObject pepto;
    public bool stream = false;
    public float bossTimer = 0;
    public bool AngledShots = false;
    private Vector3 oldplayertransform;
    public Vector3 dir;
    public bool burst = false;
    private int i = 0;
    private bool burstDone = false;
    void Start()
    {
        burstDone = true;
    }

    // Update is called once per frame
    void Update()
    {

        SeekPlayer();
  

    }
    public Vector2 LaserDirection;
    private void SeekPlayer()
    {
   
        if (follow && !laserBeam.stopTurret)
        {

            if (AngledShots)
            {
                StartCoroutine(OldPlayerPos());

            }
            else
            {
                dir =  GameObject.Find("Player").GetComponent<PlayerBehavior>().transform.position - transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 180;
                //Debug.Log (angle - 90f);

            

                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }


        }
        else{
            LaserDirection = transform.up;
        }
    }
    


    public LaserBeam laserBeam;
    IEnumerator OldPlayerPos()
    {
        dir =  GameObject.Find("Player").GetComponent<PlayerBehavior>().transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //Debug.Log (angle - 90f);
        if (SceneManager.GetActiveScene().name == "Level07")
        {
            angleMax = 80;
            angleMin = -270;
        }

        angle = Mathf.Clamp(angle - 90f, angleMin, angleMax);
        yield return new WaitForSeconds(1f);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}