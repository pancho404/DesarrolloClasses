using Inputs;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    const float maxDistance = 10;
    [Header("Setup")]
    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject feet;

    [Header("Movement")]
    [SerializeField] float minJumpDistance = 0.1f;
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float dash = 10.0f;
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private float speedMultiplier = 2.0f;
    [SerializeField] private float coyoteTime = 4.0f;
    private bool isJumpInput = false;
    private bool isDashInput = false;
    private bool isOnAir = false;
    private Vector3 currentMovement;

    private float timeElapsed;

    private void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>(); ;
        if (feet == null)
            feet = GetComponent<GameObject>();
    }
    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * currentMovement);
    }

    public void OnJump(InputValue input)
    {
        // isJumpInput = true;
        StopCoroutine(JumpCoroutine());
        StartCoroutine(JumpCoroutine());
    }

    private IEnumerator JumpCoroutine()
    {
        if (!feet)
            yield break;

        RaycastHit hit;
        RaycastHit coyoteHit;
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (IsGrounded(out hit) && Physics.Raycast(feet.transform.position-rb.velocity*coyoteTime*Time.deltaTime,Vector3.down, minJumpDistance))
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            yield break;
        }

    }

    private bool IsGrounded(out RaycastHit hit)
    {
        return Physics.Raycast(feet.transform.position, Vector3.down, out hit, maxDistance) && hit.distance <= minJumpDistance;
    }

    public void OnMove(InputValue input)
    {
        var movement = input.Get<Vector2>();
        currentMovement.x = movement.x;
        currentMovement.z = movement.y;
    }

    public void OnSprint(InputValue input)
    {
        if (input.isPressed)
        {
            speed *= speedMultiplier;
        }
        else
        {
            speed /= speedMultiplier;
        }
    }

    public void OnDash(InputValue input)
    {
        isDashInput = true;
    }

    private void FixedUpdate()
    {
        //RaycastHit hit;
        //if (Physics.Raycast(feet.transform.position, Vector3.down, out hit, maxDistance) && hit.distance <= minJumpDistance)
        //    isOnAir = true;

        //if (isJumpInput && isOnAir)
        //{
        //    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        //    isJumpInput = false;
        //    isOnAir = false;
        //}
        if (isDashInput)
        {
            rb.AddForce(currentMovement * dash, ForceMode.Impulse);
            isDashInput = false;
        }
    }

    public IEnumerator CoyoteTime()
    {

        if (!feet)
            yield break;

        RaycastHit feetHit;

        while(true)
        {
            yield return new WaitForFixedUpdate();


        }


        yield break;
    }
}