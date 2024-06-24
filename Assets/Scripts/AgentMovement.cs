using System.Collections;
using UnityEngine;

public class AgentMovement : AIAgent
{
    public float moveSpeed=1;
    public float turnSpeed=10;
    public float turnTimer = 3;

    //private Vector3 velocity;
    private float counter = 0;
    public float BoundX ;
    public float BoundZ ;

    private void Start()
    {
        //velocity = Vector3.forward * moveSpeed * Time.deltaTime * 100;
    }
    private void Update()
    {
        Move();
        if (counter >= turnTimer)
        {
            Turn(Random.Range(45, 90));
            counter = 0;
        }
        else
        {
            counter += Time.deltaTime;
        }
    }

    private void Move()
    {
        Vector3 movementVector = Vector3.forward * moveSpeed * Time.deltaTime;
        transform.Translate(movementVector, Space.Self);
        //rb.velocity = velocity;

        //if position is outside bounds,turn
        //if(transform.position.x>=BoundX || transform.position.x <= -BoundX || transform.position.z >= BoundZ || transform.position.z <= -BoundZ)
        //{
        //    Turn(90);
        //}
    }
    private void Turn(float angle)
    {
        transform.Rotate(Vector3.up, angle);
        //StartCoroutine(RotateSmooth(angle));
    }

    //IEnumerator RotateSmooth(float angle)
    //{
    //    float newAngle = 0;
    //    while (newAngle < angle)
    //    {
    //        newAngle += Time.deltaTime;
    //        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed);
    //        yield return new WaitForEndOfFrame();
    //    }
    //}
    public void OnTriggerEnter(Collider collision)
    {
        //Vector3 normalVectorOfObstacle = collision.GetContact(0).normal.normalized;
        //Vector3 reflectionDirection = Vector3.Reflect(rb.velocity.normalized, normalVectorOfObstacle);
        //velocity = reflectionDirection * rb.velocity.magnitude;
        //transform.localEulerAngles = new Vector3(0, 0, 0)* turnSpeed * Time.deltaTime;
        Turn(Random.Range(80, 100));
    }

}
