using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneController : MonoBehaviour
{

    public void GotoHomeScene()
    {
        Debug.Log("go home");
        SceneManager.LoadScene("Home");
    }

    public void GotoInTheTreePlayScene()
    {
        SceneManager.LoadScene("InTheTreePlay");
       
    }

    public void GotoInTheTreeVocabScene()
    {
        SceneManager.LoadScene("InTheTreeVocab");
    }
}
