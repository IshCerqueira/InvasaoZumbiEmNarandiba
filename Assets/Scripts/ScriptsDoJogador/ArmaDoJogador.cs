using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaDoJogador : MonoBehaviour
{

    public float damage = 1;

   

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Inimigo")
        {
           ZumbiScript zumbiScript = other.GetComponent<ZumbiScript>();
            if(zumbiScript != null)
            {
                zumbiScript.TakeDamage(damage);
            }

        }
    }


}
