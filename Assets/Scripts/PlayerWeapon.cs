using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    int ammo;
    [Header("���������� ���� � ��������")]
    public int ammo_def = 30;
    [Header("����������������")]
    public float time_attack=0.2f;
    bool reload;
    [Header("����� �����������")]
    public float time_reload = 1;

    [Header("������ ����")]
    public Transform bullet;
    [Header("���� ����")]
    public float bullet_damage=10;
    [Header("����� ��������")]
    public Transform point_gun;

    Animator weapon_animator;
    void OnEnable()
    {
        weapon_animator = gameObject.GetComponent<Animator>();
        ammo = ammo_def;//��������� �������
        reload = false;
        StartCoroutine("Attack");//������������
    }

    IEnumerator Attack()
    {
        while (true)
        {
            if (reload)
                yield return new WaitForSeconds(1);//���
            ammo--;//�������� ����
            Transform bullet_inst =  Instantiate(bullet, point_gun.position, point_gun.rotation);
            bullet_inst.SetParent(point_gun);
            bullet_inst.SetParent(transform.parent);
            bullet_inst.gameObject.GetComponent<Bullet>().damage = bullet_damage;//��������� ���� ����
            
            if (ammo ==0)
            {
                StartCoroutine("Reload");//��������� ����������� ���� ��� ����
            }
            yield return new WaitForSeconds(time_attack);//���
        }
        
    }
    IEnumerator Reload()
    {
        reload = true;
        weapon_animator.SetBool("reload",true);
        yield return new WaitForSeconds(time_reload);//���
        weapon_animator.SetBool("reload", false);
        ammo = ammo_def;//��������� �������
        reload = false;
    }

}
