using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    void Awake() 
    {
        //Ne kadar varsa civarda buluyo:)
        int numScenePersists = FindObjectsOfType<ScenePersist>().Length;
        if (numScenePersists > 1){
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ReserScenePersist()
    {
        Destroy(gameObject);
    }
}
