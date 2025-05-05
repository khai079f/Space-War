using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemyDameReceiver : ShootableObjDameReceiver
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
    }
}
