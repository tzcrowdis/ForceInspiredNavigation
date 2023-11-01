using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceNav : MonoBehaviour
{
    public Transform dest;
    public Collider[] obstacles;
    public float speed;

    Vector3 direction;
    float destF = 100f; //force magnitude towards destination
    float obstF; //force magnitude away from obstacles
    float destAngle;
    float obstAngle;
    float dist;

    float updateTime = 0.1f;
    float currentTime = 0f;

    //basic math behind the idea
    //F = F1 * <cos(theta1), sin(theta1)> + ... + FN * <cos(thetaN), sin(thetaN)>
    //cos of x-axis and sin of z-axis bc unity

    void updateDirection()
    {
        //find angle between agent and destination
        destAngle = Mathf.Atan2(dest.position.z - transform.position.z, dest.position.x - transform.position.x);
        direction = destF * new Vector3(Mathf.Cos(destAngle), 0, Mathf.Sin(destAngle));

        foreach (Collider obst in obstacles)
        {
            Vector3 closestPointObst = obst.ClosestPointOnBounds(transform.position);
            Vector3 closestPoint = GetComponent<Collider>().ClosestPointOnBounds(closestPointObst);
            dist = Vector3.Distance(closestPointObst, closestPoint); //get distance from surface to surface

            obstAngle = Mathf.Atan2(closestPointObst.z - transform.position.z, closestPointObst.x - transform.position.x); //find angle between each obstacle and the agent

            if (-180f <= obstAngle * Mathf.Rad2Deg & obstAngle * Mathf.Rad2Deg < 0f) { continue;  } //skip if obstancle isn't in front of agent

            obstF = -destF / Mathf.Pow(dist, 2); //scale obstacle forces inversely proportional to distance
            //obstF = -destF / dist; //linear inversely proportional

            direction += obstF * new Vector3(Mathf.Cos(obstAngle), 0, Mathf.Sin(obstAngle)); //update direction
        }

        //normalize direction
        direction = direction.normalized;
    }

    void Start()
    {
        //set agent towards only the destination
        direction = (dest.position - transform.position).normalized;
    }

    void Update()
    {
        //move agent
        transform.position += speed * Time.deltaTime * direction;

        //update direction at chosen increment
        currentTime += Time.deltaTime;
        if (currentTime >= updateTime)
        {
            updateDirection();
            currentTime = 0f;
        }
    }
}
