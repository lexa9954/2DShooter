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
    [Header("�������� �����")]
    public float hp = 100;

    [Header("����� �������� �����")]
    public Slider hp_hud;

    [Header("��������� ��� ����� ������")]
    public float distance_attack = 10;
    [Header("����")]
    public float damage = 20;
    void Start()
    {
        hp_hud.maxValue = hp;
        mob_animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (dead)//���� ������ ������������
            return;

        hp_hud.value = hp;//������� �����
        if (hp <= 0)//�� �� ���� �������
            dead = true;

        if (!attack && !dead)
        {
            //��������
            mob_animator.SetBool("walk", true);
            mob_animator.SetBool("attack", false);
            //���������� �����
            Vector3 pos = new Vector3(move_speed* move * Time.deltaTime, 0, 0);
            transform.position = transform.position + pos;
        }
        else if(attack && !dead)
        {
            //��������
            mob_animator.SetBool("attack", true);
            mob_animator.SetBool("walk", false);
        }
        else if(dead)
        {
            //��������
            mob_animator.SetBool("attack", false);
            mob_animator.SetBool("walk", false);
            mob_animator.SetBool("dead", true);
            //�������
            StartCoroutine(Dead());
        }

        //���������� ��������� �� ������
        float distance_to_player = Vector3.Distance(transform.position,player.transform.position);
        if (distance_to_player<=distance_attack) //������� ���� ������
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
        player.GetComponent<PlayerController>().hp -=damage; //���� ������
    }

    IEnumerator Dead()
    {
        this.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(3);
        //������� ������ ����� � �������� ����� ������ � GameSystem
        gameObject.SetActive(false);
        GS.dead_mob();
    }
}
