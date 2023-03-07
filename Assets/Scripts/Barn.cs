using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barn : MonoBehaviour
{
    [SerializeField] private float gatherRadius = 3f;

    private Player player;
    public static Barn Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        player = Player.Instance;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if(distanceToPlayer < gatherRadius && player.CurrentWheatAmount() > 0)
        {
            player.SellAllWheat();
        }
    }
}
