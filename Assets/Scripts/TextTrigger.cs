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
        if (collision.CompareTag("Player"))
        {
            audioController.movesBlocked = true;
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
