using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
public class Movement : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private Vector2 mousePosition;
    private float movementSpeed = 2f;
    private Rigidbody2D rb;
    public Vector2 movementDirection;
    
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private InputAction lookAction;
    
    private float dashDistance = 5f, dashCooldown = 2f;
    private float dashcounter;

    private void Awake(){
        lookAction = new InputAction(type: InputActionType.Value, binding: "<Mouse>/position");
        lookAction.Enable();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = lookAction.ReadValue<Vector2>();
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isMoving = movementDirection.magnitude > 0;
        animator.SetBool("isWalking", isMoving);
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, mainCamera.nearClipPlane));

        Vector3 moveDirection = (worldPosition - transform.position).normalized;
        moveDirection.z = 0;

        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (Input.GetKeyDown(KeyCode.Space) && dashcounter <= 0)
        {
            StartCoroutine(Dash());
            dashcounter = dashCooldown;
        }

    }
    void FixedUpdate(){
        rb.linearVelocity = movementDirection * movementSpeed;
    }
    private IEnumerator Dash()
    {
    float startTime = Time.time; // Get the time the dash starts
    Vector2 startPosition = transform.position;
    Vector2 endPosition = startPosition + movementDirection.normalized * dashDistance;

    while (Time.time < startTime + 0.2f) // Dash lasts 0.2 seconds
    {
        rb.MovePosition(Vector2.Lerp(startPosition, endPosition, (Time.time - startTime) / 0.2f));
        yield return null; // Wait until next frame
    }
    yield return new WaitForSeconds(dashCooldown);
    dashcounter = 0;
    }

}
