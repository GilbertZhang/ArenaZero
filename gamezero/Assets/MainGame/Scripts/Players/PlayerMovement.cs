using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;
using System.Collections;
using System.Collections.Generic;

namespace CompleteProject
{
    public class PlayerMovement : MonoBehaviour
    {
        public GameObject joystickGO;
        public float speed;
        public float rotatespeed;
        public float knockbackStrength;
        public float knockbackStunTime;
        private JoystickController jscontrol;
        private Vector3 direction;
        private int lastid = -1;
        private bool isKnockedBack;
        private float time;


        // Use this for initialization
        void Start()
        {
            jscontrol = joystickGO.GetComponent<JoystickController>();
        }

        // Update is called once per frame
        void Update()
        {
            if (this.isKnockedBack)
            {
                if (time < this.knockbackStunTime)
                {
                    time += Time.deltaTime;
                }
                else
                {
                    this.isKnockedBack = false;
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                }
            }


            Debug.DrawRay(transform.position, transform.forward * 10, Color.red);


            Vector3 trans = jscontrol.getDirection();
            if (trans.magnitude != 0 && !this.isKnockedBack)
            {
                float dotprod = Vector3.Dot(trans.normalized, transform.forward.normalized);

                if (dotprod < 0.9)
                { //turn 
                    float step = rotatespeed * Time.deltaTime;
                    Vector3 newDir = Vector3.RotateTowards(transform.forward, trans, step, 0);
                    transform.rotation = Quaternion.LookRotation(newDir);
                }
                else
                { //turn instantenous

                    trans.Normalize();
                    Vector3 move = trans * (speed * Time.deltaTime);
                    transform.Translate(move, null);
                    transform.forward = trans;
                }
            }
        }
    }
}