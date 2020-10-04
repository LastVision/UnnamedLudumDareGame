using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMovement : MonoBehaviour
{
    //public
	public Camera cam;
    public List<AudioClip> jumpAudioClips = new List<AudioClip>();
    public List<AudioClip> landAudioClips = new List<AudioClip>();
    public List<AudioClip> stepAudioClips = new List<AudioClip>();
	public float speed = 10f;
    public float jumpForce = 1f;
	public float sensitivity;

    //private
    private float sensitivityScale = 10f;
    private float footstepTimer = 0.0f;
    private float footstepCooldown = 0.3f;
    private bool isGrounded = true;
    private AudioSource playerSource = null;
    private int myLastPlayedStepIndex = 0;
    private float distToGround = 0;
    private float landCooldown = 0.4f;
    private float landTimer = 0.0f;


    //Methods
	void Start()
	{
        playerSource = GameObject.FindWithTag("Player").GetComponent<AudioSource>();
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
        distToGround = gameObject.GetComponent<Collider>().bounds.extents.y;
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
        if (isGrounded)
        {
            if (footstepTimer > 0.0f)
            {
                footstepTimer -= Time.deltaTime;
            }
            else
            {
                if (move.magnitude > 0.0f)
                {
                    int randomizedIndex = Random.Range(0, stepAudioClips.Count - 1);
                    if (randomizedIndex == myLastPlayedStepIndex) // Make sure we don't get same audio twice
                    {
                        if (randomizedIndex > 0)
                        {
                            randomizedIndex--;
                        }
                        else
                        {
                            randomizedIndex++;
                        }
                    }
                    playerSource.PlayOneShot(stepAudioClips[randomizedIndex]);
                    myLastPlayedStepIndex = randomizedIndex;
                    footstepTimer = footstepCooldown;
                }
            }
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            landTimer = landCooldown;
            playerSource.PlayOneShot(jumpAudioClips[Random.Range(0, jumpAudioClips.Count)]);
        }
        if (!isGrounded && landTimer <= 0.0f)
        {
            if (Physics.Raycast(gameObject.transform.position, -Vector3.up, distToGround + 0.85f))
            {
                isGrounded = true;
                playerSource.PlayOneShot(landAudioClips[Random.Range(0, landAudioClips.Count)]);
            }
        }
        else if (landTimer > 0.0f)
        {
            landTimer -= Time.deltaTime;
        }
        Debug.Log(Physics.Raycast(gameObject.transform.position, -Vector3.up, distToGround + 0.85f));
    }

}
