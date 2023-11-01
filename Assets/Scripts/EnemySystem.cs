using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    public float move;
    public float move_speed;
    public Animator mob_animator;

    public bool attack;
    public bool dead;
    public float hp = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
        }


    }
}
