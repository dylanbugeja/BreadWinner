using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressTracker : MonoBehaviour
{
    [SerializeField] private Transform highPoint;
    [SerializeField] private Transform lowPoint;

    private int total;
    private TrackTarget tracker;
    private GameManager gm;
    private bool performed;

    private void Awake()
    {
        tracker = FindObjectOfType<TrackTarget>();
        gm = FindObjectOfType<GameManager>();
    }
    private void Start()
    {
        total = gm.totalPlayers;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (performed == false)
        {
            tracker.NewLevelPart(highPoint, lowPoint);
            performed = true;
        }

        Destroy(gameObject);
    }
}
