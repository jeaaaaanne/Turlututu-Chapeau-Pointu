using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float smoothing; // plus sa valeur est importante, plpus la caméra va se déplacer rapidement vers le personnage
    public Vector2 maxPosition; // limites max en x et y pour ne pas sortir de la carte
    public Vector2 minPosition; // limites min en x et y pour ne pas sortir de la carte

    void LateUpdate()
    {
        if(transform.position != target.position)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z); // pour régler problème en z (sinon la caméra partait et on ne voyait plus rien

            // Pour que la caméra ne sorte pas du champs :
            targetPosition.x = Mathf.Clamp(targetPosition.x,minPosition.x,maxPosition.x); //retourne toujours un x entre min et max
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y); //retourne toujours un y entre min et max

            // Pour déplacer la caméra
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }
}
