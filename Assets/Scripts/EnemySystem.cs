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
    public Animator mob_animator;

    public bool attack;
    public bool dead;
    public float hp = 100;

    public Slider hp_hud;

    public float distance_attack = 10;
    public float damage = 20;
    void Start()
    {
        hp_hud.maxValue = hp;
    }

    void Update()
    {
        if (dead)
            return;

        hp_hud.value = hp;
        if (hp <= 0)
            dead = true;

        if (!attack && !dead)
        {
            mob_animator.SetBool("walk", true);
            mob_animator.SetBool("attack", false);
            Vector3 pos = new Vector3(move_speed* move * Time.deltaTime, 0, 0);
            transform.position = transform.position + pos;
        }
        else if(attack)
        {
            mob_animator.SetBool("attack", true);
            mob_animator.SetBool("walk", false);
        }
        else if(dead)
        {
            mob_animator.SetBool("attack", false);
            mob_animator.SetBool("walk", false);
            mob_animator.SetBool("dead", true);
            Destroy(gameObject,3);
            GS.dead_mob();
        }

        float distance_to_player = Vector3.Distance(transform.position,player.transform.position);
        if (distance_to_player<=distance_attack)
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
        player.GetComponent<PlayerController>().hp -=damage;
    }
}
