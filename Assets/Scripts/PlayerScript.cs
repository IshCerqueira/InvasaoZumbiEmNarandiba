using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{

    [SerializeField] private float moveSpeed;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;

    public Transform aim;
    private bool isWalking = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        moveSpeed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = moveInput * moveSpeed;
        if (isWalking)
        {
            Vector3 vector3 = Vector3.left * moveInput.y + Vector3.up * moveInput.x;
            aim.rotation = Quaternion.LookRotation(Vector3.forward, vector3);
        }
        
    }

    public void Move(InputAction.CallbackContext context)
    {
        animator.SetBool("IsWalking", true);
        isWalking = true;

        if (context.canceled)
        {
            isWalking = false;
            animator.SetBool("IsWalking", false);
            animator.SetFloat("LastInputX", moveInput.x);
            animator.SetFloat("LastInputY", moveInput.y);
            Vector3 vector3 = Vector3.left * animator.GetFloat("LastInputY") + Vector3.up * animator.GetFloat("LastInputX");
            aim.rotation = Quaternion.LookRotation(Vector3.forward, vector3);
        }
        
        moveInput = context.ReadValue<Vector2>();
        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);
    }
}
