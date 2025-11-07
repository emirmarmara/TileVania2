using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    // Start is called before the first frame update
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player"){
        //Bu yöntemle bir delay ekliyoruz fonksiyona
        StartCoroutine(LoadNextLevel());
        }
       
       
    }

    IEnumerator LoadNextLevel(){
        yield return new WaitForSecondsRealtime(levelLoadDelay);
         int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
         int nextSceneIndex = currentSceneIndex + 1;
         //Fazladan index numarası eklemeyi engelliyo başa dönüyo.
         if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) {
            nextSceneIndex = 0;
         }
        FindObjectOfType<ScenePersist>().ReserScenePersist();
        SceneManager.LoadScene(nextSceneIndex);
    }
}
