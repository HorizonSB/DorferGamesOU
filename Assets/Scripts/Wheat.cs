using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Wheat : MonoBehaviour
{
    [SerializeField] private GameObject wheatBlockPrefab;
    [SerializeField] private GameObject cutPoint;

    private int currentCutsNumber;
    private int cutsNumberMax = 2;

    private bool canCut = true;

    private float timerToGrow = 10;

    public UnityEvent cutEvent = new UnityEvent();
    public UnityEvent growEvent = new UnityEvent();

    public void Cut()
    {
        if(currentCutsNumber < cutsNumberMax && canCut)
        {
            cutEvent?.Invoke();
            currentCutsNumber++;
        }
        else if(currentCutsNumber == cutsNumberMax && canCut)
        {
            canCut = false;
            cutPoint.SetActive(false);
            Instantiate(wheatBlockPrefab, transform.position, Quaternion.identity);
            StartCoroutine("WheatGrowing");
        }

        if(Player.Instance.PlayerSelectedWheat() == this && !canCut)
        {
            Player.Instance.SetCutBool(false);
        }
    }

    private IEnumerator WheatGrowing()
    {
        yield return new WaitForSeconds(timerToGrow);
        currentCutsNumber = 0;
        canCut = true;
        cutPoint.SetActive(true);
        growEvent?.Invoke();
    }

    public bool CanCut()
    {
        return canCut;
    }
}
