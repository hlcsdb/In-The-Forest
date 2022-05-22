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
    internal bool haltInput = true;



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
            iRand = Random.Range(0, iArr.Count);
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
        yield return new WaitForSeconds(1f);
        if (clickables[curItem].IsInstructionCustom())
        {
            selectedScenarioUI.ShowCustomInstruction(clickables[curItem].WordString());
        }
        else
        {
            Debug.Log(clickables[curItem].WordString());
            selectedScenarioUI.ShowRepeater(clickables[curItem].WordString());
        }

        PlayInstructionAud();

        yield return new WaitUntil(() => !audioSource.isPlaying);
        sceneAudButton.interactable = true;
        haltInput = false;
    }
    
    public void CountItemsLayered(bool correct)
    {
        haltInput = true;
        if (!correct)
        {
            StartCoroutine(AfterWrongDrop());
            numErrors++;
            if (numErrors >= 3)
            {
                clickableObjects[curItem].transform.GetChild(0).GetComponent<DisplayClickable>().HighlightCorrectItem();
            }
        }

        else if (correct)
        {
            numErrors = 0;
            numItemsDropped++; //this might be redundant
            StartCoroutine(AfterCorrDrop());
        }
    }

    public IEnumerator AfterWrongDrop()
    {
        sceneAudButton.interactable = false;
        selectedScenarioUI.ShowFailureText();
        audioSource.PlayOneShot(selectedScenarioSO.GetIncorrectAudio());
        yield return new WaitUntil(() => !audioSource.isPlaying);

        //yield return new WaitForSeconds(1);
        PlayInstructionAud();
        selectedScenarioUI.ShowCustomInstruction(clickables[curItem].WordString());
        yield return new WaitUntil(() => !audioSource.isPlaying);
        haltInput = false;
        sceneAudButton.interactable = true;
    }

    public IEnumerator AfterCorrDrop()
    {
        sceneAudButton.interactable = false;

        audioSource.PlayOneShot(clickables[curItem].GetFeedbackAud("A"));
        selectedScenarioUI.ShowFeedbackText(clickables[curItem].GetFeedbackString("A"));
        yield return new WaitUntil(() => !audioSource.isPlaying);

        clickableObjects[curItem].transform.GetChild(0).GetComponent<MoveObjectTo>().OnCorrClick();

        audioSource.PlayOneShot(clickables[curItem].GetFeedbackAud("B"));
        selectedScenarioUI.ShowFeedbackText(clickables[curItem].GetFeedbackString("B"));
        yield return new WaitUntil(() => !audioSource.isPlaying);
        

        if (numItemsDropped == clickables.Count)
        {
            yield return new WaitForSeconds(1);
            yield return new WaitUntil(() => !audioSource.isPlaying);
            StartCoroutine(Success());
        }
        else
        {
            curItem++;
            yield return new WaitForSeconds(1.5f);
            StartCoroutine(InstructDragging(curItem));
        }
    }

    public IEnumerator Success()
    {
        new WaitForSeconds(2);
        
        //big particle effect
        yield return new WaitUntil(() => !audioSource.isPlaying);
        
        new WaitForSeconds(1);
        audioSource.PlayOneShot(selectedScenarioSO.GetSuccessAudio());
        selectedScenarioUI.ShowSuccessText();
        //ShowCompletionScreen();
    }

    //public IEnumerator ShowCompletionScreen()
    //{
    //    yield return new WaitForSeconds(1);
    //    yield return new WaitUntil(() => !audioSource.isPlaying);

    //    selectedScenarioUI.ShowCompletionText();
    //    audioSource.PlayOneShot(selectedScenarioSO.GetCompletionAudio());
    //    yield return new WaitUntil(() => !audioSource.isPlaying);
    //}

    //public void SetDialect(int currDialect)
    //{
    //    dialect = currDialect;

    //    foreach (ClickableItem draggableItem in clickables)
    //    {
    //        draggableItem.SetCurrDialect(currDialect);
    //    }
    //}

    public void ResetAllDraggableObjects()
    {
        foreach (GameObject draggableObject in clickableObjects)
        {
            draggableObject.GetComponent<DisplayClickable>().ResetDraggableDisplay();
        }
        clickables.Clear();
        clickableObjects.Clear();
    }

    public void PlayInstructionAud()
    {
        audioSource.PlayOneShot(clickables[curItem].clickInstruction);
    }
}
