using UnityEngine;

[RequireComponent(typeof(CubeMotor))]
[RequireComponent(typeof(Cube))]
public class CubeController : MonoBehaviour
{
    private Cube cube;

    [SerializeField]
    private float speed = 3f;
    [SerializeField]
    private float jumpForce = 4f;
    [SerializeField]
    private float timeToJump = 0.15f;

    private CubeMotor motor;
    private bool canJump = true;
    private float time = 0f;

    void Awake()
    {
        cube = GetComponent<Cube>();
    }

    void Start()
    {
        motor = GetComponent<CubeMotor>();
    }

    void Update()
    {
        //float xMov = Input.GetAxisRaw("Horizontal");
        //float yMov = Input.GetAxisRaw("Vertical");
        float xMov = cube.input.Horizontal;
        float yMov = cube.input.Vertical;

        Vector3 moveHorizontal = Vector3.right * xMov;  // (1, 0, 0) world space
        Vector3 moveVertical = Vector3.up * yMov;       // (0, 1, 0) world space


        // final movement vector
        Vector3 velocity = (moveHorizontal).normalized * speed;

        // apply movement
        motor.Move(velocity);


        // ---- JUMP ----
        Vector3 _jumpForce = Vector3.zero;
        // if (Input.GetButton("Jump") && canJump)
        if(cube.input.IsButtonDown(PlayerInput.Button.A) && canJump)
        {
            _jumpForce = Vector3.up * jumpForce;
            canJump = false;
        }

        if(!canJump)
        {
            time += Time.deltaTime;
            if (time >= timeToJump)
                canJump = true;
        }

        // apply the jump force
        motor.Jump(_jumpForce);
    }
}
