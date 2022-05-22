using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveObjectTo : MonoBehaviour
{
    Animator anim;
    public float exitDur;
    public GameObject targetPos;
    public int yBound;
    public int xBound;
    public bool willLerp;

    private void Start()
    {
        anim = GetComponent<Animator>();
        //foreach (AnimatorControllerParameter param in anim.parameters)
        //{
        //    if (hasIdleStateAnim())
        //    {
                //anim.enabled = false;
                anim.SetBool("isIdle", true);
                
        //    }
        //}
    }
    //bool hasIdleStateAnim()
    //{
    //    foreach (AnimatorControllerParameter param in anim.parameters)
    //    {
    //        if (param.name == "isIdle")
    //        {
    //            return true;
    //        }
    //    }
    //    return false;
    //}


    void Update()
    {
        if (willLerp)
        {
            if (transform.parent.transform.localPosition.x > xBound || transform.parent.transform.localPosition.x < -xBound || transform.parent.transform.localPosition.y < -yBound || transform.parent.transform.localPosition.y > yBound)
            //if (transform.parent.transform.localPosition == targetPos.transform.position)
            {
                Debug.Log("atPosition");
                Destroy(gameObject);
            }
        }
        else if (!willLerp)
        {
            if (transform.localPosition.x == xBound || transform.localPosition.y == yBound)
            //if(transform.localPosition == targetPos.transform.position)
            {
                Debug.Log("atPosition");
                Destroy(gameObject);
            }
        }
    }

    public void OnCorrClick()
    {
        Debug.Log("clicked");
        //anim.enabled = true;
        anim.SetBool("move", true);
        if (willLerp)
        {
            Debug.Log("toLerp");
            StartCoroutine(LerpPosition(targetPos, exitDur));
        }
    }

    IEnumerator LerpPosition(GameObject targetPos, float duration)
    {
        float time = 0;
        yield return new WaitForSeconds(.5f);
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