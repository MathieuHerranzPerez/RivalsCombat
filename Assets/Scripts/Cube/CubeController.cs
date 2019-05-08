using System.Collections;
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
    private float timeBetweenJumps = 0.1f;

    [SerializeField]
    private Animator animator = default;

    [SerializeField]
    private Material transparentMaterial = default;

    // ---- INTERN ----

    private CubeMotor motor;
    private bool canJump = true;
    private Material previousMaterial;
    private Renderer cubeRenderer;


    void Awake()
    {
        cube = GetComponent<Cube>();
    }

    void Start()
    {
        motor = GetComponent<CubeMotor>();
        cubeRenderer = cube.GetCubeRenderer();
        previousMaterial = cubeRenderer.material;
    }

    void Update()
    {
        float xMov = cube.input.Horizontal;
        float yMov = cube.input.Vertical;

        Vector3 moveHorizontal = Vector3.right * xMov;  // (1, 0, 0) world space
        // final movement vector
        Vector3 velocity = (moveHorizontal).normalized * speed;

        // apply movement
        motor.Move(velocity);


        // ---- JUMP ----
        Vector3 _jumpForce = Vector3.zero;
        if(cube.input.IsButton(PlayerInput.Button.A) && canJump)
        {
            _jumpForce = Vector3.up * jumpForce;
            canJump = false;
            StartCoroutine(DelayJump());
            animator.SetTrigger("Jumping");
        }

        //if(!canJump)
        //{
        //    time += Time.deltaTime;
        //    if (time >= timeToJump)
        //    {
        //        canJump = true;
        //        time = 0f;
        //    }
        //}

        // apply the jump force
        motor.Jump(_jumpForce);
    }

    public void CancelDash()
    {
        motor.CancelDash();
    }

    public void ApplyForce(Vector3 force)
    {
        motor.Impulse(force);
    }

    public void ImmobilizeForFire()
    {
        motor.CanMove(false);
        animator.SetTrigger("Aiming");
    }

    public void LetFree()
    {
        motor.CanMove(true);
        animator.SetTrigger("StopAiming");
    }

    public void Dash(Vector3 direction, float speed)
    {
        motor.Dash(direction, speed);
        animator.SetTrigger("Dashing");
    }

    public void TP(Vector3 direction, float distance)
    {
        motor.TP(direction, distance);
    }

    public void SetInvisible()
    {
        cubeRenderer.material = transparentMaterial;
    }

    public void SetVisible()
    {
        cubeRenderer.material = previousMaterial;
    }

    private IEnumerator DelayJump()
    {
        float time = 0f;
        while(time < timeBetweenJumps)
        {
            time += Time.deltaTime;
            yield return null;
        }
        canJump = true;
    }
}
