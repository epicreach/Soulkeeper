using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GoToBoss : MonoBehaviour
{
    
    public void goToBoss(){
        SceneManager.LoadSceneAsync(0);
    }

}
