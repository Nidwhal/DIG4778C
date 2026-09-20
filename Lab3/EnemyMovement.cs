using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //create speed and distance variable
    float speed;
    float rangeX;
    float rangeY;

    //create variable for player position
    public Transform player;

    //create center point to orbit around and direction boolean
    Vector2 centerPoint;
    bool clockwise;
    float rotationDirection;
    float rotationAngle;
    float radius;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //set speed variable and set to random speed and repeat for distance from player
        rangeX = Random.Range(1f, 9f);
        rangeY = Random.Range(1f, 3.5f);
        clockwise = Random.value > 0.5f;
        centerPoint = Vector3.zero;

        //set starting position and radius length
        transform.position = new Vector3(rangeX, rangeY, transform.position.z);
        radius = (Vector3.zero - transform.position).magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        FaceThePlayerAndSetSpeed();
        OrbitCenter();
    }

    void FaceThePlayerAndSetSpeed()
    {
        //check if the player exists
        if(player != null)
        {
            //get the direction of the player
            Vector3 direction = player.position - transform.position;

            //set speed based on player distance
            speed = .1f + (0.2f * direction.magnitude);

            //calculate angle of rotation
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            //rotate the enemy accordingly (-90f because the sprite is facing upward not right)
            transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
        }
    }

    void OrbitCenter()
    {
        if(player != null)
        {
            float centerPointX = player.position.x;
            float centerPointY = player.position.y;
            //check if clockwise
            if (clockwise)
            {
                rotationDirection = 1f;
            }
            else
            {
                rotationDirection = -1f;
            }
            //findangle
            rotationAngle += speed * Time.deltaTime * rotationDirection;

            Debug.Log(radius);

            //find the new positions for circular rotation
            float x = centerPoint.x + Mathf.Cos(rotationAngle) * radius;
            float y = centerPoint.y + Mathf.Sin(rotationAngle) * radius;

            //transform object
            transform.position = new Vector3(x, y, transform.position.z);
        }
    }
}
