using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AtaqueScript : MonoBehaviour
{
    public GameObject melee;
    private bool isAttacking = false;
    private float atkDuration = 0.3f;
    private float atkTimer = 0f;
    private Animator animator;

    [SerializeField] PlayerScript _playerScript;

    // Start is called before the first frame update
    void Start()
    {
        isAttacking = false;
        atkDuration = 1f;
        atkTimer = 0f;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckMeleeTimer();
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (!isAttacking)
        {
            animator.SetBool("IsAttacking", true);
            _playerScript.ToggleMoveSpeed();
            melee.SetActive(true);
            isAttacking = true;
                
        }
        
    }

    void CheckMeleeTimer()
    {
        if(isAttacking)
        {
            atkTimer += Time.deltaTime;
            if(atkTimer >= atkDuration)
            {
                atkTimer = 0;
                isAttacking = false;
                animator.SetBool("IsAttacking", false);
                _playerScript.ToggleMoveSpeed();
                melee.SetActive(false);

            }
        }
    }



    }
