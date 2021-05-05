using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private float hp = 100.0f;

    public float HP
    {
        get
        {
            return hp;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void GetDamage(float amount)
    {
        hp -= amount;

        if(hp<0)
        {
            Die();
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);

        GameManager.Instance.EndGame();
    }
}
