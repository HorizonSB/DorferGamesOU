using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePoint : MonoBehaviour
{
    [SerializeField] private Transform wheatBlockPrefab;
    [SerializeField] private Vector3 wheatBlockOffset;


    private List<GameObject> wheatBlocks;

    public static HandlePoint Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        wheatBlocks = new List<GameObject>();

        for(int i =0; i< Player.Instance.MaxWheatAmount(); i++)
        {
            Transform wheatBlock = Instantiate(wheatBlockPrefab, gameObject.transform);
            wheatBlock.position += wheatBlockOffset * i;
            wheatBlocks.Add(wheatBlock.gameObject);
            wheatBlock.gameObject.SetActive(false);
        }
    }

    public void SetActiveBlock()
    {
        for(int i =0; i < Player.Instance.CurrentWheatAmount(); i ++)
        {
            wheatBlocks[i].gameObject.SetActive(true);
        }
    }

    public void ResetWheatBlocks()
    {
        foreach(GameObject wheatBlock in wheatBlocks)
        {
            wheatBlock.SetActive(false);
        }
    }
}
