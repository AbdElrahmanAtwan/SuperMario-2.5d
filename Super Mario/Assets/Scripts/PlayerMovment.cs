using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    [Header("Jump")]
    public float jumpingForce;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public Transform groundCheck;
    public float maxDistance;

    [Header("Movement")]
    private bool playerMovment=true;
    public float movingSpeed = 5.0f;
    public float shiftSpeed = 5.0f;

    [Header("Shoot")]
    public Transform bulletSpawn;
    public GameObject bulletPrefab;
    public float shootingSpeed;

    private Rigidbody rb;
    private Vector3 movement;
    private RaycastHit hit;
    private float speedTemp;
    private float moveHorizontal;
    private Camera cam;

    public bool Playermovment
    {
        get
        {
            return playerMovment;
        }

        set
        {
            playerMovment = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speedTemp = movingSpeed;
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            FireBullets();
        }
        if(playerMovment)
            PlayerMove();

        if (Input.GetKey(KeyCode.LeftShift))
            movingSpeed = shiftSpeed;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            movingSpeed = speedTemp;

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.X) && IsGrounded(out hit))
            rb.velocity += Vector3.up * jumpingForce;

        JumpGravity();
    }

    private void FireBullets()
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawn.right * shootingSpeed;
        Destroy(bullet, 2.0f);
    }

    private void PlayerMove()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity += (Vector3.right * movingSpeed);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity += (-Vector3.right * movingSpeed);
            transform.eulerAngles = new Vector3(0, 180, 0);

            Vector3 screenPos = cam.WorldToScreenPoint(transform.position);
            if (screenPos.x < 1)
            {
                GetComponent<Rigidbody>().velocity = -Vector3.right * 0f;
            }
        }
    }

    private void JumpGravity()
    {
        if (rb.velocity.y < 0)
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;

        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.X))
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
    }

    bool IsGrounded(out RaycastHit hit)
    {
        Debug.DrawRay(groundCheck.transform.position, -transform.up * maxDistance, Color.blue, 0.5f);
        return Physics.Raycast(groundCheck.transform.position, -transform.up, out hit, maxDistance);
    }
}
