using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    public bool isPaused = false;
    public bool isPickAxeTriggered = false;
    void FixedUpdate()
    {
        GetInput();
    }

    private void GetInput()
    {
        if (!isPaused)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                transform.localPosition += transform.forward * speed * Time.deltaTime;

            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                transform.localPosition += -transform.forward * speed * Time.deltaTime;

            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                Vector3 rotation = new Vector3(0, 1, 0);
                transform.Rotate(rotation, -2);
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                Vector3 rotation = new Vector3(0, 1, 0);
                transform.Rotate(rotation, 2);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickaxe"))
        {
            isPickAxeTriggered = true;
        }
    }
}
