using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Скорость пули")]
    public float bullet_speed =1000;
    public float damage = 10;
    void Start()
    {
        Destroy(gameObject,3);//удаляем пулю через время
    }

    void Update()
    {
        #region перемещение
        Vector3 pos = new Vector3(bullet_speed* Time.deltaTime, 0, 0);
        transform.localPosition = transform.localPosition + pos;
        #endregion
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject,0.1f);//удаляем пулю через время
        if(collision.tag == "Mob")//если это враг
            collision.GetComponent<EnemySystem>().hp -= damage;//наносим урон
    }
}
