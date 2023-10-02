using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class fadeInTexto : MonoBehaviour
{

	float timer;
	bool animationStarted = false;
	public float delay;
	public TextMeshProUGUI text;

	IEnumerator FadeImage(bool order) {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);

        while (text.color.a < 1.0f) {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + Time.deltaTime / 30);
            yield return null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
     	timer += Time.deltaTime;
     	
     	if (timer >= delay && !animationStarted) {
			StartCoroutine(FadeImage(true));
 	        
 	        animationStarted = true;
        }   
    }
}
