using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private int coinsAmount;
    [SerializeField] private Transform coinStartFlyPoint;
    [SerializeField] private TextMeshProUGUI moneyCounter;
    [SerializeField] private GameObject coinPrefab;


    bool addingCoin = false;

    public static ShopManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        moneyCounter.text = this.coinsAmount.ToString();
    }

    public void GetMoney(int coinsAmount)
    {
        GameObject coin = Instantiate(coinPrefab, Barn.Instance.transform.position, Quaternion.identity);

        float flyTime = 1f;
        coin.transform.DOMove(moneyCounter.gameObject.transform.parent.position, flyTime).OnComplete(() => {

            StartCoroutine(BustingMoneyCounter(coinsAmount));

            float shakeDutaion = 1f;
            float strenth = 0.2f;
            int vibrato = 3;
            float randomness = 10f;
            bool fadeOut = true;
            ShakeRandomnessMode randomnessMove = ShakeRandomnessMode.Harmonic;
            moneyCounter.transform.DOShakeScale(shakeDutaion, strenth, vibrato, randomness, fadeOut, randomnessMove).OnComplete(() => {
                Vector3 startScale = new Vector3(1, 1, 1);
                float dutation = 0.3f;
                moneyCounter.transform.DOScale(startScale, dutation);
            });

            Destroy(coin);
        });
    }

    private IEnumerator BustingMoneyCounter(int moneyAmount)
    {
        float timeToAddCoin = 0.01f;

        for (int i = 0; i < moneyAmount; i++)
        {
            coinsAmount++;
            moneyCounter.text = coinsAmount.ToString();
            yield return new WaitForSeconds(timeToAddCoin);

        }
    }
}
