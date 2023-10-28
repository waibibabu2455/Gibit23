
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

    public Sprite blankSPrite;
    public Sprite yellowSprite;
    public Sprite purpleSprite;
    public Sprite cyanSprite;
    

    private Dictionary<string, string> colourCombineMap = new Dictionary<string, string>();

    private Dictionary<string, Color> colourParmMap = new Dictionary<string, Color>();
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        InitializeColourCombineMap();
        InitializeColourParmMap();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, 0);
        if (health <= 0) {
            Destroy(this.gameObject);
            Player.GetComponent<PlayerController>().ExpUp(Exp);

        }
        if (transform.position.x < -10) {
            Destroy(this.gameObject);

        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet")) {

            string bulletColour = collision.GetComponent<Bullet>().colour;
            ChangeMonsterColor(bulletColour);

        }
    }

    void ChangeMonsterColor(string bulletColour)
    {
        //base monster color with base bullet color
        if(baseColorBulletList.Contains(currentColor) && baseColorBulletList.Contains(bulletColour)){
            if(!string.Equals(currentColor, bulletColour)){

                Debug.Log("in");

                string combineColorStr = currentColor+"+"+bulletColour;
                currentColor = colourCombineMap[combineColorStr];

                switch(currentColor)
                {
                    case "YELLOW":
                        spriteRenderer.sprite = yellowSprite;
                        break;

                    case "CYAN":
                        spriteRenderer.sprite = cyanSprite;
                        break;
                    
                    case "PURPLE":
                        spriteRenderer.sprite = purpleSprite;
                        break;
                        
                    default:
                        break;
                }

            }
        }
    }
}
