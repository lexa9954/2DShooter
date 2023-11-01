using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float move;
    public float move_speed;
    public Animator player_animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
}
