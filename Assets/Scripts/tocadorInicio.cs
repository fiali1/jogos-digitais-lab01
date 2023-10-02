using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tocadorInicio : MonoBehaviour
{
    AudioSource audio1;

    public void playInicio()
    {
        audio1 = GetComponent<AudioSource>();
        audio1.Play();
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
