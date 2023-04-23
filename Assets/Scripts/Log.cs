using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform movePoint;
    public AudioController audioController;
    public LayerMask whatStopsMovement;
    public bool canMove;
    public bool previousCanMove;
    private int hasToAct;

    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.idle;
        target = GameObject.FindWithTag("Player").transform;
        movePoint.parent = null;
        audioController = target.GetComponent<AudioController>();
        hasToAct = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        canMove = audioController.canMove;

        // la variable hasToAct permet de savoir si on est au tout début d'un beat (et donc le log doit bouger)
        // l'ennemi ne pourra agir que 1 beat sur 2, lors de la première frame du beat
        if((canMove==true && previousCanMove==false) || hasToAct ==1)
        {
            hasToAct ++;
        }
        if((canMove==false && previousCanMove==true) && hasToAct==3)
        {
            hasToAct = 0;
        }
        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f && hasToAct ==1)
        {
            // si la cible est dans son champs de vision, le log se déplace vers elle
            // si la cible est à portée d'attaque, le log ataque
            CheckDistance();
        }
        else if(Vector3.Distance(transform.position, movePoint.position) >= 0.05f)
        {
            // mise à jour de l'animation et déplacement du log
            UpdateAnimationsAndMove();
        }
        previousCanMove = canMove;
    }
    void CheckDistance()
    {
        if(Vector3.Distance(target.position, transform.position)<= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius) // déplacement du log
        {
            if(currentState==EnemyState.idle || currentState==EnemyState.walk)
            {
                MoveMovePoint();
                ChangeState(EnemyState.walk);
                anim.SetBool("wakeUp", true);
            }
        }
        else if(Mathf.Round(Mathf.Abs(Vector3.Distance(target.position,transform.position)))==1) // attaque du log
        {
            StartCoroutine(AttackCo());
            PlayerHealth playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
            playerHealth.health--;
            HealthHeartBar healthHeartBar = GameObject.FindWithTag("HealthHolder").GetComponent<HealthHeartBar>();
            healthHeartBar.DrawHearts();
        }
        else // il se rendort si la cible est trop loin
        {
            anim.SetBool("wakeUp", false);
        }
    }
    void MoveMovePoint() // déplacement du movePoint du log, en fonction de la position de sa cible (pour s'en rapprocher au plus vite)
    {
        float horizontalDistance = target.position.x - transform.position.x;
        float verticalDistance = target.position.y - transform.position.y;
        if(horizontalDistance > 0 && Mathf.Abs(horizontalDistance)>Mathf.Abs(verticalDistance)) 
        {
            // l'ennemi se déplace à droite
            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(1f, 0f, 0f), .2f, whatStopsMovement)) // on vérifie qu'il n'y a pas d'objets
            {
                movePoint.position += new Vector3(1, 0f, 0f);
            }
        }
        else if (horizontalDistance < 0 && Mathf.Abs(horizontalDistance) > Mathf.Abs(verticalDistance))
        {
            // l'ennemi se déplace à gauche
            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(-1f, 0f, 0f), .2f, whatStopsMovement)) // on vérifie qu'il n'y a pas d'objets
            {
                movePoint.position += new Vector3(-1, 0f, 0f);
            }
        }
        else if (verticalDistance>0)
        {
            //  l'ennemi se déplace vers le haut
            if (!Physics2D.OverlapPoint(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), whatStopsMovement)) // on vérifie qu'il n'y a pas d'objets
            {
                movePoint.position += new Vector3(0f, 1, 0f);
            }
        }
        else
        {
            //  l'ennemi se déplace vers le bas
            if (!Physics2D.OverlapPoint(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), whatStopsMovement)) // on vérifie qu'il n'y a pas d'objets
            {
                movePoint.position += new Vector3(0f, -1, 0f);
            }
        }
    }
    private void ChangeState(EnemyState newState)
    {
        if (newState != currentState)
        {
            currentState = newState;
        }
    }
    void UpdateAnimationsAndMove() 
    {
        anim.SetBool("moving", true);
        float deplacementX = movePoint.position.x - transform.position.x;
        float deplacementY = movePoint.position.y - transform.position.y;
        anim.SetFloat("moveX", deplacementX);
        anim.SetFloat("moveY", deplacementY);
    }
    private IEnumerator AttackCo() // coroutine pour l'attaque du log
    {
        anim.SetBool("attacking", true);
        currentState = EnemyState.attack;
        yield return null; // attend 1 frame
        anim.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f);
        currentState = EnemyState.walk;
    }
}
