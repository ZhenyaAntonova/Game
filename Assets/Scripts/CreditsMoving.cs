using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsMoving : MonoBehaviour
{
    [SerializeField] private float speed = 40.0f;
    [SerializeField] private float higherBound = 120;
    bool startedMoving = false;

    void Start()
    {
        StartCoroutine(MoveCredits());
    }

    IEnumerator MoveCredits()
    {
        yield return new WaitForSeconds(1);
        startedMoving = true;
    }

    void Update()
    {
        if (startedMoving)
        {
            transform.localPosition += transform.up * speed * Time.deltaTime;

            if (transform.localPosition.y >= higherBound)
            {
                gameObject.SetActive(false);

                if (gameObject.CompareTag("LastWords"))
                {
                    SceneManager.LoadScene("Menu");
                }
            }
        }
    }
}
