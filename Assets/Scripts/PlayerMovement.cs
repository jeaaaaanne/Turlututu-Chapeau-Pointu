using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    walk,
    attack,
    interact
}

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D myRigidbody;
    public Transform movePoint;
    public PlayerState currentState;

    public LayerMask whatStopsMovement;

    public Animator animator;
    public AudioController audioController;
    public bool canMove;
    private bool flag=false;
    private bool previousCanMove;
    private bool playerIsActing;
    private bool playerWasActing;

    void Start()
    {
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();
        //myRigidbody = GetComponent<Rigidbody2D>();
        movePoint.parent = null;
        audioController = GetComponent<AudioController>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
    }
    void Update()
    {
        if (Input.GetKey("escape")) // pour quitter l'application, tant qu'il n'y a pas de menu
        {
            Application.Quit();
        }
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        //myRigidbody.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        canMove = audioController.canMove;
        playerIsActing = Input.anyKeyDown; // true si le joueur appuie
        
        flag = false; // ne vaut true que sur la première frame où canMove est true et le joueur appuie 
        if (canMove == true && playerIsActing==true && playerWasActing==false)
        {
            flag = true;
        }

        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack)
        {
            if (flag == true)
            {
                StartCoroutine(AttackCo());
            }
        }
        else if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f) // l'objet movePoint et le perso sont au même endroit donc on peut réenclencher un déplacement (le perso est actuellement à l'arrêt) // à changer avec currentState wlaking ?
        {
            animator.SetBool("moving", false); 

            // le perso peut se déplacer si il y a la place ou bien seulement se tourner si un objet bloque son mouvement

            if (flag == true) // le perso va bouger seulement si le joueur appuie sur un beat
            {
                // si il y a un objet autour, le perso se tourne seulement
                if (Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, whatStopsMovement) || Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, whatStopsMovement))
                {

                }
                // sinon, le perso se déplace 
                else
                {
                    MoveMovePoint(); // on déplace l'objet movePoint d'une case
                }
            }
        }
        else if (Vector3.Distance(transform.position, movePoint.position) >= 0.05f) // else if(currentState === PlayerState.walk)
        {
            // Le joueur est en mouvement, gestion des animations :
            UpdateAnimationAndMove();
        }

        previousCanMove = canMove;
        playerWasActing = playerIsActing;
    }
    void MoveMovePoint()
    {
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f) // pour gauche ou droite : abs
        {
            if (!Physics2D.OverlapPoint(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), whatStopsMovement)) // on vérifie qu'il n'y a pas d'objets
            {
                movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
            }
        }
        else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f) // pour haut ou bas : abs
        {
            if (!Physics2D.OverlapPoint(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), whatStopsMovement)) // on vérifie qu'il n'y a pas d'objets
            {
                movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
            }
        }
    }
    void UpdateAnimationAndMove()
    {
        animator.SetBool("moving", true);
        float deplacementX = movePoint.position.x - transform.position.x;
        float deplacementY = movePoint.position.y - transform.position.y;
        animator.SetFloat("moveX", deplacementX);
        animator.SetFloat("moveY", deplacementY);
    }
    private IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null; // attend 1 frame
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f);
        currentState = PlayerState.walk;
    }
}
