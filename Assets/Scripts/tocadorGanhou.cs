using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tocadorGanhou : MonoBehaviour
{
    AudioSource audio5;

    public void playGanhou()
    {
        audio5 = GetComponent<AudioSource>();
        audio5.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
