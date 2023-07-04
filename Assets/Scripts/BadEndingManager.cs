using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using TMPro;

public class BadEndingManager : MonoBehaviour
{
    [SerializeField] private Canvas historyCanvas;
    [SerializeField] private Canvas messageCanvas;
    [SerializeField] private TextMeshProUGUI historyText;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private ThirdPlayerController playerContr;
  //  [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject pastFinish;
    [SerializeField] private GameObject presentFinish;
    [SerializeField] private GameObject pickaxe;
    [SerializeField] private Canvas tenYearsCanvas;
    [SerializeField] private Canvas noTenCanvas;
    [SerializeField] private Canvas goodPic;
    bool isPickaxeTouched = false;
    bool isFirstHistoryShown = false;


   /* [SerializeField] private Canvas firstText;
    [SerializeField] private Canvas lastText;
    [SerializeField] private Canvas tenYearsLater;*/
   

    private void Start()
    {   
        messageText.text = "������� ���� ��������� ������� ������ �� �������, ��� �� ������������� ��������.\n" +
            "�, �����, �� ��� �����! " +
            "������ � ����� ������� �� �������������, ��� ��������� � �����.";
        StartCoroutine(ShowAndPutAwayMessage(8));
    }

    private void Update()
    {
        if(playerContr.isPickAxeTriggered)
        {
            playerContr.isPickAxeTriggered = false;
            pickaxe.SetActive(false);
            pastFinish.SetActive(false);
            presentFinish.SetActive(true);
          //playerCamera.GetComponent<Effects>().enabled = false;
            messageText.text = "��� ��������? ��� ��� ������?! � ����� � ���������?!";
            StartCoroutine(ShowAndPutAwayMessage());
            isPickaxeTouched = true;
        }
    }

    IEnumerator Wait(float waitTime = 3)
    {
        yield return new WaitForSeconds(waitTime);
    }

    IEnumerator ShowAndPutAwayMessage(float waitingTime = 3)
    {
        messageCanvas.gameObject.SetActive(true);
        yield return new WaitForSeconds(waitingTime);
        messageCanvas.gameObject.SetActive(false);

        if (isPickaxeTouched)
        {
            yield return new WaitForSeconds(10);
            historyText.text = "������, ������ �����, � ������� ��� �������, " +
                "� �������� ��� � �� ���� ���������. � ������ �� ���� � ���� ��������, ���� ��� �����.";
            ShowHistory();
        }
    }
    public void ShowHistory()
    {
        historyCanvas.gameObject.SetActive(true);
        playerContr.isPaused = true;

    }
    public void Next()
    {
        if(isFirstHistoryShown)
        {
            StartCoroutine(GoodPicc());
        }
        else
        {
            isFirstHistoryShown = true;
            StartCoroutine(Ten());
        }
    }

    IEnumerator GoodPicc()
    {
        goodPic.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Credits");
    }

    IEnumerator Ten()
    {
        historyCanvas.gameObject.SetActive(false);
        tenYearsCanvas.gameObject.SetActive(true);

        yield return new WaitForSeconds(3);

        noTenCanvas.gameObject.SetActive(true);
        historyText.text = "���� ���� �������� ���� ���������. " +
            "� ���� ����������� �� ����� ������ � ����������� ��������� � ������. ������ �� ������ ������.";
        ShowHistory();
    }

}
