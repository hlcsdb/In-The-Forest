using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Target : MonoBehaviour, IPointerClickHandler
{
    private PlaySceneManager playManagerScript;
    private bool clicked = false;
    private MoveObjectTo moveObjectScript;
    // Start is called before the first frame update
    void Start()
    {
        playManagerScript = GameObject.Find("Play Scene Manager").GetComponent<PlaySceneManager>();
        moveObjectScript = GetComponent<MoveObjectTo>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("clicked");

        if (!clicked)
        {
            clicked = true;
            GetComponent<BoxCollider>().enabled = false;

            if (gameObject.name == playManagerScript.clickableObjects[playManagerScript.curItem].transform.GetChild(0).name)
                {
                    GetComponent<DisplayClickable>().HighlightCorrectItem();
                    playManagerScript.CountItemsLayered(true);
                }

             else
                {
                    //Debug.Log("target tags don't match" + "; gameobj tag: " + gameObject.tag + "; targetNum: " + challengeSpawnManagerScript.targetTagNum);
                    //transform.GetChild(0).gameObject.SetActive(true);
                    playManagerScript.CountItemsLayered(true);

            }
        }

        }
    
    
}
