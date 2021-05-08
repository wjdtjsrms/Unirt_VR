using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{

    [SerializeField]
    private float attackAmount = 35.0f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Monster"))
        {
            BulletSpawner bulletSpawner = other.GetComponent<BulletSpawner>();
            bulletSpawner?.GetDamage(attackAmount);
            Destroy(gameObject);
        }
        if (other.CompareTag("Monster2"))
        {
            MonsterCtrl ailen = other.GetComponent<MonsterCtrl>();
            ailen?.GetDamage(attackAmount);
            Destroy(gameObject);
        }
    }
}
