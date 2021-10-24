using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class PlayerBehavior : MonoBehaviour
{
    public class Player
    {
        public float health = 100;
        public float mana = 100;
        public float stamina = 100;
        public bool canMove = true;
        public bool isMoving = false;
        public bool canSwing = true;
        public bool canCast = true;
        public bool dead = false;
        public int levelsCleared = 0;
        public int enemiesKilled = 0;

    }
    public Player mainPlayer;
    public float speed = 10f;

    public InputHandling input;

    public Rigidbody2D rb2d;

    private Animator animator;
    public Dictionary<string, bool> powerups = new Dictionary<string, bool>();
    public Perserve perserve;
    public int enemiesKilled;
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Next"))
        {
            mainPlayer.levelsCleared++;
            Vector3 entrance = new Vector3();
            switch (other.name)
            {
                case ("ArrowLeft"):
                    entrance = new Vector3(-14, 4.4f, 0);
                    break;
                case ("ArrowRight"):
                    entrance = new Vector3(13.1f, 4.83f, 0);
                    break;
                case ("ArrowTop"):
                    entrance = new Vector3(-0.62f, -5.83f, 0);
                    break;

            }
            NewLevel(mainPlayer.levelsCleared, enemiesKilled, entrance, perserve.abilitiesRng);
        }

    }
    public GameObject[] randomItems;
    public List<GameObject> gennedStuff = new List<GameObject>();

    Vector3 worldMin;
    Vector3 worldMax;

    private GameObject tilemapObj;
    private int randomArray;
    private int oldNumbersOfEnemies = 3;
    public float damage = 1;
    void increaseReach(float abilitiesRng)
    {
        Debug.Log(abilitiesRng + " og");
        Debug.Log((weapon.transform.localScale.x + abilitiesRng * weapon.transform.localScale.x) + " new");

        weapon.transform.localScale += new Vector3(weapon.transform.localScale.x + abilitiesRng * weapon.transform.localScale.x, weapon.transform.localScale.x + abilitiesRng * weapon.transform.localScale.x, 1f);
    }
    void increaseDamage(float abilitiesRng)
    {
        damage = damage + (abilitiesRng * damage);
    }
    void increaseStamina(float abilitiesRng)
    {
        StaminaUsage = (int)(StaminaUsage - (abilitiesRng * StaminaUsage));

    }
    void increaseDash(float abilitiesRng)
    {
        force = force + (abilitiesRng / 2 * force);
    }
    public GameObject dog;
    void increaseDoggy(float abilitiesRng)
    {
        dog.SetActive(true);
    }
    public ClearedLevel clearedLevel;
    void NewLevel(int levelsCleared, int enemiesKilled, Vector3 entrance, float abilitiesRng)
    {

        Debug.Log("i");
        clearedLevel.CloseArrows();
        transform.position = entrance;
        //need to spawn baddies, and respawn items and delete old.
        GameObject.Find("Floor").GetComponent<RandomItems>().NewGen();
        tilemapObj = GameObject.Find("Floor");
        Bounds bounds = tilemapObj.GetComponent<Tilemap>().localBounds;
        worldMin = new Vector3(-30, -17.4f, 0);
        worldMax = new Vector3(24f, 17.5f, 0);
        abilitiesRng = abilitiesRng / 100;
        Debug.Log(abilitiesRng);
        foreach (KeyValuePair<string, bool> k in powerups)
        {
            if (k.Value)
            {
                if (k.Key == "Reach")
                {
                    increaseReach(abilitiesRng);
                }
                if (k.Key == "Damage")
                {
                    increaseDamage(abilitiesRng);
                }
                if (k.Key == "Stamina")
                {
                    increaseStamina(abilitiesRng);
                }
                if (k.Key == "Dash")
                {
                    increaseDash(abilitiesRng);
                }
                if (k.Key == "Dog")
                {
                    increaseDoggy(abilitiesRng);
                }
            }

        }
        powerups.Clear();
        powerups.Add("Reach", false);
        powerups.Add("Damage", false);
        powerups.Add("Stamina", false);
        powerups.Add("Dash", false);
        powerups.Add("Dog", false);

        randomArray = randomItems.Length;
        int numberOfRandomItems = Random.Range(2, 3);
        Debug.Log(enemiesKilled);

        for (int i = 0; i < (numberOfRandomItems + oldNumbersOfEnemies); i++)
        {

            if ( mainPlayer.levelsCleared < 3)
            {
                GameObject obj = Instantiate(randomItems[0], new Vector3(Random.Range(worldMin.x + 1, worldMax.x - 3), Random.Range(worldMin.y + 1, worldMax.y - 3), 0), Quaternion.identity);
                obj.GetComponent<EnemyBehavior>().enemy = EnemyBehavior.EnemyType.SmallTree;
                obj.GetComponent<EnemyBehavior>().health = 3 + Random.Range(0, 2);
                obj.GetComponent<EnemyBehavior>().damage = 15 + Random.Range(0, 4);
                obj.GetComponent<EnemyBehavior>().speed = 3 + Random.Range(0, 2);
                obj.GetComponent<AiZ>().target = transform;
            }
            else if ( mainPlayer.levelsCleared >= 3 ||  mainPlayer.levelsCleared %3==0)
            {
                if (i == 0)
                {
                    GameObject obj = Instantiate(randomItems[1], new Vector3(0.26f, 6.28f, 0), Quaternion.identity);
       Debug.Log(enemiesKilled);
                    obj.GetComponent<EnemyBehavior>().enemy = EnemyBehavior.EnemyType.Boss;
                    obj.GetComponent<EnemyBehavior>().health = 30;
                    obj.GetComponent<EnemyBehavior>().damage = 10;
                    obj.GetComponent<EnemyBehavior>().speed = 0;
                }
                else
                {
                    GameObject obj = Instantiate(randomItems[0], new Vector3(Random.Range(worldMin.x + 1, worldMax.x - 3), Random.Range(worldMin.y + 1, worldMax.y - 3), 0), Quaternion.identity);

                    obj.GetComponent<EnemyBehavior>().health = 3 + Random.Range(2, 6);
                    obj.GetComponent<EnemyBehavior>().damage = 15 + Random.Range(3, 6);
                    obj.GetComponent<EnemyBehavior>().speed = 3 + Random.Range(2, 3);
                    obj.GetComponent<EnemyBehavior>().enemy = EnemyBehavior.EnemyType.SmallTree;
                    obj.GetComponent<AiZ>().target = transform;
                }
            }
            else
            {
                GameObject obj = Instantiate(randomItems[0], new Vector3(Random.Range(worldMin.x + 1, worldMax.x - 3), Random.Range(worldMin.y + 1, worldMax.y - 3), 0), Quaternion.identity);

                obj.GetComponent<EnemyBehavior>().health = 3 + Random.Range(2, 6);
                obj.GetComponent<EnemyBehavior>().damage = 15 + Random.Range(3, 6);
                obj.GetComponent<EnemyBehavior>().speed = 3 + Random.Range(2, 3);
                obj.GetComponent<EnemyBehavior>().enemy = EnemyBehavior.EnemyType.SmallTree;
                obj.GetComponent<AiZ>().target = transform;

            }
        }
        mainPlayer.levelsCleared ++;
        oldNumbersOfEnemies += numberOfRandomItems;

    }
    // Start is called before the first frame update
    void Start()
    {
        mainPlayer = new Player();
        animator = gameObject.GetComponent<Animator>();
        input = FindObjectOfType(typeof(InputHandling)) as InputHandling;
        weaponAnim = weapon.GetComponent<Animator>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        powerups.Add("Reach", false);
        powerups.Add("Damage", false);
        powerups.Add("Stamina", false);
        powerups.Add("Dash", false);
        powerups.Add("Dog", false);





    }

    // Update is called once per frame
    void Update()
    {
        handleMovement();
        handleAnimation();
        handleAttacks();
    }
    public Slider staminaBar;
    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);
    private Coroutine regen;
    private void UseStamina(int amount)
    {
        if (mainPlayer.stamina - amount >= 0)
        {
            mainPlayer.stamina -= amount;
            staminaBar.value = mainPlayer.stamina;
            if (regen != null) StopCoroutine(regen);
            regen = StartCoroutine(RegenStamina());
        }
    }
    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(1);

        while (mainPlayer.stamina < 100)
        {
            mainPlayer.stamina += 10;
            staminaBar.value = mainPlayer.stamina;
            yield return regenTick;
        }
    }
    private bool dashing = false;
    private float dashCD = 0.5f;
    public DashState dashState;
    public float dashTimer;
    public float maxDash = 1.3f;

    public Vector2 savedVelocity;
    public TrailRenderer[] trails;
    public float force = 15f;
    public int StaminaUsage = 25;
    private void handleMovement()
    {
        if (mainPlayer.canMove)
        {
            if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
            {
                mainPlayer.isMoving = false;
            }
            else
            {
                mainPlayer.isMoving = true;
            }
            var v3 = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0.0f);
            transform.Translate(speed * v3.normalized * Time.deltaTime);
            //if they try to dash and they are not dashing and have enough stamina
            var v2 = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));


            switch (dashState)
            {
                case DashState.Ready:
                    var isDashKeyDown = Input.GetKeyDown(KeyCode.LeftShift);
                    if (isDashKeyDown && mainPlayer.stamina >= 25f)
                    {
                        savedVelocity = rb2d.velocity;
                        trails[0].enabled = true;
                        trails[1].enabled = true;
                        rb2d.velocity = new Vector2(force * v2.normalized.x, force * v2.normalized.y);
                        UseStamina(StaminaUsage);
                        dashState = DashState.Dashing;

                    }

                    break;
                case DashState.Dashing:
                    dashTimer += Time.deltaTime * 3;
                    if (dashTimer >= maxDash)
                    {
                        dashTimer = maxDash;
                        rb2d.velocity = savedVelocity;

                        dashState = DashState.Cooldown;
                    }

                    break;
                case DashState.Cooldown:
                    dashTimer -= Time.deltaTime;
                    trails[0].enabled = false;
                    trails[1].enabled = false;
                    rb2d.velocity = new Vector2(0, 0);
                    if (dashTimer <= 0)
                    {
                        dashTimer = 0;
                        dashState = DashState.Ready;
                    }
                    break;
            }


        }


    }
    public enum DashState
    {
        Ready,
        Dashing,
        Cooldown
    }
    public Sprite[] iFrames;
    private void handleAnimation()
    {
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bool left = false;
        bool right = false;
        bool up = false;
        bool down = false;
        if (mousepos.x > transform.position.x)
        {
            right = true;
            left = false;
        }
        else if (mousepos.x < transform.position.x)
        {
            left = true;
            right = false;
        }
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

        //ok so i check where the mouse is respective to the character. So i'll have two booleans true each time. What we can do is
        //check which value has the greatest difference and move that way. no we need to check direction he is in aka check if horizontal and vertical is greater
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            up = false;
            down = false;
            animator.SetBool("idle", false);
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
        {
            left = false;
            right = false;
            animator.SetBool("idle", false);
        }
        else if (Input.GetAxisRaw("Horizontal") == 0)
        {
            left = false;
            right = false;
        }
        else if (Input.GetAxisRaw("Vertical") == 0)
        {
            up = false;
            down = false;
        }

        if (mainPlayer.isMoving)
        {
            if (right)
            {
                if (dashState == DashState.Dashing)
                {
                    animator.SetBool("rightI", true);
                    animator.SetBool("rightWalk", false);
                }
                else
                {
                    animator.SetBool("rightWalk", true);
                    animator.SetBool("rightI", false);
                }
                animator.SetBool("leftI", false);
                animator.SetBool("upI", false);
                animator.SetBool("downI", false);
                animator.SetBool("downWalk", false);
                animator.SetBool("upWalk", false);
                animator.SetBool("leftWalk", false);
                animator.SetBool("idle", false);
            }
            else if (left)
            {
                if (dashState == DashState.Dashing)
                {
                    animator.SetBool("leftI", true);
                    animator.SetBool("leftWalk", false);

                }
                else
                {
                    animator.SetBool("leftWalk", true);
                    animator.SetBool("leftI", false);
                }
                animator.SetBool("rightI", false);
                animator.SetBool("upI", false);
                animator.SetBool("downI", false);

                animator.SetBool("downWalk", false);
                animator.SetBool("upWalk", false);
                animator.SetBool("rightWalk", false);
                animator.SetBool("idle", false);
            }
            else if (up)
            {
                if (dashState == DashState.Dashing)
                {
                    animator.SetBool("upI", true);
                    animator.SetBool("upWalk", false);

                }
                else
                {
                    animator.SetBool("upWalk", true);
                    animator.SetBool("upI", false);
                }
                animator.SetBool("downI", false);
                animator.SetBool("rightI", false);
                animator.SetBool("leftI", false);
                animator.SetBool("downWalk", false);
                animator.SetBool("leftWalk", false);
                animator.SetBool("rightWalk", false);
                animator.SetBool("idle", false);
            }
            else if (down)
            {
                if (dashState == DashState.Dashing)
                {
                    animator.SetBool("downI", true);
                    animator.SetBool("downWalk", false);

                }
                else
                {
                    animator.SetBool("downWalk", true);
                    animator.SetBool("downI", false);
                }
                animator.SetBool("rightI", false);
                animator.SetBool("leftI", false);
                animator.SetBool("upI", false);
                animator.SetBool("upWalk", false);
                animator.SetBool("leftWalk", false);
                animator.SetBool("rightWalk", false);
                animator.SetBool("idle", false);
            }

        }
        else if (!mainPlayer.isMoving)
        {
            animator.SetBool("downWalk", false);
            animator.SetBool("upWalk", false);
            animator.SetBool("leftWalk", false);
            animator.SetBool("rightWalk", false);
            animator.SetBool("idle", true);

        }


    }

    private Animator weaponAnim;
    public GameObject weapon;
    private bool attacking = false;
    private float attackTimer = 2f;
    private float attackCompareTimer = 2f;
    private float baseCD = 0.2f;
    private bool casting = false;
    private float castTimer = 0.5f;
    private float castCompareTimer = 0.5f;
    private void handleAttacks()
    {
        if (Input.GetMouseButton(0) && !attacking && mainPlayer.canSwing && !mainPlayer.dead)
        {
            attacking = true;
            weapon.GetComponent<BoxCollider2D>().enabled = true;
            //if get click, point arrow and attack
            PointArrow();
            if (Input.GetMouseButtonDown(0))
            {
                weaponAnim.SetBool("swing", true);
                FindObjectOfType<AudioManager>().Play("Swing");
                attackTimer = .5f;
                attackCompareTimer = attackTimer;
            }
        }
        if (attacking)
        {

            if (attackTimer > 0 && attackTimer > (attackCompareTimer - baseCD))
            {
                attackTimer -= Time.deltaTime;
                mainPlayer.canSwing = false;
                mainPlayer.canCast = false;
            }
            else if (attackTimer <= (attackCompareTimer - baseCD))
            {
                attackTimer -= Time.deltaTime;
                mainPlayer.canCast = true;
                weaponAnim.SetBool("swing", false);
                weapon.GetComponent<BoxCollider2D>().enabled = false;
            }
            if (attackTimer == 0 || attackTimer < 0)
            {
                attacking = false;
                mainPlayer.canSwing = true;
            }
        }
        if (Input.GetKey(KeyCode.Q) && !casting && mainPlayer.canCast)
        {
            casting = true;
            //if get click, point arrow and attack
            PointArrow();
            if (Input.GetKeyDown(KeyCode.Q))
            {
                //do spell q, need to do file saving so we can save spells
                castTimer = 2f;
                castCompareTimer = castTimer;
            }
        }
        if (casting)
        {
            if (castTimer > 0 && castTimer > (castCompareTimer - baseCD))
            {
                attackTimer -= Time.deltaTime;
                mainPlayer.canSwing = false;
                mainPlayer.canCast = false;
            }
            else if (castTimer <= (castCompareTimer - baseCD))
            {
                attackTimer -= Time.deltaTime;
                mainPlayer.canSwing = true;
            }
            else if (castTimer == 0 || castTimer < 0)
            {
                casting = false;
                mainPlayer.canCast = true;
            }
        }
    }
    public GameObject Ghost;
    public GameObject ArrowGhost;
    public float DistanceFromPlayer;


    void PointArrow()
    {

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mouseDist = mousePos - transform.position;
        mousePos.z = 0.0f;
        Vector3 pos = mouseDist.normalized * 1.1f;
        float angle = Mathf.Atan2(mouseDist.y, mouseDist.x) * Mathf.Rad2Deg;
        // arrow.transform.rotation = Quaternion.AngleAxis (angle - 180, Vector3.forward);

        Vector3 gmouseDist = mousePos - Ghost.transform.position;
        Vector3 gpos = mouseDist.normalized * 1.1f;
        float gangle = Mathf.Atan2(gmouseDist.y, gmouseDist.x) * Mathf.Rad2Deg;
        ArrowGhost.transform.rotation = Quaternion.AngleAxis(gangle - 180, Vector3.forward);
        ArrowGhost.transform.position = (-(mousePos - Ghost.transform.position).normalized) * DistanceFromPlayer + Ghost.transform.position;
        Debug.DrawRay(Ghost.transform.position, -(mousePos - Ghost.transform.position).normalized, Color.red);
        weapon.transform.position = (Ghost.transform.position - ArrowGhost.transform.position) + transform.position;
        Quaternion rotation = Quaternion.Euler(weapon.transform.rotation.eulerAngles.x, weapon.transform.rotation.eulerAngles.y, ArrowGhost.transform.rotation.eulerAngles.z);
        weapon.transform.rotation = rotation;
        //Quaternion.Euler(arrow.transform.rotation.eulerAngles.x, arrow.transform.rotation.eulerAngles.y, ArrowGhost.transform.rotation.eulerAngles.z);
        //Quaternion Euler yes = (arrow.transform.rotation.eulerAngles.x, arrow.transform.rotation.eulerAngles.y, ArrowGhost.transform.rotation.eulerAngles.z)

    }
    public Player ExportPlayer()
    {
        return mainPlayer;
    }
    public Slider healthBar;
    public void DoDamage(int amount)
    {
        if (dashState != DashState.Dashing)
        {
            StartCoroutine(damaged());
            GameObject.Find("Main Camera").GetComponent<StartEffect>().DoShake(Normalize(amount, 15, 25, 2, 4));
            if (mainPlayer.health - amount >= 0)
            {
                mainPlayer.health -= amount;
                healthBar.value = mainPlayer.health;
            }
            if (mainPlayer.health - amount <= 0)
            {
                OpenDeath();
            }
        }

    }
        public void DoHeal(int amount)
    {

      
   
            if ((mainPlayer.health + amount) < 100)
            {
                mainPlayer.health += amount;
                healthBar.value = mainPlayer.health;
            }

         

    }
    public GameObject deathScreen;
    void OpenDeath()
    {
        deathScreen.SetActive(true);
        mainPlayer.canMove = false;
        mainPlayer.dead = true;
    }
    IEnumerator damaged()
    {

        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.3f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    float Normalize(float val, float valmin, float valmax, float min, float max)
    {
        return (((val - valmin) / (valmax - valmin)) * (max - min)) + min;
    }

}
