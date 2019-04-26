using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CubeMotor : MonoBehaviour
{
    [SerializeField]
    private Animator animator = default;
    [SerializeField]
    private float startDashTime = 0.2f;

    // ---- INTERN ----
    private Vector3 velocityForMovement = Vector3.zero;
    private Vector3 jumpForce = Vector3.zero;
    private Rigidbody cubeRigidbody;
    private Quaternion previousRotation = Quaternion.identity;
    private bool isFlying = true;
    private bool canMove = true;
    private Vector3 preFreezeVelocity = Vector3.zero;
    private Vector3 preFreezeRVelocity = Vector3.zero;
    private Quaternion baseAimRotation;

    private float previousYVelocity;

    // dash
    private float dashTime;
    private Vector3 dashDirection;
    private float dashSpeed;
    private Vector3 previousVelocity;

    void Start()
    {
        cubeRigidbody = GetComponent<Rigidbody>();
        baseAimRotation = cubeRigidbody.rotation;
    }

    void FixedUpdate()
    {
        if(canMove)
            PerformMovement();

        if(isFlying)
        {
            if(previousYVelocity >= 0 && cubeRigidbody.velocity.y <= 0)
            {
                animator.SetTrigger("Falling");
            }
            previousYVelocity = cubeRigidbody.velocity.y;
        }

        if(dashTime > 0)
        {
            dashTime -= Time.deltaTime;
            cubeRigidbody.velocity = dashDirection * dashSpeed;
            if (dashTime <= 0)
            {
                cubeRigidbody.velocity = previousVelocity;
            }
        }

        DisplayRigidBodyVelocity();
    }

    void LateUpdate()
    {
        if (isFlying)
            SlowRigidBodyMovement();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground" && isFlying)
        {
            isFlying = false;
            animator.SetTrigger("Landing");
        }
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isFlying = false;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isFlying = true;
            animator.ResetTrigger("Landing");
        }
    }

    public void Move(Vector3 velocity)
    {
        this.velocityForMovement = velocity;
    }

    public void Jump(Vector3 jumpForce)
    {
        this.jumpForce = jumpForce;
    }

    public void Impulse(Vector3 force)
    {
        cubeRigidbody.AddForce(force, ForceMode.Impulse);
    }

    public void Dash (Vector3 direction, float speed)
    {
        previousVelocity = cubeRigidbody.velocity;
        dashDirection = direction;
        dashSpeed = speed;

        dashTime = startDashTime;
    }

    public void CancelDash()
    {
        dashTime = 0f;
    }

    public void TP(Vector3 direction, float distance)
    {
        // calcul the new positionand check if we can tp there
        Vector3 vector = direction * distance;
        Vector3 newPos = transform.position + vector;

        transform.position = newPos;
    }

    public void CanMove(bool canMove)
    {
        if (this.canMove != canMove)
        {
            if (!canMove)
            {
                // store the current velocity
                preFreezeVelocity = cubeRigidbody.velocity;
           //     preFreezeRVelocity = cubeRigidbody.angularVelocity;
                // turn off the gravity
                cubeRigidbody.useGravity = false;
                // freeze velocity
                cubeRigidbody.velocity = Vector3.zero;
                // freeze rotation velocity
           //     cubeRigidbody.angularVelocity = Vector3.zero;
                // store the rotation
           //     previousRotation = cubeRigidbody.rotation;
                // set the rotation to the base value
           //     cubeRigidbody.rotation = baseAimRotation;
            }
            else
            {
                cubeRigidbody.useGravity = true;
                // restore previous rotation
           //     cubeRigidbody.rotation = previousRotation;
                // restore the previous velocity
                cubeRigidbody.velocity = preFreezeVelocity;
           //     cubeRigidbody.angularVelocity = preFreezeRVelocity;
            }

            this.canMove = canMove;
        }
    }

    private void PerformMovement()
    {
        // if want to move
        if(velocityForMovement != Vector3.zero)
        {
            cubeRigidbody.MovePosition(cubeRigidbody.position + velocityForMovement * Time.fixedDeltaTime);
        }
        // if want to jump
        if (jumpForce != Vector3.zero)
        {
            Vector3 currentForce = new Vector3(0f, -cubeRigidbody.velocity.y, 0f);
            currentForce += jumpForce;
            cubeRigidbody.AddForce(currentForce, ForceMode.Impulse);

            jumpForce = Vector3.zero;
        }
    }

    private void SlowRigidBodyMovement()
    {
        float coef = 0.04f;
        // tanslation
        float velocityX = CalculVelocityReduced(cubeRigidbody.velocity.x, velocityForMovement.x / 30);
        Vector3 newVelocity = new Vector3(velocityX, cubeRigidbody.velocity.y, cubeRigidbody.velocity.z);
        cubeRigidbody.velocity = newVelocity;

        // rotation
        float velocityRX = CalculVelocityReduced(cubeRigidbody.angularVelocity.x, coef);
        float velocityRY = CalculVelocityReduced(cubeRigidbody.angularVelocity.y, coef);
        float velocityRZ = CalculVelocityReduced(cubeRigidbody.angularVelocity.z, coef);
        Vector3 newVelocityR = new Vector3(velocityRX, velocityRY, velocityRZ);
        cubeRigidbody.angularVelocity = newVelocityR;
    }

    private void DisplayRigidBodyVelocity()
    {
        Debug.DrawRay(transform.position, cubeRigidbody.velocity, Color.red);
    }

    private float CalculVelocityReduced(float initialVelocity, float coef)
    {
        float newVelocity = 0f;
        if (initialVelocity > 0 && coef < 0)
        {
            newVelocity = (initialVelocity - coef) >= 0 ? initialVelocity + coef : 0f;
        }
        else if (initialVelocity < 0 && coef > 0)
        {
            newVelocity = (initialVelocity + coef) <= 0 ? initialVelocity + coef : 0f;
        }
        else if(coef < 0.01 || coef > -0.01)        // coef == 0
        {
            if (initialVelocity > 0)
            {
                newVelocity = (initialVelocity - 0.02f) >= 0 ? initialVelocity - 0.02f : 0f;
            }
            else if (initialVelocity < 0)
            {
                newVelocity = (initialVelocity + 0.02f) <= 0 ? initialVelocity + 0.02f : 0f;
            }
        }

        return newVelocity;
    }
}
