using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHeartBar : MonoBehaviour
{
    public GameObject heartPrefab;
    public GameObject player;
    public PlayerHealth playerHealth;
    public float health=0;
    public float maxHealth=0;
    List<HealthHeart> hearts = new List<HealthHeart>();

    private void Start()
    {
        DrawHearts();
    }

    public void DrawHearts()
    {

        health = playerHealth.health;
        maxHealth = playerHealth.maxHealth;
        ClearHearts();

        int heartsToMake = (int)((maxHealth / 2) + (maxHealth%2));
        for(int i=0;i<heartsToMake;i++)
        {
            CreateEmptyHeart();
        }
        for(int i=0;i<hearts.Count;i++)
        {
            int heartStatusRemainder = (int)(Mathf.Clamp(health - (i * 2), 0, 2));
            hearts[i].SetHeartImage((HeartStatus)heartStatusRemainder);
        }
    }
    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);

        HealthHeart heartComponent = newHeart.GetComponent<HealthHeart>();
        heartComponent.SetHeartImage(HeartStatus.Empty);
        hearts.Add(heartComponent);
    }
    public void ClearHearts()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts = new List<HealthHeart>();
    }
}
