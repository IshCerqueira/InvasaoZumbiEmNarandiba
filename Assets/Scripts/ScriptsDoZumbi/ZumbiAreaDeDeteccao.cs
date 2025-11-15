using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZumbiAreaDeDeteccao : MonoBehaviour
{

    [SerializeField] ZumbiScript _zumbiScript;


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            _zumbiScript.SetAgro();
            Destroy(gameObject);
             
        }
    }
}
