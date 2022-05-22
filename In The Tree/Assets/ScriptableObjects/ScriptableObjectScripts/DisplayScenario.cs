using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayScenario : MonoBehaviour
{
    public Scenario scenario;
    public GameObject scenarioObject;
    //public DraggableItem[] scenarioDraggables;
    public GameObject[] scenarioClickableObjects;
    public TextMeshProUGUI sceneText;
    public TextMeshProUGUI scenarioName;
    public AudioClip sceneDescriptionAud;
    public AudioClip openerPhraseAud;
    public AudioClip repeaterPhraseAud; //instructions that proceed the word of every item, eg. LAY DOWN THE plate
    public AudioClip successPhraseAud;
    public AudioClip completionPhraseAud;
    public Image backgroundImage;
    private List<Vector2> startSlots;
    


    // Start is called before the first frame update
    void Start()
    {
        backgroundImage.sprite = scenario.backgroundImage;
        //scenarioName.text = scenario.scenarioName;
        sceneDescriptionAud = scenario.sceneDescriptionAud;
        openerPhraseAud = scenario.openerPhraseAud;
        repeaterPhraseAud = scenario.repeaterPhraseAud;
        successPhraseAud = scenario.successPhraseAud;
        completionPhraseAud = scenario.completionPhraseAud;
    }

    public void PopulateStartSlots()
    {
        foreach (GameObject draggable in scenario.scenarioClickableObjects)
        {
            startSlots.Add(draggable.transform.localPosition);
        }
    }

    public void ShowDescription()
    {
        sceneText.text = scenario.sceneDescription[scenario.dialect];
    }

    public void ShowRepeater(string wordText)
    {
        sceneText.text = "" + scenario.repeaterPhrase[scenario.dialect] + " " + wordText + "?";
    }

    public void ShowCustomInstruction(string itemName)
    {
        sceneText.text = "" + scenario.repeaterPhrase[scenario.dialect] + " " + itemName + "?";
    }

    public void EmptyScenarioText()
    {
        sceneText.text = "";
    }

    public void ShowSuccessText()
    {
        sceneText.text = scenario.successPhrase[scenario.dialect];
    }

    public void ShowCompletionText()
    {
        sceneText.text = scenario.completionPhrase[scenario.dialect];
    }

    public void ShowScenarioName()
    {
        scenarioName.text = scenario.scenarioName;
    }

    public void ShowFailureText()
    {
        sceneText.text = scenario.failurePhrase[scenario.dialect];
    }

    public void ShowFeedbackText(string feedbackText)
    {
        sceneText.text = feedbackText;
    }
}
