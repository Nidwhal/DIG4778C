using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //create speed variable
    float speed = 4f;

    //create boundary variables to be set on start
    float boundMin;
    float boundMax;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //set boundary variables based on camera size with the last part for the size of the player
        boundMin = -1f * Camera.main.orthographicSize * Camera.main.aspect + 0.5f;
        boundMax = Camera.main.orthographicSize * Camera.main.aspect - 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        //get player movement input
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        //move the player
        transform.Translate(new Vector3(horizontalInput, 0, 0) * speed * Time.deltaTime);

        //set boundaries
        Vector3 boundary = transform.position;
        boundary.x = Mathf.Clamp(boundary.x, boundMin, boundMax);

        transform.position = boundary;
    }
}
