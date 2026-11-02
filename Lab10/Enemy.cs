using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour
{
    public float speed;
    public int pointValue;
    public int direction;
    public string targetName;

    private void Update()
    {
        //horizontal move
        transform.Translate(Vector3.right * direction * speed * Time.deltaTime);
        if(transform.position.x < -15f || transform.position.x > 15f)
        {
            Destroy(gameObject);
        }
    }

    //destroy if hit
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddScore(pointValue);
            }
            Destroy(gameObject);
        }
    }
}
