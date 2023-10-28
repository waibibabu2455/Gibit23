
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletNumListener : MonoBehaviour 
{
    private GameObject Player;

    public TMP_Text YellowBulletnumText;
    public TMP_Text PurpleBulletnumText;
    public TMP_Text CyanBulletnumText;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("FindPlayer", 0.5f);
    
    }

    void FindPlayer()
    {
    Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBulletNum();
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
}
