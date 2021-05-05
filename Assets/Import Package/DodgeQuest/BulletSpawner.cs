using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float spwanRateMin = 0.5f;
    public float spwanRateMax = 3f;

    private Transform target;
    private float spwanRate;
    private float timeAffterSpawn;

    public float hp = 100.0f; 
    public AudioClip fireClip;
    AudioSource fireAudio;


    // Start is called before the first frame update
    void Start()
    {
        timeAffterSpawn = 0f;
        spwanRate = Random.Range(spwanRateMin, spwanRateMax);
        target = FindObjectOfType<PlayerController>().transform;

        fireAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timeAffterSpawn += Time.deltaTime;

        if(timeAffterSpawn >= spwanRate)
        {
            timeAffterSpawn = 0f;

            if(!GameManager.Instance.isGameOver)
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                bullet.transform.LookAt(target);
                fireAudio.PlayOneShot(fireClip); // 발사 사운드 재생
            }

            spwanRate = Random.Range(spwanRateMin, spwanRateMax);
        }

    }

    public void GetDamage(float amount)
    {
        hp -= amount;
        if(hp<0) // hp가 0보다 적어지면 사망한다.
        {
            gameObject.SetActive(false);
            GameManager.Instance.GetScored(1);
        }
    }
}
