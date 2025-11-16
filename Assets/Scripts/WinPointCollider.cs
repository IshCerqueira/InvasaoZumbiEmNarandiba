using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPointCollider : MonoBehaviour
{
    [SerializeField] PlayerScript _playerScript;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            _playerScript.WinSequence();
        }
        }

}
