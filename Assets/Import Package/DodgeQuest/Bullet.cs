using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 6f;
    public float attackAmount = 50.0f;
    private Rigidbody bulletRigidbody;


    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
        bulletRigidbody.velocity = transform.forward * speed;

        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerController playercontroller= other.GetComponent<PlayerController>();
            playercontroller?.GetDamage(attackAmount);            
        }
    }

}
