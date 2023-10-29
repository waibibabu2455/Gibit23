
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class BulletNumListener : MonoBehaviour 
{
    private GameObject Player;

    public GameObject redLeafModule;
    public GameObject blueLeafModule;
    public GameObject greenLeafModule;
    public GameObject purpleLeafModule;
    public GameObject cyanLeafModule;
    public GameObject yellowLeafModule;

    public Color unselectColor;

    public Color selectColor;


    public TMP_Text YellowBulletnumText;
    public TMP_Text PurpleBulletnumText;
    public TMP_Text CyanBulletnumText;

    public int currentLeafIdx = 0;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("FindPlayer", 0.5f);
        initializeAll();
    
    }

    void FindPlayer()
    {
        Player = GameObject.FindWithTag("Player");
        if(Player != null){
            currentLeafIdx = Player.GetComponent<PlayerController>().currentIndex;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBulletNum();
        UpdateBulletCategory();
    }

    void UpdateBulletNum()
    {
        Debug.Log("update");
        if(Player != null){
            int PurplebulletNum = Player.GetComponent<PlayerController>().PurpleBulletNum;
            int YellowbulletNum = Player.GetComponent<PlayerController>().YellowBulletNum;
            int CyanbulletNum = Player.GetComponent<PlayerController>().CyanBulletNum;

            PurpleBulletnumText.text = PurplebulletNum.ToString();
            YellowBulletnumText.text = YellowbulletNum.ToString();
            CyanBulletnumText.text = CyanbulletNum.ToString();
        }
    }

    void initializeAll()
    {
        redLeafModule.GetComponent<Image>().color = unselectColor;
        blueLeafModule.GetComponent<Image>().color = unselectColor;
        greenLeafModule.GetComponent<Image>().color = unselectColor;
        purpleLeafModule.GetComponent<Image>().color = unselectColor;
        cyanLeafModule.GetComponent<Image>().color = unselectColor;
        yellowLeafModule.GetComponent<Image>().color = unselectColor;
    }

    void UpdateBulletCategory()
    {
        if(Player != null)
        {
            currentLeafIdx = Player.GetComponent<PlayerController>().currentIndex;
            switch(currentLeafIdx)
            {
                case 0:
                    initializeAll();
                    redLeafModule.GetComponent<Image>().color = selectColor;
                    break;
                case 1:
                    initializeAll();
                    blueLeafModule.GetComponent<Image>().color = selectColor;
                    break;
                case 2:
                    initializeAll();
                    greenLeafModule.GetComponent<Image>().color = selectColor;
                    break;
                case 3:
                    initializeAll();
                    purpleLeafModule.GetComponent<Image>().color = selectColor;
                    break;
                case 4:
                    initializeAll();
                    cyanLeafModule.GetComponent<Image>().color = selectColor;
                    break;
                case 5:
                    initializeAll();
                    yellowLeafModule.GetComponent<Image>().color = selectColor;
                    break;
                default:
                    break;

            }
        }
    }
}
