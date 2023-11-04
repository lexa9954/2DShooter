using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [Header("Точка перемещения")]
    public Transform point_move;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Mob")//если это враг
        {
            collision.transform.position = point_move.position;//перемещаем врага к точке
        }
    }
}
