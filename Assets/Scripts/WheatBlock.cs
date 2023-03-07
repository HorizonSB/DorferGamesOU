using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class WheatBlock : MonoBehaviour
{
    [SerializeField] private float gatherRange = 2f;
    [SerializeField] private float blockHeight = 0.1f;
    [SerializeField] private float speed = 0.5f;

    private Player player;

    private bool wasTaken = false;
    private bool wasSet = false;
    private bool sold = false;

    [Range(0, 1)]
    [SerializeField] private float timer;

    private Transform startPoint;
    private Transform handlePoint;
    private Transform endPoint;

    private float blockLifetimeOnGround = 100f;

    private void Start()
    {
        player = Player.Instance;
        startPoint = transform;
    }

    private void Update()
    {
        handlePoint = player.HandlePointTransform();

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (player.CurrentWheatAmount() < player.MaxWheatAmount() && distanceToPlayer < gatherRange && !wasTaken)
        {
            wasTaken = true;
            player.AddWheat(this);
            endPoint = handlePoint;
            endPoint.position += new Vector3(0f, blockHeight, 0f);
        }

        if (wasTaken)
        {
            timer += Time.deltaTime * speed;
        }
            

        if (wasTaken && !sold)
        {
            transform.position = Bezier.GetFollowPointVector(startPoint, endPoint, timer);
        }

        if(timer > 1f && !wasSet && player.CurrentWheatAmount() <= player.MaxWheatAmount())
        {
            wasSet = true;
            transform.parent = player.HandlePointTransform();

            float offsetZ = -0.7f;
            Vector3 handlePointLocalPosition = new Vector3(0, 0, offsetZ);
            transform.parent.transform.localPosition = handlePointLocalPosition;
            SetToPlayerHandlePoint();
            HandlePoint.Instance.SetActiveBlock();
        }

        if (sold)
        {
            StartCoroutine(DestroyBlock());
        }
    }

    public void SellOut()
    {
        transform.parent = null;
        sold = true;

        bool snapping = false;
        float changeDuration = 1f;

        transform.DOMove(Barn.Instance.transform.position, changeDuration, snapping).OnComplete(() => {

            int blockCost = 15;
            ShopManager.Instance.GetMoney(blockCost);

            if (timer > blockLifetimeOnGround && transform.parent == null)
            {
                Destroy(gameObject);
            }
        });
    }

    private void SetToPlayerHandlePoint()
    {

        float blockSizeY = 0.3f;
        float changeDuration = 0.1f;
        transform.DOScaleY(blockSizeY, changeDuration);

        transform.localPosition = new Vector3(0, blockHeight * player.CurrentWheatAmount(), 0f);

        float moveAngelZ = 90;
        transform.localRotation = Quaternion.Euler(0, 0, moveAngelZ);
    }

    private IEnumerator DestroyBlock()
    {
        float timeTillDestroy = 10f;
        yield return new WaitForSeconds(timeTillDestroy);
        Destroy(gameObject);
    }
}
