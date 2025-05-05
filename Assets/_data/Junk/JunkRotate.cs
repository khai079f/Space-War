using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkRotate : JunkAbstract
{
    [SerializeField] protected float speedRotate = 150f;

    protected virtual void Update()
    {
       
        this.Rotating();
    }

    protected virtual void Rotating()
    {
        Vector3 eulers = new Vector3(0, 0, 1);
        this.JunkCtrl.Model.transform.Rotate(eulers *this.speedRotate * Time.deltaTime);
    }
}
