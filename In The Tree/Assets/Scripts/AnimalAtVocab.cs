using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class AnimalAtVocab : MonoBehaviour, IPointerClickHandler
{
    private float timer = 0.0f;
    private float scaleDur = 0.1f;
    private float maxSize = 1.05f;
    private float minSize = 0.95f;
    private float rotateAmount = 10.0f; //Amount to rotate in degrees
    //private LearnSceneController learnSceneController;
    //private TextMeshProUGUI hovertext;
    private AudioSource audioSource;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        //learnSceneController = GameObject.Find("LearnSceneController").GetComponent<LearnSceneController>();
        //hovertext = GameObject.Find("AnimalLabel").gameObject.GetComponent<TextMeshProUGUI>();
        audioSource = GameObject.Find("Carousel Display").GetComponent<AudioSource>();
        anim.SetBool("isIdle", true);

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //if (learnSceneController.inSelection)
        //{
        StartCoroutine(DoAnimalAction());
        //hovertext = gameObject.transform.GetChild(0).gameObject;
        //hovertext.text = GetComponent<DisplayClickable>().clickable.WordString();
        audioSource.PlayOneShot(GetComponent<DisplayClickable>().clickable.GetAudio());
        //}
        //anim.SetBool("move", true);
    }

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    //if (learnSceneController.inSelection)
    //    //{
    //    StartCoroutine(Shrink());
    //    //hovertext.text = "";
    //    audioSource.Stop();
    //    //}
    //    //anim.SetBool("isIdle", true);
    //    anim.SetBool("move", false);
    //}

    private IEnumerator Grow()
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

    private IEnumerator Shrink()
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

    //public void HighlightCorrectItem()
    //{
    //    //StartCoroutine(TwistDraggable());
    //    StartCoroutine(GrowShrinkLoop());
    //}

    public IEnumerator TwistDraggable()
    {
        Debug.Log("twisting");
        transform.Rotate(0.0f, 0.0f, rotateAmount / 2);
        for (int i = 0; i < 2; i++)
        {
            if (transform.rotation.z > 0)
            {
                transform.Rotate(0.0f, 0.0f, -rotateAmount);
            }
            else if (transform.rotation.z < 0)
            {
                transform.Rotate(0.0f, 0.0f, rotateAmount);
            }
            yield return new WaitForSeconds(0.07f);
        }
        transform.Rotate(0.0f, 0.0f, -(rotateAmount / 2));
    }

    public IEnumerator GrowShrinkLoop()
    {
        for (int i = 0; i < 2; i++)
        {
            StartCoroutine(Grow());
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(Shrink());
            yield return new WaitForSeconds(0.2f);
        }
    }

    public IEnumerator DoAnimalAction()
    {
        anim.SetBool("move", true);
        yield return new WaitForSeconds(1.25f);
        anim.SetBool("move", false);
        //StartCoroutine(Grow());
        //yield return new WaitForSeconds(0.2f);
        //StartCoroutine(Shrink());
        //yield return new WaitForSeconds(0.2f);
    }
    //public IEnumerator ShowLabelForSeconds(int seconds)
    //{
    //    hovertext.text = GetComponent<DisplayClickable>().clickable.WordString();
    //    hovertext.SetActive(true);
    //    yield return new WaitForSeconds(4);
    //    hovertext.text = "";
    //}
}
