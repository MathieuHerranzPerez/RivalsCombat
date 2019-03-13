using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CubeMotor : MonoBehaviour
{

    private Vector3 velocity = Vector3.zero;
    private Vector3 jumpForce = Vector3.zero;
    private Rigidbody cubeRigidbody;

    void Start()
    {
        cubeRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        PerformMovement();
    }

    public void Move(Vector3 velocity)
    {
        this.velocity = velocity;
    }

    public void Jump(Vector3 jumpForce)
    {
        this.jumpForce = jumpForce;
    }

    private void PerformMovement()
    {
        // if want to move
        if(velocity != Vector3.zero)
        {
            cubeRigidbody.MovePosition(cubeRigidbody.position + velocity * Time.fixedDeltaTime);
        }
        // if want to jump
        if (jumpForce != Vector3.zero)
        {
            Vector3 currentForce = new Vector3(0f, -cubeRigidbody.velocity.y, 0f);
            currentForce += jumpForce;
            cubeRigidbody.AddForce(currentForce, ForceMode.Impulse);
        }
    }
}
