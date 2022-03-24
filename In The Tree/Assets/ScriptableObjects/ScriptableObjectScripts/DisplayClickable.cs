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

    private float timer = 0.0f;
    private float scaleDur = 0.1f;

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

    public void HighlightCorrectItem()
    {
        //StartCoroutine(TwistDraggable());
        StartCoroutine(GrowShrinkLoop());
    }

    public IEnumerator GrowShrinkLoop()
    {
        for (int i = 0; i < 2; i++)
        {
            StartCoroutine(Grow(1.2f));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(Shrink(0.5f));
            yield return new WaitForSeconds(0.2f);

        }
        StartCoroutine(Grow(1f));
    }
    private IEnumerator Grow(float maxSize)
    {
        Vector2 startScale = transform.localScale;
        Vector2 maxScale = new Vector2(maxSize, maxSize);
        do
        {
            transform.localScale = Vector3.Lerp(startScale, maxScale, timer / scaleDur);
            timer += Time.deltaTime;
            yield return null;
        }
        while (timer < scaleDur);
        timer = 0;
    }

    private IEnumerator Shrink(float minSize)
    {
        Vector2 startScale = transform.localScale;
        Vector2 minScale = new Vector2(minSize, minSize);
        do
        {
            transform.localScale = Vector3.Lerp(startScale, minScale, timer / scaleDur);
            timer += Time.deltaTime;
            yield return null;
        }
        while (timer < scaleDur);
        timer = 0;
    }
}
