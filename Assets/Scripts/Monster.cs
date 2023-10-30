
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public BoxCollider2D collision2d;
    public int health;
    public float speed;

    public string currentColor;
    public int Exp;
    private GameObject Player;
    private List<string> baseColorList = new List<string> { "RED", "BLUE", "GREEN" };
    private List<string> baseColorBulletList = new List<string> { "RED", "BLUE", "GREEN" };
    private SpriteRenderer spriteRenderer;

    public Sprite yellowSprite;
    public Sprite purpleSprite;
    public Sprite cyanSprite;
    public RuntimeAnimatorController yellowMonsterAnimator;
    public RuntimeAnimatorController purpleMonsterAnimator;
    public RuntimeAnimatorController cyanMonsterAnimator;

    private Animator animator;

    private bool isAttacking = false;

    private bool isHurting = false;

    private bool isDied = false;
    
    

    private Dictionary<string, string> colourCombineMap = new Dictionary<string, string>();

    private Dictionary<string, Color> colourParmMap = new Dictionary<string, Color>();

    private AudioSource audioSource;

    public AudioClip AttackSound;

    public AudioClip hurtSound;

    public AudioClip diedSound;

    public AudioClip switchSound;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        InitializeColourCombineMap();
        InitializeColourParmMap();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void InitializeColourCombineMap()
    {
        colourCombineMap.Add("RED+GREEN", "YELLOW");
        colourCombineMap.Add("RED+BLUE", "PURPLE");
        colourCombineMap.Add("BLUE+RED", "PURPLE");
        colourCombineMap.Add("BLUE+GREEN", "CYAN");
        colourCombineMap.Add("GREEN+RED", "YELLOW");
        colourCombineMap.Add("GREEN+BLUE", "CYAN");
    }

    void InitializeColourParmMap()
    {
        colourParmMap.Add("YELLOW", new Color(1f, 1f, 0f, 1f));
        colourParmMap.Add("PURPLE", new Color(1f, 0f, 1f, 1f));
        colourParmMap.Add("CYAN", new Color(0f, 1f, 1f, 1f));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
        if (health <= 0) {
            IsDied();

        }
        if (transform.position.x < -10) {
            Destroy(this.gameObject);

        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet")) {

            string bulletColour = collision.GetComponent<Bullet>().colour;
            if(!string.Equals(currentColor, bulletColour)){
                ChangeMonsterColor(bulletColour);
            }
            else{
                health -= collision.GetComponent<Bullet>().damage;
                StartCoroutine(IsHurting());
                if(health<=0){IsDied();}
              
            }

        }
        if (collision.CompareTag("Player"))
        {
            IsDied();
        }
    }

    void IsAttacking()
    {
        isAttacking = true;
        animator.SetBool("IsAttack", true);
    }

    void IsDied()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        isDied = true;
        animator.SetBool("IsDied", true);
        audioSource.PlayOneShot(diedSound);
        Destroy(this.gameObject);
    }

    IEnumerator IsHurting()
    {
        isHurting = true;
        animator.SetBool("IsHurt", true);
        audioSource.PlayOneShot(hurtSound);
        AnimationClip hurtClip = animator.runtimeAnimatorController.animationClips[0];
        float hurtDuration = hurtClip.length/2;
        yield return new WaitForSeconds(hurtDuration);

        animator.SetBool("IsHurt", false);
        isHurting = false;
        
    }

    void ChangeMonsterColor(string bulletColour)
    {
        //base monster color with base bullet color
        if(baseColorBulletList.Contains(currentColor) && baseColorBulletList.Contains(bulletColour)){
            if(!string.Equals(currentColor, bulletColour)){


                string combineColorStr = currentColor+"+"+bulletColour;
                currentColor = colourCombineMap[combineColorStr];

                audioSource.PlayOneShot(switchSound);
                switch(currentColor)
                {
                    case "YELLOW":
                        Debug.Log("in");
                        spriteRenderer.sprite = yellowSprite;
                        animator.runtimeAnimatorController = yellowMonsterAnimator;
                        IsAttacking();
                        audioSource.PlayOneShot(AttackSound);
                        break;

                    case "CYAN":
                        spriteRenderer.sprite = cyanSprite;
                        animator.runtimeAnimatorController = cyanMonsterAnimator;
                        IsAttacking();
                        audioSource.PlayOneShot(AttackSound);
                        break;
                    
                    case "PURPLE":
                        spriteRenderer.sprite = purpleSprite;
                        animator.runtimeAnimatorController = purpleMonsterAnimator;
                        IsAttacking();
                        audioSource.PlayOneShot(AttackSound);
                        break;
                        
                    default:
                        break;
                }

            }
        }
    }
}
