using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZumbiScript : MonoBehaviour
{
    private float health;
    private float maxHealth = 3;
    private float moveSpeed = 1f;
    private Transform target;
    private Vector2 moveDirection;
    private bool agroRange = false;
    public bool  fast;
    private Transform zumbiDistance;
    private Animator animator;

    public GameObject deadZombiePrefab;

 
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerScript _playerScript;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        zumbiDistance = GetComponent<Transform>();
        moveSpeed = 0.4f;
        health = maxHealth;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();

        if (fast)
        {
            moveSpeed = 0.8f;
        }
    }

    private void Update()
    {
        if (target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            moveDirection = direction;

            
        }
    }

    private void FixedUpdate()
    {
        if (target && agroRange)
        {
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
            MoveAnimation();
        }
    }

    public void MoveAnimation()
    {
        animator.SetBool("IsWalking", true);
        animator.SetFloat("InputX", moveDirection.x);
        animator.SetFloat("InputY", moveDirection.y);
    }

    public void TakeDamage( float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            _playerScript.IncrementScore();
            Vector2 prefabPosition = new Vector2(zumbiDistance.position.x, zumbiDistance.position.y);
            GameObject deadBody = Instantiate(deadZombiePrefab, prefabPosition, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void SetAgro()
    {
        agroRange = true;
    }




}
