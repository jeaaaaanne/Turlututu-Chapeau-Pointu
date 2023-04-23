using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTransition : MonoBehaviour
{
    public List<GameObject> textList=new List<GameObject>();
    public int displayText;
    public TextTrigger textTrigger;

    // Start is called before the first frame update
    void Start()
    {
        displayText = 0;
        textList[displayText].SetActive(true);
        textTrigger = GetComponent<TextTrigger>();
    }

    void Update()
    {
        if(Input.anyKeyDown==true)
        {
            if(displayText==textList.Count)
            {
                textTrigger.End();
            }
            
            StartCoroutine(TextChangeCo());
        }
    }
    private IEnumerator TextChangeCo()
    {
        textList[displayText].SetActive(false);
        yield return null; // attend 1 frame
        displayText++;
        textList[displayText].SetActive(true);
    }
}
