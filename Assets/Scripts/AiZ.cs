using UnityEngine;
using System.Collections;

public class AiZ : MonoBehaviour
{

    public Transform target;//set target from inspector instead of looking in Update
    public float speed = 5f;


    void handleAnimation()
    {
        Vector3 mousepos = target.position;
        bool up = false;
        bool down = false;

        if (mousepos.y > transform.position.y)
        {
            up = true;
            down = false;
        }
        else if (mousepos.y < transform.position.y)
        {
            down = true;
            up = false;
        }


             if (up)
            {
                animator.SetBool("WalkUp", true);
                animator.SetBool("WalkDown", false);

            }
            else if (down)
            {
                animator.SetBool("WalkUp", false);
                animator.SetBool("WalkDown", true);

            }

        }


    private Animator animator;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {

        handleAnimation();
        //move towards the player
        if (Vector3.Distance(transform.position, target.position) > 1f)
        {//move if distance from target is greater than 1
            Vector2 dir = (target.position-transform.position).normalized;
            transform.Translate(new Vector3(speed * Time.deltaTime *dir.x, speed * Time.deltaTime*dir.y, 0));
        }

    }

}