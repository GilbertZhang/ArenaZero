using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {

    public GameObject actionbuttonobject;
    private JumpActionButton actionbutton;
    private bool previous_shootpressdown = false;// last time whether the button is pressed
    public Rigidbody playerRigidbody;
    
    public float intensity = 2; //control how much force to be used; public for testing only 
    //using collisions to check 
    public bool m_isGrounded;
    private List<Collider> m_collisions = new List<Collider>();
    // Use this for initialization
    void Start () {
        actionbutton = actionbuttonobject.GetComponent<JumpActionButton>(); //initialise the button 
        playerRigidbody = GetComponent<Rigidbody>(); //get the player's rigidbody to apply on 

    }

     void FixedUpdate()
    {
        Debug.Log("down" + actionbutton.shootbuttondown);
        Debug.Log("previous: " + previous_shootpressdown  + "\n");

        if (actionbutton.shootbuttondown && m_isGrounded &&!previous_shootpressdown) //testing for space bar 
       
            {
            playerRigidbody.AddForce(new Vector3(0, intensity, 0), ForceMode.Impulse); //to add force onto the rigidbody 
        }
        else
        {
            //don't do anything 
        }
        previous_shootpressdown = actionbutton.shootbuttondown;
        
        
    }

    // Update is called once per frame
    void Update () {
        
	}

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                if (!m_collisions.Contains(collision.collider))
                {
                    m_collisions.Add(collision.collider);
                }
                m_isGrounded = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        bool validSurfaceNormal = false;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                validSurfaceNormal = true; break;
            }
        }

        if (validSurfaceNormal)
        {
            m_isGrounded = true;
            if (!m_collisions.Contains(collision.collider))
            {
                m_collisions.Add(collision.collider);
            }
        }
        else
        {
            if (m_collisions.Contains(collision.collider))
            {
                m_collisions.Remove(collision.collider);
            }
            if (m_collisions.Count == 0) { m_isGrounded = false; }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (m_collisions.Contains(collision.collider))
        {
            m_collisions.Remove(collision.collider);
        }
        if (m_collisions.Count == 0) { m_isGrounded = false; }
    }
}
