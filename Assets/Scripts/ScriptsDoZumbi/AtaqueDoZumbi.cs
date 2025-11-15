using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueDoZumbi : MonoBehaviour
{
    public bool onRange = false;
    public bool explosive = false;
    private int baseDamage = 2;

    public GameObject explosionRadius;

    [SerializeField] PlayerScript _playerScript;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !explosive)
        {
            _playerScript = other.GetComponent<PlayerScript>();
            onRange = true;
            StartCoroutine(DamageCount());

        }
        else if(other.gameObject.tag == "Player" && explosive)
        {
   
            onRange = true;
            StartCoroutine(ExplosionTime());
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            onRange = false;
        }

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

    IEnumerator ExplosionTime()
    {
        yield return new WaitForSeconds(1.5f);
        if (onRange)
        {
            explosionRadius.SetActive(true);
        }
    }
}
