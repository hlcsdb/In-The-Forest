using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveObjectTo : MonoBehaviour, IPointerClickHandler
{
    Animator anim;
    public float exitDur;
    public GameObject targetPos;

    private void Start()
    {
        anim = GetComponent<Animator>();
        foreach (AnimatorControllerParameter param in anim.parameters)
        {
            if (hasIdleStateAnim())
            {
                anim.SetBool("isIdle", true);
            }
        }
    }

    bool hasIdleStateAnim()
    {
        foreach (AnimatorControllerParameter param in anim.parameters)
        {
            if (param.name == "isIdle")
            {
                return true;
            }
        }
        return false;
    }


    void Update()
    {
        if (transform.parent.transform.localPosition.x > 700 || transform.parent.transform.localPosition.x < -700 || transform.parent.transform.localPosition.y < -400 || transform.parent.transform.localPosition.y > 400)
        {
            Destroy(gameObject);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("clicked");
        anim.SetBool("move", true);
        StartCoroutine(LerpPosition(targetPos, exitDur));
    }

    IEnumerator LerpPosition(GameObject targetPos, float duration)
    {
        float time = 0;
        Vector2 targetPosition = (targetPos.transform.localPosition);
        Vector2 startPosition = transform.parent.transform.localPosition;

        while (time < duration)
        {
            transform.parent.transform.localPosition = Vector2.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.parent.transform.localPosition = targetPosition;
    }
}