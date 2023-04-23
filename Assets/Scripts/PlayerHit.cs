using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // lorsque le joueur attaque, cela active les box colliders autour de lui
        // on regarde le tag de l'objet en contact avec ce collider pour savoir s'il s'agit d'un pot à casser ou d'un ennemi à blesser
        if(other.CompareTag("breakable"))
        {
            other.GetComponent<Pot>().Smash();
        }
        else if(other.CompareTag("log"))
        {
            other.GetComponent<Log>().Hit();
        }
    }
}
