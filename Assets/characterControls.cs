﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class characterControls : MonoBehaviour {

    static Animator anim;
    public float speed = 10.0F;
    public float rotationSpeed = 100.0F;
    private Rigidbody rb;
    private int count;
    public Text countText;
    public Text winText;
	private bool isFalling = false;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
    }

    // Update is called once per frame
    void Update() {

        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
		translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);

        rb.AddRelativeTorque(Vector3.back * rotation);

		if (Input.GetButtonDown("Jump") && isFalling == false)
        	{
                anim.SetTrigger("isJumping");
                rb.velocity = new Vector3(0, 5, 0);
				isFalling = true;
            }

        if (translation != 0)
            {
                anim.SetBool("isRunning", true);
                anim.SetBool("isIdle", false);
            }
        else
            {
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", true);
            }

        }
	
	void OnCollisionStay()
	{
		isFalling = false;
	}


        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("Pick Up"))
            {
                other.gameObject.SetActive(false);
                count = count + 1;
                SetCountText();
            }
        }

        private void SetCountText() {
            countText.text = "Count: " + count.ToString();
            if (count >= 3) {
                winText.text = "You Win!";
            }

        }
  }