using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    creature
}

[CreateAssetMenu(fileName = "New Clickable Item", menuName = "Clickable/ClickableItem")]

public class ClickableItem : ScriptableObject
{
    public ItemType type;
    public string description;
    public string[] wordString = new string[3];
    public string[] customInstructionText = new string[3];
    public string[] clickFeedbackTextA;
    public string[] clickFeedbackTextB;
    public GameObject tile;

    public Sprite itemSprite;

    public int thisRandIndex;
    public bool clicked;
    public bool correct;
    public int dialect = 1;
    public Color[] tileStateOutlineColors = new Color[] { new Color(1, 1, 1, 1), new Color(0.9882f, 0.9333f, 0.1294f, 1), new Color(0.9490f, 0.3294f, 0.1882f, 1f) };
    public AudioClip audioClip;
    public AudioClip clickInstruction;
    public AudioClip clickFeedbackA;
    public AudioClip clickFeedbackB;
    public Vector2 selectionPos;
    public Vector2 startPos;
    public bool activeInScenario;


    public void SetCurrDialect(int currDialect)
    {
        dialect = currDialect;
    }

    public string Description()
    {
        return description;
    }

   
    public string WordString()
    {
        return wordString[dialect];
    }

    public string InstructionString()
    {
        string instruction = customInstructionText[dialect] + " " + wordString[dialect] + "?";
        return instruction;
    }

    public bool IsInstructionCustom()
    {
        return customInstructionText[dialect] != "";
    }

    public void SetIfCorrect(bool yes)
    {
        correct = yes;
    }

    public bool IsItCorrect()
    {
        return correct;
    }

    public bool IsItClicked()
    {
        return clicked;
    }

    public void Dragged(bool isClicked)
    {
        clicked = isClicked;
    }

    public void ResetSO()
    {
        dialect = 1;
        clicked = false;
        correct = false;
    }

    public AudioClip GetAudio()
    {
        return audioClip;
    }

    public AudioClip GetFeedbackAud(string whichFeedback)
    {
        if (whichFeedback == "B")
        {
            return clickFeedbackB;
        }
        return clickFeedbackA;
    }

    public string GetFeedbackString(string whichFeedback)
    {
        if (whichFeedback == "A"){
            return clickFeedbackTextA[dialect];
        }
        return clickFeedbackTextB[dialect];
        
    }
    public void ThisItemIndex(int thisIndex)
    {
        thisRandIndex = thisIndex;
    }
}