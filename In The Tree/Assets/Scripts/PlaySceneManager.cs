using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlaySceneManager : MonoBehaviour
{
    public List<ClickableItem> clickables;
    public List<GameObject> clickableObjects = new List<GameObject>();
    private AudioSource audioSource;
    public Button sceneAudButton;
    private int numObjects;
    public Scenario selectedScenarioSO;
    public GameObject selectedScenarioObj;
    private DisplayScenario selectedScenarioUI;
    internal int dialect = 1;
    private int numItemsDropped;
    internal int curItem = 0;
    int numErrors = 0;
    public AudioClip corrAud;
    public AudioClip wrongAud;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        sceneAudButton.interactable = false;
        numObjects = selectedScenarioObj.transform.childCount;
        selectedScenarioUI = selectedScenarioObj.GetComponent<DisplayScenario>();

        SetDraggableOrder();
    }

    public List<int> IndicesArray(int length)
    {
        Debug.Log("" + length);
        List<int> draggableIndices = new List<int>();
        for (int i = 0; i < length; i++)
        {
            draggableIndices.Add(i);
        }
        return draggableIndices;
    }

    public void SetDraggableOrder()
    {
        List<ClickableItem> tempDraggables = selectedScenarioSO.scenarioClickableItems;
        List<int> iArr = IndicesArray(numObjects);

        int iRand;
        for (int i = 0; i < numObjects; i++)
        {
            iRand = Random.Range(0, 1);
            Debug.Log("irand:" + iRand);
            clickables.Add(tempDraggables[iArr[iRand]]);
            clickableObjects.Add(selectedScenarioObj.transform.GetChild(iArr[iRand]).gameObject);
            clickables[i].ThisItemIndex(i);
            clickables[i].ResetSO();
            iArr.RemoveAt(iRand);
        }
        StartCoroutine(InstructDragging(curItem));
    }

    public IEnumerator InstructDragging(int curItem)
    {
        yield return new WaitUntil(() => !audioSource.isPlaying);
        yield return new WaitForSeconds(2f);
        if (clickables[curItem].IsInstructionCustom())
        {
            selectedScenarioUI.ShowCustomInstruction(clickables[curItem].InstructionString());
        }
        else
        {
            Debug.Log(clickables[curItem].WordString());
            selectedScenarioUI.ShowRepeater(clickables[curItem].WordString());
        }

        audioSource.PlayOneShot(clickables[curItem].clickInstruction);

        yield return new WaitUntil(() => !audioSource.isPlaying);
        sceneAudButton.interactable = true;
    }

    public void CountItemsLayered(bool correct)
    {
        PlayResultAud(correct);
        if (!correct)
        {
            numErrors++;
            if (numErrors == 3)
            {
                clickableObjects[curItem].GetComponent<DisplayClickable>().HighlightCorrectItem();
            }
        }

        else if (correct)
        {
            numErrors = 0;
            numItemsDropped++;
            curItem++;
            //scoreText.text = "" + curItem;

            StartCoroutine(AfterCorrDrop());
        }
    }

    public IEnumerator AfterCorrDrop()
    {
        yield return new WaitForSeconds(1);
        sceneAudButton.interactable = false;
        yield return new WaitUntil(() => !audioSource.isPlaying);
        audioSource.PlayOneShot(selectedScenarioSO.repeaterPhraseAud);

        if (numItemsDropped == clickables.Count)
        {
            yield return new WaitForSeconds(1);
            yield return new WaitUntil(() => !audioSource.isPlaying);
            StartCoroutine(Success());
        }
        else
        {
            clickableObjects[curItem].GetComponent<MoveObjectTo>().OnCorrClick();
            StartCoroutine(InstructDragging(curItem));
        }
    }

    public IEnumerator Success()
    {
        new WaitForSeconds(2);
        selectedScenarioUI.ShowSuccess();
        //big particle effect
        yield return new WaitUntil(() => !audioSource.isPlaying);
        audioSource.PlayOneShot(selectedScenarioSO.successPhraseAud);
        yield return new WaitWhile(() => audioSource.isPlaying);
        ShowCompletionScreen();
    }

    public IEnumerator ShowCompletionScreen()
    {
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => !audioSource.isPlaying);

        selectedScenarioUI.ShowCompletionText();
        audioSource.PlayOneShot(selectedScenarioSO.completionPhraseAud);
        yield return new WaitWhile(() => audioSource.isPlaying);
    }

    public void SetDialect(int currDialect)
    {
        dialect = currDialect;

        foreach (ClickableItem draggableItem in clickables)
        {
            draggableItem.SetCurrDialect(currDialect);
        }
    }

    public void ResetAllDraggableObjects()
    {
        foreach (GameObject draggableObject in clickableObjects)
        {
            draggableObject.GetComponent<DisplayClickable>().ResetDraggableDisplay();
        }
        clickables.Clear();
        clickableObjects.Clear();
    }

    public void ReplaySceneAud()
    {

        audioSource.PlayOneShot(clickables[curItem].clickInstruction);
    }

    public void PlayResultAud(bool corr)
    {
        if(corr)
        {
            audioSource.PlayOneShot(corrAud);
        }
        else
        {
            audioSource.PlayOneShot(wrongAud);
        }
    }

}
