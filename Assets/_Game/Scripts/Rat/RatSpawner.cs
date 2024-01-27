using Assets._Game.Scripts.Rat;
using System.Collections.Generic;
using UnityEngine;

public class RatSpawner : MonoBehaviour
{
    public static RatSpawner instance;

    public List<SpriteRenderer> spawnPoints;
    public List<SpriteRenderer> spawnPointsActive;

    public float timePerRat = 1;
    private float nextSpawnTime = 0f;

    private void Awake()
    {
        instance = this;
        foreach (SpriteRenderer img in spawnPoints)
        {
            img.enabled = false;
            spawnPointsActive.Add(img);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Time.time < nextSpawnTime)
        {
            return;
        }

        if (!RatManager.instance.AreAllRatsSpawned)
        {
            nextSpawnTime = Time.time + timePerRat / 10;
            SpawnRat();
        }
    }

    private void SpawnRat()
    {
        RatNPC npc = RatManager.instance.GetNotActiveRat();
        if (npc != null)
        {
            RatManager.instance.RatSpawned(npc);
            npc.gameObject.SetActive(true);
            PositionRat(npc);
        }
    }

    private void PositionRat(RatNPC npc)
    {
        if (spawnPointsActive.Count == 0)
        {
            AllSpawnPointsUsed();
        }

        int posIndex = Random.Range(0, spawnPointsActive.Count);
        SpriteRenderer spawnPos = spawnPointsActive[posIndex];

        spawnPointsActive.Remove(spawnPos);

        if (spawnPos != null)
        {
            npc.transform.position = spawnPos.transform.position;
        }
    }

    private void AllSpawnPointsUsed()
    {
        foreach (SpriteRenderer img in spawnPoints)
        {
            spawnPointsActive.Add(img);
        }
    }
}
