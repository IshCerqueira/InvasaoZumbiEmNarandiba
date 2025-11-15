using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueDoZumbi : MonoBehaviour
{
    public bool onRange = false;
    private int baseDamage = 2;

    [SerializeField] PlayerScript _playerScript;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            _playerScript = other.GetComponent<PlayerScript>();
            onRange = true;
            StartCoroutine(DamageCount());

        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        onRange = false;
    }

    IEnumerator DamageCount()
    {
        yield return new WaitForSeconds(1);
        if (onRange)
        {
            _playerScript.TakeDamage(baseDamage);
            yield return null;
            StartCoroutine(DamageCount());
        }
     

    }
}
