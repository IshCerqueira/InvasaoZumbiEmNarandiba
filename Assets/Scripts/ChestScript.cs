using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{

    private int healAmount = 0;
    public bool bigBox = false;
    public bool opened = false;
    [SerializeField] PlayerScript _playerScript;
    public GameObject healParticles;
    public Animator chestAnimator;

    private void Start()
    {
        chestAnimator = GetComponent<Animator>();

        if (bigBox)
        {
            healAmount = 10;
        }
        else healAmount = 4;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !opened)
        {
        _playerScript = other.GetComponent<PlayerScript>();
            _playerScript.ToggleInteract();
            StartCoroutine(WaitForInteraction());
             
        }

        }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !opened)
        {
            _playerScript = other.GetComponent<PlayerScript>();
            _playerScript.ToggleInteract();
        }

    }


    IEnumerator WaitForInteraction()
    {

        while (_playerScript.GetInteract())
        {
            if (_playerScript.GetInteractionTime())
            {
                chestAnimator.SetBool("Open", true);
                _playerScript.ToggleInteractionTime();
                _playerScript.ToggleInteract();
                opened = true;
                _playerScript.PlayerHeal(healAmount);
                healParticles.SetActive(true);
                yield return null;
            }
            else yield return null;
        }

        yield return null;
    }
}

