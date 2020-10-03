using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMovement : MonoBehaviour
{
	public float minX = -60f;
	public float maxX = 60f;
	public float speed = 15f;
    public float jumpForce = 1f;
	public float sensitivity;
    private float ScaleSensitivity = 50;
	public Camera cam;

	float rotY = 0f;
	float rotX = 0f;
	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

    void Update()
    {
		rotY += Input.GetAxis("Mouse X") * sensitivity * ScaleSensitivity * Time.deltaTime;
		rotX += Input.GetAxis("Mouse Y") * sensitivity * ScaleSensitivity * cam.aspect * Time.deltaTime;

		transform.localEulerAngles = new Vector3(0, rotY, 0);
		cam.transform.localEulerAngles = new Vector3(-rotX, rotY, 0);

        var move = (transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal")).normalized;
        transform.Translate(move * speed * Time.deltaTime);

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}

		if (Cursor.visible && Input.GetMouseButtonDown(1))
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}

        if (Input.GetButton("Jump"))
        {
        }

        bool isGrounded = false;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            var rb = GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

}
