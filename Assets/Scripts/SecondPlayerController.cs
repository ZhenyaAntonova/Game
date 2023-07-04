using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UIElements;

public class SecondPlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private GameObject pickaxe;
    [SerializeField] private Canvas messageCanvas;
    [SerializeField] private Canvas historyCanvas;
    [SerializeField] private Canvas tenYearsCanvas;
    [SerializeField] private Canvas noTenYearsCanvas;
    [SerializeField] private Canvas controlsCanvas;
    [SerializeField] private TextMeshProUGUI textHistory;
    [SerializeField] private TextMeshProUGUI textMessage;
    [SerializeField] private GameObject pyramid;
    [SerializeField] private GameObject presentBlocks;
    [SerializeField] private GameObject pastBlocks;
  //  [SerializeField] private Camera playerCamera;
    [SerializeField] private Button nextBtn;
    public bool isPaused = false;
    public bool isFinished = false;

    void Start()
    {

            textHistory.text = "я долго искал эту гробницу и вот € перед ней. Ќужно осмотретьс€.";
            ShowHistory();

    }

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
        if (other.CompareTag("Pickaxe") && !isPaused)
        {
            pickaxe.SetActive(false);
            textMessage.text = "√де гробница? √де мое золото?! я точно в насто€щем?!";
          //  playerCamera.GetComponent<Effects>().enabled = false;
            StartCoroutine(ShowAndPutAwayMessage());
            StartCoroutine(WaitForPlayerToExplore());
            StartCoroutine(tenY());
        }
        else if (other.CompareTag("Pyramid") && !isPaused)
        {
            SceneManager.LoadScene("MainScene");
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cactus"))
        {
            textMessage.text = "ќй";
            ShowAndPutAwayMessage();
        }
    }

    IEnumerator ShowAndPutAwayMessage(float waitingTime = 3)
    {
        messageCanvas.gameObject.SetActive(true);
        yield return new WaitForSeconds(waitingTime);
        messageCanvas.gameObject.SetActive(false);
    }

    IEnumerator WaitForPlayerToExplore()
    {
        textMessage.text = "ѕойду € домой, здесь нечего ловить.";
        yield return new WaitForSeconds(6);
        StartCoroutine(ShowAndPutAwayMessage());
    }

    IEnumerator tenY()
    {
        tenYearsCanvas.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        tenYearsCanvas.gameObject.SetActive(false);
        noTenYearsCanvas.gameObject.SetActive(true);
        textHistory.text = "успешный самосто€тельный дружит с братом";
        historyCanvas.gameObject.SetActive(true);
        isPaused = true;
    }

    public void ToCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void ShowHistory()
    {
        historyCanvas.gameObject.SetActive(true);
        isPaused = true;
        
    }

    public void ReturnToGame()
    {
        Debug.Log("btn pressed");
        historyCanvas.gameObject.SetActive(false);
        isPaused = false;
        controlsCanvas.gameObject.SetActive(true);
        isPaused = true;
    }
    public void CloseControls()
    {
        controlsCanvas.gameObject.SetActive(false);
        isPaused = false;
    }


}
