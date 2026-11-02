using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float xLimit = 8f;

    [Header("Combat Settings")]
    [SerializeField] private float shotCooldown = 1f; // 1 second shot cooldown
    private float lastShot = 0f;

    private PlayerInputActions inputActions;
    private float moveInput;

    private void Awake()
    {
        //initialize input actions
        inputActions = new PlayerInputActions();

        //listen for shoot event
        inputActions.Player.Shoot.performed += x => ShootBullet();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Update()
    {
        //check shot cooldown
        lastShot += Time.deltaTime;

        //determine left or right
        Vector2 moveVector = inputActions.Player.Move.ReadValue<Vector2>();
        moveInput = moveVector.x;

        //horizontal movement
        transform.Translate(Vector3.right * moveInput * moveSpeed * Time.deltaTime);

        //clamp to stay on screen
        Vector3 clampedPos = transform.position;
        clampedPos.x = Mathf.Clamp(clampedPos.x, -xLimit, xLimit);
        transform.position = clampedPos;
    }

    private void ShootBullet()
    {
        //check cooldown on shooting
        if(lastShot < shotCooldown)
        {
            return;
        }
        else
        {
            //take bullet from pool
            if (BulletPool.Instance != null)
            {
                GameObject bullet = BulletPool.Instance.GetBullet();
                bullet.transform.position = transform.position;
                bullet.transform.rotation = Quaternion.identity;
                lastShot = 0f;
            }
        }
    }
}