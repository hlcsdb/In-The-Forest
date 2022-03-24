using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Scenario curScenario;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(GameObject.Find("Game Manager").gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
