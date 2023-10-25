using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public bool Run;
    public bool Attack;
    private Rigidbody2D rigidbody;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Attacking();
    }
    private void FixedUpdate()
    {
        run();

    }
    public void run()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 vel = new Vector2(horizontal * speed * Time.deltaTime, vertical*speed*Time.deltaTime);
        if (vel.magnitude > Mathf.Epsilon)
        {
            Run = true;
            animator.SetBool("Run", Run);
        }
        else {
            Run = false;
            animator.SetBool("Run", Run);
        }
        if (horizontal > 0.1f)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (horizontal<-0.1f){
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        rigidbody.velocity = vel;
    }
    public void Attacking() {
        Attack = Input.GetKeyDown(KeyCode.Mouse0);
        animator.SetBool("Attack", Attack);

    }
}
