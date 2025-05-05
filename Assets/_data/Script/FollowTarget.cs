using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : GameMonoBehaviour
{
    [SerializeField] protected Transform target;
    [SerializeField] protected float speed = 20f;

    protected virtual void Update()
    {
        this.Following();
    }
    protected virtual void Following()
    {
        if (target == null) return;

        // Tính toán vị trí mới với Vector3.Lerp
        Vector2 newPosition = Vector2.Lerp(transform.position, this.target.position, speed * Time.deltaTime);

        // Đảm bảo rằng giá trị z luôn bằng 0


        // Cập nhật vị trí của đối tượng
        transform.position = newPosition;
    }

    public virtual void SetTarget(Transform target)
    {
        this.target = target;
    }
}
