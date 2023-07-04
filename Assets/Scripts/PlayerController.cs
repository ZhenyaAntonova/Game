using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Canvas messageCanvas;
    [SerializeField] private Canvas historyCanvas;
    //[SerializeField] private Canvas controlsCanvas;
    [SerializeField] private TextMeshProUGUI textHistory;
    [SerializeField] private TextMeshProUGUI textMessage;
   // [SerializeField] private Camera camera;
   // private Effects sepia;
    //private bool controlesShowed = false;
    //private bool chosenBadEnding = false;
    private bool chosenGoodEnding = false;
    //[SerializeField] AudioSource audioRStep;
    //[SerializeField] AudioSource audioLStep;
    //private bool isMoving = false;
    //private bool turn = false;
    bool keyTaken = false;

    // 1 Room
    [SerializeField] private GameObject pregrada1;
    [SerializeField] private GameObject key;
    [SerializeField] private GameObject timeMachine1;
    [SerializeField] private bool touchedFirstFloor = false;
    private bool touchedPic = false;
    private bool touchedPregrada1 = false;

    //2 Room
    [SerializeField] private GameObject timeMachine2;
    [SerializeField] private GameObject openedWay;
    [SerializeField] private GameObject closedWay;
    [SerializeField] private GameObject cross;
    [SerializeField] private GameObject picture;
    [SerializeField] private Canvas pincodeCanvas;
    [SerializeField] private Canvas pincodeCanvas2;
    [SerializeField] private GameObject pregrada2;
    [SerializeField] private Canvas testCanva;
    private bool touchedSecondFloor = false;
    private bool testOpened = false;

    //3 Room
    private bool enteredThirdRoom = false;
    [SerializeField] private GameObject timeMachine3;
    [SerializeField] private Canvas tenCanvas;
    [SerializeField] private Canvas noTenCanvas;
    private bool noTen = false;
    [SerializeField] private Canvas badPic;
    void Start()
    {
       // sepia = camera.GetComponent<Effects>();
        textMessage.text = "Дверь закрылась. Стоит осмотреться.";
        ShowAndPutAwayMessage(messageCanvas);
    }


    void FixedUpdate()
    {
        GetInput();
    }

    private void GetInput()
    {
        if (!gameManager.isPaused)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                transform.localPosition += transform.right * speed * Time.deltaTime;
                //isMoving = true;
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                transform.localPosition += -transform.right * speed * Time.deltaTime;
                //isMoving = true;
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

           /* if (isMoving && !audioRStep.isPlaying && !audioLStep.isPlaying)
            {
                if (!turn)
                {
                    audioRStep.Play(0);
                    isMoving = false;
                    turn = true;
                }
                else if (turn)
                {
                    audioLStep.Play(0);
                    isMoving = false;
                    turn = false;
                }
            }*/
            
        }

    }

   
    private void OnTriggerEnter(Collider other)
    {
        //Room1
        /*if (other.CompareTag("Floor1") && !gameManager.isPaused && !touchedFirstFloor)
        {
            textMessage.text = "1 txt";
            touchedFirstFloor = true;
            StartCoroutine(ShowAndPutAwayMessage(messageCanvas));
        }*/
        if (other.CompareTag("TimeMachine1") && !gameManager.isPaused)
        {
          
            key.SetActive(true);
            timeMachine1.SetActive(false);
         //   sepia.enabled = true;

            textMessage.text = "Я прикоснулся к идолу, на котором была надпись: «Ты можешь вернуться в прошлое," +
                " но у всего есть последствия», и он исчез Почему все в сепии? Я в прошлом?! Что это за ключ?";
            StartCoroutine(ShowAndPutAwayMessage(messageCanvas, 9));
        }
        else if (other.CompareTag("Key") && !gameManager.isPaused)
        {
            textMessage.text = "Ключ? Его здесь раньше не было." +
                " Возьму его с собой, может он мне пригодится.";
            keyTaken = true;
            StartCoroutine(ShowAndPutAwayMessage(messageCanvas));
            
          //  sepia.enabled = false;
            key.SetActive(false);
            textMessage.text = "Что снова произошло? И куда исчезла сепия?";
            StartCoroutine(ShowAndPutAwayMessage(messageCanvas));
        }

        //Room2
       /* else if (other.CompareTag("Floor2") && !gameManager.isPaused && !touchedSecondFloor)
        {
            touchedSecondFloor = true;
            textMessage.text = "вошли во вторую комнату";
            StartCoroutine(ShowAndPutAwayMessage(messageCanvas));
        }*/
        else if (other.CompareTag("Cross") && !gameManager.isPaused)
        {
            openedWay.SetActive(true);
            closedWay.SetActive(false);
            textMessage.text = "Крест выглядит как большая кнопка, я нажал на него и появился путь к идолу.";
            StartCoroutine(ShowAndPutAwayMessage(messageCanvas, 5));
        }
        else if (other.CompareTag("TimeMachine2") && !gameManager.isPaused)
        {
            textMessage.text = "Снова сепия, глазам так приятнее.\n Куда делся крест?";
            StartCoroutine(ShowAndPutAwayMessage(messageCanvas, 4));
            timeMachine2.SetActive(false);
            cross.SetActive(false);
            picture.SetActive(true);
         //   sepia.enabled = true;
        }
        else if (other.CompareTag("Picture") && !gameManager.isPaused)
        {
            picture.SetActive(false);
            pincodeCanvas.gameObject.SetActive(true);
            gameManager.isPaused = true;
            StartCoroutine(PinToTheCorner());
          //  sepia.enabled = false;
            touchedPic = true;
        }

        //Room3
        
        else if (other.CompareTag("Gold") && !gameManager.isPaused)
        {
            timeMachine3.SetActive(false);
            textHistory.text = "Передо мной слитки золота я достиг своей цели. Наконец-то я стану успешнее моего брата." +
                " Он всегда был во всем первым." +
                " А теперь посмотрим кого мама будет больше любить.";
            chosenGoodEnding = true;
            ShowHistory();
        }
        else if (other.CompareTag("TimeMachine3") && !gameManager.isPaused)
        {
            SceneManager.LoadScene("LastLastScene");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor3") && !gameManager.isPaused && !enteredThirdRoom)
        {
            enteredThirdRoom = true;
            textHistory.text = "Наконец я достиг своей цели и добрался до золота," +
                " а что это в стенах? Мне кажется там драгоценности, я думаю что они обоготят меня ещё больше." +
                " Я также вижу идола возможно он поможет мне найти оружие что бы достать эти камни." +
                "Что же мне выбрать? довольствоваться золотом Или вернуться в прошлое что бы попытать удачу с добычей драгоценностей.";

            ShowHistory();
        }
        else if (collision.gameObject.CompareTag("Pregrada2") && !gameManager.isPaused && touchedPic)
        {
            textHistory.text = "Рассмотрю стену поближе." +
                " На некоторых кирпичах есть последовательности таких же символов. " +
                "Я касаюсь по памяти на нужную последовательность…";
            testOpened = true;
            ShowHistory();
            
        }
        else if (collision.gameObject.CompareTag("Floor2") && !gameManager.isPaused && !touchedSecondFloor)
        {
            touchedSecondFloor = true;
            textMessage.text = "Снова замурованная стена." +
                " Я вижу идола, но как до него добраться? \nХм, что это за крест и почему он светится как идол?";
            StartCoroutine(ShowAndPutAwayMessage(messageCanvas, 7));
        }
        if (collision.gameObject.CompareTag("Floor1") && !gameManager.isPaused && !touchedFirstFloor)
        {
            textMessage.text = "Проход дальше замурован, что же мне делать." +
                " Может в этой комнате есть что-то, что сможет мне убрать преграду";
            touchedFirstFloor = true;
            StartCoroutine(ShowAndPutAwayMessage(messageCanvas, 5));
        }
        else if (collision.gameObject.CompareTag("Pregrada1") && !gameManager.isPaused && !touchedPregrada1 && keyTaken)
        {
            pregrada1.SetActive(false);
            touchedPregrada1 = true;
            textHistory.text = "Возможно этот ключ сможет мне помочь." +
                " Я обнаружил в стене отверстие, похожее на замочную скважину." +
                " Я вставил ключ и, к моему удивлению, он подошел," +
                " преграда исчезла на моих глазах. В путь";
            ShowHistory();
        }
    }

    IEnumerator ShowAndPutAwayMessage(Canvas message, float waitingTime = 3)
    {
        message.gameObject.SetActive(true);
        yield return new WaitForSeconds(waitingTime);
        message.gameObject.SetActive(false);
    }

    IEnumerator PinToTheCorner()
    {
        yield return new WaitForSeconds(3);
        gameManager.isPaused = false;
        pincodeCanvas.gameObject.SetActive(false);
        pincodeCanvas2.gameObject.SetActive(true);
    }

    public void OpenLastRoom()
    {
        pregrada2.SetActive(false);
        testCanva.gameObject.SetActive(false);
        gameManager.isPaused = false;
        
    }

    public void FailedToOpen()
    {
        Vector3 setbackPos = new Vector3(-92, transform.position.y, 12);
        transform.localPosition = setbackPos;
        testCanva.gameObject.SetActive(false);
        gameManager.isPaused = false;
        pincodeCanvas2.gameObject.SetActive(true);
        textMessage.text = "Стена все еще на месте. Где я допустил ошибку?";
        StartCoroutine(ShowAndPutAwayMessage(messageCanvas));
    }

    public void ReturnToGame()
    {
        historyCanvas.gameObject.SetActive(false);
        //controlsCanvas.gameObject.SetActive(false);
        gameManager.isPaused = false;
        if (chosenGoodEnding)
        {
            TenYears();
            
        }
        else if (testOpened)
        {
            pincodeCanvas2.gameObject.SetActive(false);
            Debug.Log("Napishi otvet");
            testCanva.gameObject.SetActive(true);
            gameManager.isPaused = true;
            testOpened = false;
        }
        else if(noTen)
        {
            StartCoroutine(showGoodPic());
            
        }
        else if(touchedPregrada1)
        {
            pregrada1.gameObject.SetActive(false);
            touchedPregrada1 = false;
        }
    }

    IEnumerator showGoodPic()
    {
        badPic.gameObject.SetActive(true);
        noTenCanvas.gameObject.SetActive(false);
        tenCanvas.gameObject.SetActive(false);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Credits");
    }

    public void ShowHistory()
    {
        historyCanvas.gameObject.SetActive(true);
        gameManager.isPaused = true;
        
    }

   /* public void ShowControls()
    {
        controlsCanvas.gameObject.SetActive(true);
        gameManager.isPaused = true;
        controlesShowed = true;
    }*/

    public void TenYears()
    {
        tenCanvas.gameObject.SetActive(true);
        gameManager.isPaused = true;
        StartCoroutine(ten());
        
    }

    IEnumerator ten()
    {        
        yield return new WaitForSeconds(3);
        NoTen();
    }

    public void NoTen()
    {
        noTen = true;
        chosenGoodEnding = false;
        noTenCanvas.gameObject.SetActive(true);
        textHistory.text = "Забрав золото, он так и не смог стать счастливым, мать всё так же сравнивает его с братом и ненависть продолжает расти.";
        ShowHistory();
    }
}
