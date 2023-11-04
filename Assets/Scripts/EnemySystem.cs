using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySystem : MonoBehaviour
{
    public GameSystem GS; 
    public GameObject player;

    public float move;
    public float move_speed;
    Animator mob_animator;

    public bool attack;
    public bool dead;
    [Header("Здоровье врага")]
    public float hp = 100;

    [Header("Шкала здоровья врага")]
    public Slider hp_hud;

    [Header("Дистанция для отаки игрока")]
    public float distance_attack = 10;
    [Header("Урон")]
    public float damage = 20;
    void Start()
    {
        hp_hud.maxValue = hp;
        mob_animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (dead)//если мертвы возвращаемся
            return;

        hp_hud.value = hp;//двигаем шкалу
        if (hp <= 0)//хп на нуле умираем
            dead = true;

        if (!attack && !dead)
        {
            //анимации
            mob_animator.SetBool("walk", true);
            mob_animator.SetBool("attack", false);
            //Перемещает врага
            Vector3 pos = new Vector3(move_speed* move * Time.deltaTime, 0, 0);
            transform.position = transform.position + pos;
        }
        else if(attack && !dead)
        {
            //анимации
            mob_animator.SetBool("attack", true);
            mob_animator.SetBool("walk", false);
        }
        else if(dead)
        {
            //анимации
            mob_animator.SetBool("attack", false);
            mob_animator.SetBool("walk", false);
            mob_animator.SetBool("dead", true);
            //умираем
            StartCoroutine(Dead());
        }

        //определяем дистанцию до игрока
        float distance_to_player = Vector3.Distance(transform.position,player.transform.position);
        if (distance_to_player<=distance_attack) //атакуем если близко
        {
            attack = true;
        }
        else
        {
            attack = false;
        }
    }
    public void Attack()
    {
        player.GetComponent<PlayerController>().hp -=damage; //бьем игрока
    }

    IEnumerator Dead()
    {
        this.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(3);
        //удаляем обьект врага и вызываем метод смерти в GameSystem
        gameObject.SetActive(false);
        GS.dead_mob();
    }
}
