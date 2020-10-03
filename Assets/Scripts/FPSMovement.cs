using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMovement : MonoBehaviour
{
    //public
	public Camera cam;
	public float speed = 10f;
    public float jumpForce = 1f;
	public float sensitivity;

    //private
    private float sensitivityScale = 10f;
    private bool isGrounded = true;


    //Methods
	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

    void FixedUpdate()
    {

        float mouseX = Input.GetAxis("Mouse X") * sensitivity * sensitivityScale * Time.deltaTime;
        float mouseY = -Input.GetAxis("Mouse Y") * sensitivity * sensitivityScale * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);
        if (cam.transform.localEulerAngles.x + mouseY < 90f || cam.transform.localEulerAngles.x + mouseY > 270f)
        {
            cam.transform.Rotate(Vector3.right * mouseY);
        }

        var forwardDir = Vector3.ProjectOnPlane(cam.transform.forward, Vector3.up).normalized;
        var move = (forwardDir * Input.GetAxis("Vertical") + cam.transform.right * Input.GetAxis("Horizontal")).normalized;
        transform.Translate(move * speed * Time.deltaTime, Space.World);


        // Cursor code
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
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        isGrounded = true;;
    }
}
