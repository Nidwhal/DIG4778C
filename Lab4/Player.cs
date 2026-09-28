using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerInputHandler))]
public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private float speed = 6f;
    [SerializeField] private float horizontalScreenLimit = 10f;
    [SerializeField] private float verticalScreenLimit = 6f;
    [SerializeField] private float fireRate = 0.5f;

    //create hp values
    [SerializeField] protected int maxHealth = 1;
    protected int currentHealth;

    private PlayerInputHandler inputHandler;
    private bool canShoot = true;
    

    private void Awake()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        HandleMovement();
        HandleShooting();
    }

    private void HandleMovement()
    {
        // Get movement direction from PlayerInputHandler
        Vector2 moveVector = inputHandler.MoveInput;
        transform.Translate(new Vector3(moveVector.x, moveVector.y, 0) * (speed * Time.deltaTime));

        // Screen wrapping logic
        Vector3 pos = transform.position;
        if (Mathf.Abs(pos.x) > horizontalScreenLimit)
        {
            pos.x *= -1f;
        }
        if (Mathf.Abs(pos.y) > verticalScreenLimit)
        {
            pos.y *= -1f;
        }
        transform.position = pos;
    }

    private void HandleShooting()
    {
        if (inputHandler.IsFiring && canShoot)
        {
            Instantiate(laserPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            inputHandler.IsFiring = false;
            canShoot = false;
            StartCoroutine(CooldownRoutine());
        }
    }

    private IEnumerator CooldownRoutine()
    {
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    // IDamageable implementation for Player death
    public void TakeDamage(int damageTaken)
    {
        currentHealth -= damageTaken;
        if(currentHealth <= 0)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.SetGameOver();
            }
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Contains("Meteor"))
        {
            TakeDamage(1);
        }
    }
}
