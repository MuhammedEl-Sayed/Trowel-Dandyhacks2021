using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public enum EnemyType { SmallTree, Boss, Top, Bottom }
    public EnemyType enemy;

    public float health = 3;
    public int damage = 15;
    public float speed = 3;
    public bool turret = false;
   void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Weapon") && !turret)
        {
            health -= GameObject.Find("Player").GetComponent<PlayerBehavior>().damage;
            StartCoroutine(damaged());
        }
    }

           void OnTriggerStay2D(Collider2D col)
    {
        Debug.Log("hurting");
 
        if (col.CompareTag("Player") && start)
        {
            start = false;
            StartCoroutine(StuckDamage());
        }
    }
       void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("stop");
        if (col.CompareTag("Player"))
        {
            start = true;
            StopCoroutine(StuckDamage());
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            GameObject.Find("Player").GetComponent<PlayerBehavior>().DoDamage(damage);

        }
    }
bool start= true;
    void OnCollisionStay2D(Collision2D other)
    {
        
        Debug.Log("hurting");
 
        if (other.collider.CompareTag("Player") && start)
        {
            start = false;
            StartCoroutine(StuckDamage());
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        Debug.Log("stop");
        if (other.collider.CompareTag("Player"))
        {
            start = true;
            StopCoroutine(StuckDamage());
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    private void ConfigEnemy(int type)
    {

    }
    IEnumerator damaged()
    {

        GetComponent<SpriteRenderer>().color = Color.red;
        FindObjectOfType<AudioManager>().Play("EnemySound");
        yield return new WaitForSeconds(0.3f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    IEnumerator StuckDamage()
    {
        while(!start){
        GameObject.Find("Player").GetComponent<PlayerBehavior>().DoDamage(damage);
        yield return new WaitForSeconds(1f);

        }

    }
    // Update is called once per frame
    void Update()
    {
        IsPlayerDashing();
        CheckIfDead();
    }
    public float delay;
    void IsPlayerDashing(){
        
                if(GameObject.Find("Player").GetComponent<PlayerBehavior>().dashState == PlayerBehavior.DashState.Dashing || (delay < 0.6f && delay > 0)){
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    delay -= Time.deltaTime;

                }
                else if(delay <=0){

                    delay = 0.6f;
                           gameObject.GetComponent<BoxCollider2D>().enabled = true;
                }

    }
    void CheckIfDead()
    {
        if (health == 0 || health < 0)
        {
            FindObjectOfType<AudioManager>().Play("EnemyDeath");
            GameObject.Find("Player").GetComponent<PlayerBehavior>().DoHeal(Random.Range(5, 10));
            Destroy(gameObject);
        }
    }

}
