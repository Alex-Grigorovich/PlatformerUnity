using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : Entity
{

    [SerializeField] private int lives = 3;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();
            lives--;
            Debug.Log("У червяка " + lives);
        }

        if (lives < 1)
        {
            Die();
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
}
