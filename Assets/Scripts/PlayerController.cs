
using System.Runtime.CompilerServices;
using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed;
    public GameObject player;

    public GameObject aimIconPrefab;
    private GameObject aimIconInstance;
    private bool isRunning = false;

    private bool isAttacking = false;
    private Animator animator;

    public float interval;

    public List<GameObject> BulletPrefabLs;

    private GameObject currentBulletPrefab;
    private int currentIndex = 0;

    private Transform muzzlePos;

    private Vector2 mousePos;

    private Vector2 shootDir;

    private float timer;
    
    public Turrent[] turrents;
    
    public float Mana;

    public float MaxMana;

    public float ManaRecovery;

    public int ManaShoot;

    public int Level=1;

    public int LevelUpExp => (Level^2) * 100+1;

    public int Exp=0;

    public GameObject absorbSpritePrefab;

    private GameObject displayedSprite;

    public int CyanBulletNum;
    public int PurpleBulletNum;
    public int YellowBulletNum;


    void Start()
    {
        isRunning = false;
        isAttacking = false;
        animator = GetComponent<Animator>();
        muzzlePos = transform.Find("Muzzle");
        ManaShoot = 10;
        ManaRecovery = 1f;
        Mana = 100;
        MaxMana = 100;
        currentIndex = 0;
        currentBulletPrefab = BulletPrefabLs[currentIndex];

        CyanBulletNum = 0;
        PurpleBulletNum = 0;
        YellowBulletNum = 0;

    }

    // Update is called once per frame
    void Update()
    {
        Moving();
        CheckAttacking();
        AimTarget();
        Shoot();
        SpawnTurret();
        ManaRecover();
        SwitchColor();
        checkAbsorb();
    }

    void checkAbsorb()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(displayedSprite != null){HideAbsorbSprite();}
            else{ShowAbsorbSprite();}
        }
        if(Input.GetKeyUp(KeyCode.Q)){
            if(displayedSprite != null){HideAbsorbSprite();}
        }
    }

    void HideAbsorbSprite(){
        Destroy(displayedSprite);
        displayedSprite = null;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MonsterBullet")) {

            string bulletColour = collision.GetComponent<MonsterBullet>().bulletColor;
            if(displayedSprite != null){AbsorbBullet(bulletColour);}
            Destroy(collision.gameObject);
        }
    }

    void AbsorbBullet(string bulletColour){
        switch(bulletColour)
        {
            case "YELLOW":
                YellowBulletNum += 1;
                break;
            case "PURPLE":
                PurpleBulletNum += 1;
                break;
            case "CYAN":
                CyanBulletNum += 1;
                break;
            default:
                break;
        }
    }

    void ShowAbsorbSprite()
    {
        displayedSprite = Instantiate(absorbSpritePrefab, muzzlePos.position, Quaternion.identity);
        displayedSprite.transform.SetParent(muzzlePos);
    }

    void Shoot()
    {
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        shootDir = -(mousePos + new Vector2(player.transform.position.x, player.transform.position.y)).normalized;

        if (timer != 0) {
            timer -= Time.deltaTime;
            if (timer <= 0) {
                timer = 0;
            }
        }
        if (Input.GetMouseButton(1)) {
            if (Input.GetMouseButtonDown(0)) {
                if (timer == 0) {
                    Fire();
                    timer = interval;
                }
            }
        }
    }

    void SwitchColor()
    {
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");
        if(scrollDelta != 0f){
            currentIndex = (currentIndex + (scrollDelta > 0f ? 1 : -1) + BulletPrefabLs.Count) % BulletPrefabLs.Count;
            currentBulletPrefab = BulletPrefabLs[currentIndex];
            // Debug.Log(currentBulletPrefab);
        }
    }

    void Fire() {
        // if (Mana >= ManaShoot)
        // {
        //     GameObject bullet = Instantiate(currentBulletPrefab, muzzlePos.position, Quaternion.identity);
        //     bullet.GetComponent<Bullet>().SetSpeed(shootDir);
        //     Mana -= ManaShoot;
        // }
        Debug.Log(currentIndex);
        if(currentIndex == 3){     // Shooting purple bullet
            if(PurpleBulletNum > 0){
                GameObject bullet = Instantiate(currentBulletPrefab, muzzlePos.position, Quaternion.identity);
                bullet.GetComponent<Bullet>().SetSpeed(shootDir);
                PurpleBulletNum -= 1;
            }
        }
        else if(currentIndex == 4){     // Shooting cyan bullet
            if(CyanBulletNum > 0){
                GameObject bullet = Instantiate(currentBulletPrefab, muzzlePos.position, Quaternion.identity);
                bullet.GetComponent<Bullet>().SetSpeed(shootDir);
                CyanBulletNum -= 1;
            }
        }
        else if(currentIndex == 5){     // Shooting yellow bullet
            if(YellowBulletNum > 0){
                GameObject bullet = Instantiate(currentBulletPrefab, muzzlePos.position, Quaternion.identity);
                bullet.GetComponent<Bullet>().SetSpeed(shootDir);
                YellowBulletNum -= 1;
            }
        }
        else{
            GameObject bullet = Instantiate(currentBulletPrefab, muzzlePos.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().SetSpeed(shootDir);
        }
    }

    void Moving()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        SetAnim(horizontalInput, verticalInput);

        if (Mathf.Abs(horizontalInput) > 0.1f) {
            ChangeFaceDirection(horizontalInput);
        }

        Vector2 newVector = new Vector2(horizontalInput, verticalInput) * moveSpeed * Time.deltaTime;
        player.transform.Translate(newVector);

    }

    void SetAnim(float horizontalInput, float verticalInput)
    {
        isRunning = (Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f);
        animator.SetBool("IsIdle", !isRunning);
        animator.SetBool("IsRunning", isRunning);
    }

    void ChangeFaceDirection(float horizontalInput)
    {
        if (horizontalInput < 0.1f) {
            player.transform.localScale = new Vector2(0.3f, 0.3f);
        } else {
            player.transform.localScale = new Vector2(-0.3f, 0.3f);
        }
    }

    void CheckAttacking()
    {
        if (Input.GetMouseButtonDown(0) && !Input.GetMouseButton(1))
        {
            StartCoroutine(PlayAttackAnimation());
        }
    }

    IEnumerator PlayAttackAnimation()
    {
        isAttacking = true;
        animator.SetBool("IsAttack", true);

        AnimationClip attackClip = animator.runtimeAnimatorController.animationClips[0];
        float attackDuration = attackClip.length;
        yield return new WaitForSeconds(attackDuration);

        animator.SetBool("IsAttack", false);
        isAttacking = false;
    }

    void AimTarget()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
            mousePosition.z = 0f;

            aimIconInstance = Instantiate(aimIconPrefab, mousePosition, Quaternion.identity);
        }
        if (Input.GetMouseButton(1))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
            mousePosition.z = 0f;
            // Debug.Log("Aim Icon Position: " + aimIconInstance.transform.position);
            aimIconInstance.transform.position = -mousePosition;
        }
        if (Input.GetMouseButtonUp(1) && aimIconInstance != null)
        {
            Destroy(aimIconInstance);
        }
    }
    void SpawnTurret() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            if (Mana >= turrents[0].manaconsume)
            {
                Instantiate(turrents[0], new Vector3(transform.position.x, transform.position.y, 0), Quaternion.Euler(0, 0, 0));
                Mana -= turrents[0].manaconsume;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (Mana >= turrents[1].manaconsume)
            {
                Instantiate(turrents[1], new Vector3(transform.position.x, transform.position.y, 0), Quaternion.Euler(0, 0, 0));
                Mana -= turrents[1].manaconsume;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (Mana >= turrents[2].manaconsume)
            {
                Instantiate(turrents[2], new Vector3(transform.position.x, transform.position.y, 0), Quaternion.Euler(0, 0, 0));
                Mana -= turrents[2].manaconsume;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (Mana >= turrents[3].manaconsume)
            {
                Instantiate(turrents[3], new Vector3(transform.position.x, transform.position.y, 0), Quaternion.Euler(0, 0, 0));
                Mana -= turrents[3].manaconsume;
            }
        }
    }
    void ManaRecover() {
        if (Mana < MaxMana)
        {
            Mana += ManaRecovery * Time.deltaTime;
        }
    }

    public void ExpUp(int i) {
        Exp += i;
        if (Exp >= LevelUpExp) {
            LevelUP();
        }
    }
    void LevelUP() {
        Exp -= LevelUpExp;
        Level += 1;

    }
       
}
