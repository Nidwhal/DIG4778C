using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeteorBase : MonoBehaviour, IDamageable
{
    //create hp values
    [SerializeField] protected int maxHealth = 1;
    protected int currentHealth;

    //create movement and other variables
    protected Transform playerPosition;
    protected float speed;
    protected Vector2 centerPoint;
    protected bool clockwise;
    protected float rotationDirection;
    protected float rotationAngle;
    protected float radius;

    //create event for any of the meteors blowing up
    public static event Action<MeteorBase> OnMeteorDestroyed;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        currentHealth = maxHealth;
        //set random position
        float rangeX = UnityEngine.Random.Range(1f, 9f);
        float rangeY = UnityEngine.Random.Range(1f, 3.5f);
        clockwise = UnityEngine.Random.value > 0.5f;
        centerPoint = Vector2.zero;

        transform.position = new Vector3(rangeX, rangeY, transform.position.z);
        radius = (Vector3.zero - transform.position).magnitude;
        rotationAngle = Mathf.Atan2(transform.position.y, transform.position.x);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        OrbitCenter();
    }
    //method for orbiting
    protected void OrbitCenter()
    {
        //dynamic speed based on player distance
        //get player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerPosition = player.transform;
            float distanceToPlayer = Vector2.Distance(transform.position, playerPosition.position);
            speed = distanceToPlayer * 0.1f;
        }
        else
        {
            speed = .2f;
        }

            rotationDirection = clockwise ? 1f : -1f;
        rotationAngle += speed * Time.deltaTime * rotationDirection;

        float x = centerPoint.x + Mathf.Cos(rotationAngle) * radius;
        float y = centerPoint.y + Mathf.Sin(rotationAngle) * radius;

        transform.position = new Vector3(x, y, transform.position.z);
    }

    //method for taking damage
    public virtual void TakeDamage(int damageTaken)
    {
        currentHealth -= damageTaken;
        if (currentHealth <= 0)
        {
            //send meteor death 
            OnMeteorDestroyed?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
