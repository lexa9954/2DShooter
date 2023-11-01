using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bullet_speed =1000;
    public float damage = 10;
    public int rot;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,3);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3(bullet_speed* Time.deltaTime, 0, 0);
        transform.localPosition = transform.localPosition + pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<EnemySystem>().hp -= damage;
        Destroy(gameObject);
    }
}
