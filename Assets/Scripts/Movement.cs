using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private AudioSource audioSource;
    private float horizontalInput, verticalInput;
    [Range(0,100)]
    public float moveSpeed;
    public float upforce=10;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");


        transform.Translate(horizontalInput * Time.deltaTime * moveSpeed, 0, 0);
        transform.Translate(0, 0, verticalInput * Time.deltaTime * moveSpeed);

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0,upforce*Time.deltaTime,0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, -upforce * Time.deltaTime, 0);

        }
    }

    public void onMute()
    {
        if(audioSource.enabled == false)
        {
            audioSource.enabled = true;
        }
        else if(audioSource.enabled==true)
        {
            audioSource.enabled = false;
        }
        
    }
}
