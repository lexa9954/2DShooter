using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public bool melee_weapon;
    int ammo;
    [Header("Количество пуль в магазине")]
    public int ammo_def = 30;
    [Header("Скорострельность")]
    public float time_attack=0.2f;
    bool reload;
    [Header("Время перезарядки")]
    public float time_reload = 1;

    [Header("Обьект пули")]
    public Transform bullet;
    [Header("Урон пули")]
    public float bullet_damage=10;
    [Header("Скорость пули")]
    public float bullet_speed = 1000;
    [Header("Точка выстрела")]
    public Transform point_gun;

    Animator weapon_animator;
    void OnEnable()
    {
        weapon_animator = gameObject.GetComponent<Animator>();
        ammo = ammo_def;//заполняем магазин
        reload = false;
        StartCoroutine("Attack");//Автострельба
    }

    IEnumerator Attack()
    {
        while (true)
        {
            if (melee_weapon)
            {
                Bullet_Spawn();
                yield return new WaitForSeconds(time_attack);//ждём
            }
            else
            {
                if (reload)
                    yield return new WaitForSeconds(1);//ждём
                ammo--;//отнимаем пули

                Bullet_Spawn();

                if (ammo == 0)
                {
                    StartCoroutine("Reload");//запускаем перезарядку если нет пуль
                }
                yield return new WaitForSeconds(time_attack);//ждём
            }
        }
    }
    public void Bullet_Spawn()
    {
        Transform bullet_inst = Instantiate(bullet, point_gun.position, point_gun.rotation);
        bullet_inst.SetParent(point_gun);
        bullet_inst.SetParent(transform.parent);
        bullet_inst.gameObject.GetComponent<Bullet>().damage = bullet_damage;//применяем пуле урон
        bullet_inst.gameObject.GetComponent<Bullet>().bullet_speed = bullet_speed;//применяем пуле скорость
    }
    IEnumerator Reload()
    {
        reload = true;
        weapon_animator.SetBool("reload",true);//анимация
        yield return new WaitForSeconds(time_reload);//ждём
        weapon_animator.SetBool("reload", false);//анимация
        ammo = ammo_def;//заполняем магазин
        reload = false;
    }

}
