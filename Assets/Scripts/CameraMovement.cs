using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    public Vector2 maxPosition;
    public Vector2 minPosition;
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(transform.position != target.position)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z); // pour r�gler probl�me en z (sinon la cam�ra partait et on ne voyait plus rien

            // Pour que la cam�ra ne sorte pas du champs :
            targetPosition.x = Mathf.Clamp(targetPosition.x,minPosition.x,maxPosition.x); //retourne toujours un x entre min et max
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y); //retourne toujours un y entre min et max

            // Pour d�placer la cam�ra
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }
}
