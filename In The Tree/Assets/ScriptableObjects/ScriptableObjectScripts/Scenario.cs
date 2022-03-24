using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScenarioName
{
    InTheTree
}

[CreateAssetMenu(fileName = "New Scenario", menuName = "Scenario")]

public class Scenario : ScriptableObject
{
    public GameObject scenarioObject;
    public ScenarioName scenarioType;
    public string scenarioName;
    public List<GameObject> scenarioClickableObjects = new List<GameObject>();
    public List<ClickableItem> scenarioClickableItems;
    public string[] sceneDescription;
    public string[] openerPhrase;

    public string[] repeaterPhrase; //instructions that proceed the word of every item, eg. LAY DOWN THE plate // if this is different for different objects in the scenario, leave it blank and specify in DisplayDraggable
    public string[] successPhrase;

    public string[] completionPhrase;

    //public AudioSource audioS;
    public AudioClip sceneDescriptionAud;
    public AudioClip openerPhraseAud;
    public AudioClip repeaterPhraseAud; //instructions that proceed the word of every item, eg. LAY DOWN THE plate
    public AudioClip successPhraseAud;
    public AudioClip completionPhraseAud;
    public AudioClip incorrectSelectionAud;

    public int numClickables;
    public Sprite backgroundImage;
    public int dialect;

    public List<Vector2> randSlots = new List<Vector2>();

    public void Awake()
    {
        numClickables = scenarioClickableItems.Count;
        randSlots = new List<Vector2> { new Vector2(-400, 135), new Vector2(-400, 20), new Vector2(-400, -95), new Vector2(-400, -210), new Vector2(400, 135), new Vector2(400, 20), new Vector2(400, -95), new Vector2(400, -210) };
        //PopulateStartSlots(numDraggables);
    }
    public void SetDialect(int currDialect)
    {
        dialect = currDialect;
        SetDialectOfDraggables(currDialect);
    }

    public void SetDialectOfDraggables(int currDialect)
    {
        //foreach(DraggableItem draggable in scenarioDraggables)
        //{
        //    draggable.SetCurrDialect(currDialect);
        //}
    }

    public void HideScenarioObject()
    {
        scenarioObject.SetActive(false);
    }

    public void ShowScenarioObject()
    {
        scenarioObject.SetActive(true);
    }
}