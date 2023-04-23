using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour
{
    public GameObject text;
    public TextTransition textTransition;
    public GameObject player;
    public AudioController audioController;

    private void Start()
    {
        textTransition = GetComponent<TextTransition>();
        audioController = player.GetComponent<AudioController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // lorsque le joueur entre dans la zone, cela active le texte et sa transition
        if (collision.CompareTag("Player"))
        {
            // il est nécessaire de bloquer les mouvements pour éviter qu'il ne se passe des choses en arrière plan durant la lecture
            audioController.movesBlocked = true; 
            // activation du texte et des transitions entre ses petites parties
            text.SetActive(true);
            textTransition.enabled = true;
        }
    }
    public void End()
    {
        text.SetActive(false);
        textTransition.enabled = false;
        audioController.movesBlocked = false;
    }
}
