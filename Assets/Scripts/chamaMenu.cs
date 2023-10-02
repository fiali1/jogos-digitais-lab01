using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class chamaMenu : MonoBehaviour
{
    float timer;
    bool creditsEnd = false;
    public float creditsLength;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= creditsLength && !creditsEnd) {
            SceneManager.LoadScene("MenuPrincipalCustom");
        }
    }
}
