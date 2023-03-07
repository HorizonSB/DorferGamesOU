using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatCounterUI : MonoBehaviour
{
    [SerializeField] private Vector3 offset;

    private void Update()
    {
        transform.position = Player.Instance.transform.position + offset;
    }
}
