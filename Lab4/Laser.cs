using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float maxY = 11f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);

        if (transform.position.y > maxY)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D whatIHit)
    {
        // Checks if whatever we hit is IDamageable
        if (whatIHit.TryGetComponent<IDamageable>(out var target))
        {
            target.TakeDamage(1);
            Destroy(this.gameObject);
        }
    }
}
