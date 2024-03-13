using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingMonster : Entity
{

    private float speed = 1.5f;
    private Vector3 dir;
    private SpriteRenderer sprite;
    
    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>(); // Присваивает переменной sprite компонент SpriteRenderer, который находится в дочернем объекте
    }

    // Start is called before the first frame update
    void Start()
    {
        dir = transform.right; // Присваивает переменной direction значение вектора, указывающего вправо относительно объекта
    }

    private void Move() // Метод, который отвечает за перемещение врага
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.1f + transform.right * dir.x * 0.7f, 0.1f);
        if (colliders.Length > 0) dir *= -1;

        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, Time.deltaTime);
        sprite.flipX = dir.x < 0.0f;
        
    }
    
    // Update is called once per frame
   private void Update()
    {
        Move();
    }
   
   private void OnCollisionEnter2D(Collision2D collision) // Метод, который вызывается при столкновении с другим коллайдером
   {
       if (collision.gameObject == Hero.Instance.gameObject) // Если объект столкновения является героем
       {
           Hero.Instance.GetDamage(); // Вызывает метод GetDamage у героя
       }
   }
   
}
