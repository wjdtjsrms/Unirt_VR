using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public float attackAmount = 20.0f;
    public AudioClip swordClip;
    AudioSource swordAudio;
    // Start is called before the first frame update
    void Start()
    {
        swordAudio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Monster"))
        {
            BulletSpawner bulletMonster = other.GetComponent<BulletSpawner>();
            bulletMonster?.GetDamage(attackAmount);
            swordAudio.PlayOneShot(swordClip);
        }
        else if (other.CompareTag("Monster2"))
        {
            MonsterCtrl alien = other.GetComponent<MonsterCtrl>();
            alien?.GetDamage(attackAmount);
            swordAudio.PlayOneShot(swordClip);
        }
    }

}
