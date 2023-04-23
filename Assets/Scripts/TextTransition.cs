using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTransition : MonoBehaviour
{
    public List<GameObject> textList=new List<GameObject>(); // liste des textes successifs à écrire
    public int displayText; // place dans la liste du texte en cours de lecture
    public TextTrigger textTrigger; // zone de déclenchement du texte

    // Start is called before the first frame update
    void Start()
    {
        displayText = 0;
        textList[displayText].SetActive(true);
        textTrigger = GetComponent<TextTrigger>();
    }

    void Update()
    {
        // le joueur appuie sur n'importe quelle touche pour passer à la suite
        if(Input.anyKeyDown==true)
        {
            if(displayText==textList.Count)
            {
                textTrigger.End();
            }
            
            StartCoroutine(TextChangeCo());
        }
    }
    private IEnumerator TextChangeCo() // coroutine pour passer au texte suivant
    {
        textList[displayText].SetActive(false);
        yield return null; // attend 1 frame
        displayText++;
        textList[displayText].SetActive(true);
    }
}
