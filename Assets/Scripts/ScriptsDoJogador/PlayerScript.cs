using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{

    
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;
    private int health;
    private int score;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI killCount;
    public TextMeshProUGUI timeCount;

    private Transform playerPosition;
    public Transform aim;
    private bool isWalking = false;
    private bool transformationActive = false;

    public GameObject deadPlayerPrefab;
    public GameObject deadEndScreen;
    public GameObject WinScreen;
    public GameObject gameUI;
    private bool timerIsRunning = false;

    [SerializeField] private Image lifeBar;
    [SerializeField] private Image transformationBar;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float timeRemaining = 1200f;
    [SerializeField] private float timeOnGoing = 0f;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private RuntimeAnimatorController[] CharacterModes;


    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GetComponent<Transform>(); 
        health = 10;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        moveSpeed = 1;
        timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        TimerInAction();
        rb.velocity = moveInput * moveSpeed;
        if (isWalking && moveSpeed != 0)
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

            if(moveSpeed != 0)
            {
                Vector3 vector3 = Vector3.left * animator.GetFloat("LastInputY") + Vector3.up * animator.GetFloat("LastInputX");
                aim.rotation = Quaternion.LookRotation(Vector3.forward, vector3);
            }
         
        }
         
        
            moveInput = context.ReadValue<Vector2>();
            animator.SetFloat("InputX", moveInput.x);
            animator.SetFloat("InputY", moveInput.y);
         
 
    }

    public void Transforming(InputAction.CallbackContext context)
    {
        if (transformationBar.fillAmount >= 1 && !transformationActive)
        {
            transformationActive = true;
            animator.runtimeAnimatorController = CharacterModes[1];
            moveSpeed = 2f;

            StartCoroutine(TransformationBarPercentageDown());

        }
    }

        public void TimerInAction()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                timeOnGoing += Time.deltaTime;
                UpdateTimerDisplay();
            }
            else
            {
                timeRemaining = 0; 
                timerIsRunning = false;
                UpdateTimerDisplay();
                deadEndScreen.SetActive(true);
                Destroy(gameObject);
            }
        }
    }

    public void UpdateTimerDisplay()
    {
        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void ToogleMoveSpeed()
    {
        if(moveSpeed == 0)
        {
            if (TransformedPlayer())
            {
                moveSpeed = 2f;
            }
            else moveSpeed = 1f;
        }
        else if(moveSpeed != 0)
        {
            moveSpeed = 0;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;      
        lifeBar.fillAmount = (health / 10f);
        EarnTransformationPoints(0.1f);

        if (health <= 0)
        {
            Debug.Log("Its over");
            Vector2 prefabPosition = new Vector2(playerPosition.position.x, playerPosition.position.y);
            GameObject deadPlayerBody = Instantiate(deadPlayerPrefab, prefabPosition, Quaternion.identity);
            timerIsRunning = false;
            deadEndScreen.SetActive(true);
            Destroy(gameObject);
        }
         
    }

    public void IncrementScore()
    {
        score++;
        EarnTransformationPoints(0.2f);
        countText.text = score.ToString();
    }

    public void WinSequence()
    {
        WinScreen.SetActive(true);
        gameUI.SetActive(false);
        timerIsRunning = false;
        float minutes = Mathf.FloorToInt(timeOnGoing / 60);
        float seconds = Mathf.FloorToInt(timeOnGoing % 60);
        timeCount.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        killCount.text = score.ToString();

        Destroy(gameObject);
    }

    public void EarnTransformationPoints(float amount)
    {
        if (!transformationActive && transformationBar.fillAmount != 1)
        {
            transformationBar.fillAmount += amount;
        }    
    }

    public bool TransformedPlayer()
    {
        if (transformationActive)
        {
            return true;
        }

        else return false;
    }

    IEnumerator TransformationBarPercentageDown()
    {
        while (transformationBar.fillAmount != 0)
        {
            transformationBar.fillAmount = Mathf.Lerp(transformationBar.fillAmount, transformationBar.fillAmount -= 0.1f, Time.deltaTime );
           
            yield return null;


        }

        if (transformationBar.fillAmount <= 0)
        {
            transformationActive = false;
            animator.runtimeAnimatorController = CharacterModes[0];
            moveSpeed = 1f;
        }


    }


}
