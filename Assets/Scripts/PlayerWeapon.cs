using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    int ammo;
    public int ammo_def = 30;
    public float time_attack=0.2f;
    public bool reload;
    public float time_reload = 1;

    public Transform bullet;
    public Transform point_gun;
    public Animator weapon_animator;
    // Start is called before the first frame update
    void Start()
    {
        ammo = ammo_def;
        StartCoroutine("Attack");
    }

    IEnumerator Attack()
    {
        while (true)
        {
            if (reload)
                yield return new WaitForSeconds(1);
            ammo--;
            Transform bullet_inst =  Instantiate(bullet, point_gun.position, point_gun.rotation);
            bullet_inst.SetParent(point_gun);
            if (ammo ==0)
            {
                StartCoroutine("Reload");
            }
            yield return new WaitForSeconds(time_attack);
        }
        
    }
    IEnumerator Reload()
    {
        reload = true;
        weapon_animator.SetBool("reload",true);
        yield return new WaitForSeconds(time_reload);
        weapon_animator.SetBool("reload", false);
        ammo = ammo_def;
        reload = false;
    }

}
