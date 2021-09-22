using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagerRPC : MonoBehaviour
{
    public CharacterController characterController;
    public float walkSpeed = 6f;

    private void Update()
    {
        Vector3 moveDirectionForward = transform.forward * Input.GetAxis("Vertical");
        Vector3 moveDirectionSide = transform.right * Input.GetAxis("Horizontal");

        Vector3 direction = (moveDirectionForward + moveDirectionSide).normalized;
        Vector3 distance = direction * walkSpeed * Time.deltaTime;

        characterController.Move(distance);
    }
}
