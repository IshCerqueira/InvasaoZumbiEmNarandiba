using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveZumbi : MonoBehaviour
{

    public bool onRange = false;
    private int baseDamage = 4;

    public GameObject explosionParticles;
    public GameObject deadZombiePrefab;
    public GameObject zumbiUser;

    [SerializeField] ZumbiScript _zumbiScript;
    [SerializeField] PlayerScript _playerScript;


    private void Start()
    {
        StartCoroutine(Exploding());
    }

    IEnumerator Exploding()
    {
        explosionParticles.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Vector2 prefabPosition = new Vector2(zumbiUser.transform.position.x, zumbiUser.transform.position.y);
        GameObject deadBody = Instantiate(deadZombiePrefab, prefabPosition, Quaternion.identity);
        Destroy(zumbiUser);


    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            _playerScript = other.GetComponent<PlayerScript>();
            _playerScript.TakeDamage(baseDamage);


        }
        else if(other.gameObject.tag == "Inimigo")
        {
            _zumbiScript = other.GetComponent<ZumbiScript>();
            _zumbiScript.TakeDamage(baseDamage);

        }
  
    }

  

 
}
