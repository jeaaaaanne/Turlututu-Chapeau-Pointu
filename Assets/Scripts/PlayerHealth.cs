using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health=6;
    public float maxHealth=6;
    public GameObject deathText;
    public TextTransition textTransition;

    void Update()
    {
        if(health==0)
        {
            // à la mort du joueur, un texte s'affiche puis l'application se ferme automatiquement (sera changé par retour au menu principal)
            deathText.SetActive(true);
            textTransition.enabled = true;

            Application.Quit();
        }
    }
}
