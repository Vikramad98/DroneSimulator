using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMovement : MonoBehaviour
{
    private const float gravity = 9.81f;
    private Rigidbody rb;
    public float ForceUpside;

    public GameObject MenuPanel;
    private AudioSource audioSource;
   
    [SerializeField]
    private float Speed = 500f;
    private float forwardtilt=0;
    private float HorizontalTilt = 0;
    private float horizontalTiltVelocity;
    private float forwardtiltVelocity;
    private float verticalInput,HorizontalInput;

    public GameObject GamePanel;
    [SerializeField]
    private bool isSimulationStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        MenuPanel.SetActive(true);
        GamePanel.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isSimulationStarted)
        {
            updownMovement();
            forwardMovement();
            HorizontalMovement();
        }
        rb.AddRelativeForce(Vector3.up * ForceUpside);
        rb.rotation = Quaternion.Euler(new Vector3(forwardtilt, rb.rotation.y, rb.rotation.z));
        
        if (Input.GetAxis("Horizontal")!=0)
        {
            rb.rotation = Quaternion.Euler(new Vector3(rb.rotation.x, rb.rotation.y, HorizontalTilt));

        }
    }

    void updownMovement()
    {
        
        if (Input.GetKey(KeyCode.Q))
        {
            ForceUpside = 450f;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            ForceUpside = -200f;
        }
        else if (!Input.GetKey(KeyCode.Q)&&!Input.GetKey(KeyCode.E))
        {
            ForceUpside = gravity * rb.mass;
        }
    }
    void forwardMovement()
    {
        verticalInput = Input.GetAxis("Vertical");
        if (verticalInput != 0)
        {
            rb.AddRelativeForce(Vector3.forward * verticalInput * Speed*Time.deltaTime);
            forwardtilt = Mathf.SmoothDamp(forwardtilt, 20 * verticalInput, ref forwardtiltVelocity, 0.1f);
        }
    }

    void HorizontalMovement()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        if (HorizontalInput != 0)
        {
            rb.AddRelativeForce(Vector3.right * HorizontalInput * Speed*Time.deltaTime);
            HorizontalTilt = Mathf.SmoothDamp(HorizontalTilt, -15 * HorizontalInput, ref horizontalTiltVelocity, 0.1f);
        }
    }
    public void onMute()
    {
        if (audioSource.enabled == false)
        {
            audioSource.enabled = true;
        }
        else if (audioSource.enabled == true)
        {
            audioSource.enabled = false;
        }

    }

    public void onPlay()
    {
        MenuPanel.SetActive(false);
        GamePanel.SetActive(true);
        isSimulationStarted = true;
    }

    public void goToMenu()
    {
        MenuPanel.SetActive(true);
        GamePanel.SetActive(false);
        isSimulationStarted = false;
    }
}
