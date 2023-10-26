
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

    public GameObject BulletPrefab;

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

    void Fire() {
        if (Mana >= ManaShoot)
        {
            GameObject bullet = Instantiate(BulletPrefab, muzzlePos.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().SetSpeed(shootDir);
            Mana -= ManaShoot;
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
            Debug.Log("Aim Icon Position: " + aimIconInstance.transform.position);
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
