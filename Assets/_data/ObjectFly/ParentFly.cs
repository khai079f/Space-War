using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentFly : GameMonoBehaviour
{
    [SerializeField] protected float speed = 1f;
    [SerializeField] protected Vector3 direction = Vector3.down;
    protected virtual void Update()
    {
        transform.parent.Translate(this.direction * this.speed * Time.deltaTime);
    }
}
