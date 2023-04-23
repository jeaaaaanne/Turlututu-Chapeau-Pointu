using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack
}

public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    public int health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Hit() //fonction appelée lorsque l'ennemi est touché par une attaque du joueur
    {
        health--;
        if (health==0)
        {
            anim.SetBool("killed", true);
            StartCoroutine(deathCo());
        }
    }
    IEnumerator deathCo() // coroutine pour la gestion de la mort d'un ennemi
    {
        yield return new WaitForSeconds(.3f);
        this.gameObject.SetActive(false);
    }
}
