using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayClickable : MonoBehaviour
{

    public ClickableItem clickable;
    public TextMeshProUGUI wordString;
    //public Image draggableArtwork;
    public GameObject tile;
    //public Vector2 startPos;
    //internal Vector2 startRandPos;
    //internal Vector2 randPos;
    internal int randI;

    public void Start()
    {
        clickable.startPos = transform.localPosition;
        randI = clickable.thisRandIndex;
    }

    //public void SetRandPos(Vector2 rPos)
    //{
    //    randPos = rPos;
    //    transform.localPosition = randPos;
    //}

    public void ColourTileOutline(int state)
    {
        tile.transform.GetChild(0).GetComponent<Image>().color = clickable.tileStateOutlineColors[state];
    }

    public void SetWord()
    {
        wordString.text = clickable.WordString();
    }

    public void ShowHideTile()
    {
        ColourTileOutline(0);

        if (tile.activeSelf)
        {
            tile.SetActive(false);
        }
        else
        {
            tile.SetActive(true);
        }
    }

    public void HideWord()
    {
        wordString.text = "";
    }


    public void ResetDraggableDisplay()
    {
        transform.localPosition = clickable.startPos;
        tile.SetActive(true);
    }

    //public Vector2 ThisRandomPos()
    //{
    //    return randPos;
    //}
}
