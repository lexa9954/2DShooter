using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    float move;
    [Header("Скорость передвижения")]
    public float move_speed;
    Animator player_animator;

    [Header("Количество здоровья")]
    public float hp;
    [Header("Количество выносливости")]
    public float strange;

    [Header("Шкала здоровья")]
    public Slider hp_slider;
    [Header("Шкала выносливости")]
    public Slider strange_slider;

    [System.Serializable]
    public class Abil
    {
        public string name;
        public GameObject gun;
        public float need_strange;
        public float time_use;
    }
    
    public List<Abil> abil_type;

    public bool use_abil;

    [Header("Стандартное оружие")]
    public GameObject ability_default;
    void Start()
    {
        player_animator = gameObject.GetComponent<Animator>();
        strange_slider.maxValue = strange;
        hp_slider.value = hp;
    }

    void Update()
    {
        strange_slider.value = strange;
        hp_slider.value = hp;

        if (Input.GetKey(KeyCode.D))
        {
            move = 1;
            transform.rotation = new Quaternion(0,0,0,1);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            move = -1;
            transform.rotation = new Quaternion(0, 180, 0, 1);
        }
        else
        {
            move = 0;
        }
        

        if(move ==0)
            AnimWalk(false);
        else
            AnimWalk(true);

        Vector3 pos = new Vector3(move_speed * move * Time.deltaTime, 0,0);
        transform.position = transform.position+ pos;
        
    }

    public void AnimWalk(bool walk)
    {
        player_animator.SetBool("walk",walk);
    }

    public void Ability(string abil)
    {
        if (use_abil)
            return;
        for (int abil_=0; abil_ < abil_type.Count; abil_++)
        {
            if (abil == abil_type[abil_].name && strange >= abil_type[abil_].need_strange)
            {
                StartCoroutine(AbilCoroutine(abil_type[abil_].gun, abil_type[abil_].time_use));
                strange -= abil_type[abil_].need_strange;
            }
        }
    }
    IEnumerator AbilCoroutine(GameObject go,float time)
    {
        use_abil = true;
        ability_default.SetActive(false);
        go.SetActive(true);
        yield return new WaitForSeconds(time);
        ability_default.SetActive(true);
        go.SetActive(false);
        use_abil = false;
    }
}
