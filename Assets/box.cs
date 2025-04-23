using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class box : MonoBehaviour
{
    private float speed=4f;
    public Transform target;
    public Transform pointA;
    public Transform pointB;
    void Start()
    {
        target = pointB;
    }

    void Update()
    {
        //MoFor();
        Le();
    }
    void MoFor()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, target.position) < 0.01f)
        {
            target = (target == pointA) ? pointB : pointA;
        }
    }
    void Le()
    {
        float t = Mathf.PingPong(Time.time * speed, 1);
        transform.position = Vector3.Lerp(pointA.position, pointB.position, t);
    }

}
