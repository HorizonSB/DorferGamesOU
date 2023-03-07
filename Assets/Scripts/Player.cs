using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;

    [SerializeField] private DynamicJoystick joystick;
    [SerializeField] private Transform handlePointTransform;
    [SerializeField] private TextMeshProUGUI wheatCounterText;
    [SerializeField] private GameObject weapon;


    private CharacterController characterController;

    private Wheat selectedWheat;
    private List<WheatBlock> wheatBlocksList;

    [SerializeField] private int maxWheatAmount = 40;
    private int currentWheatAmount = 0;

    private float radiusOfInteraction = 1f;
    private const string WHEAT_CUT_POINT_TAG = "cutPoint";

    private bool isMoving = false;
    private bool isCutting = false;


    public static Player Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        wheatBlocksList = new List<WheatBlock>();
    }

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        wheatCounterText.text = wheatBlocksList.Count.ToString() + "/" + maxWheatAmount.ToString();
        weapon.SetActive(false);
    }

    private void Update()
    {

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            MoveCharacter(new Vector3(joystick.Horizontal, 0, joystick.Vertical));
            RotateCharacter(new Vector3(joystick.Horizontal, 0, joystick.Vertical));

            isMoving = true;
            isCutting = false;
            weapon.SetActive(false);
        }
        else
        {
            isMoving = false;
            GameObject[] wheatCutPoints = GameObject.FindGameObjectsWithTag(WHEAT_CUT_POINT_TAG);
            foreach(GameObject wheatCutPoint in wheatCutPoints)
            {
                float distanceToPoint = Vector3.Distance(transform.position, wheatCutPoint.transform.position);
                if(distanceToPoint < radiusOfInteraction)
                {
                    selectedWheat = wheatCutPoint.transform.parent.gameObject.GetComponent<Wheat>();
                    if (selectedWheat.CanCut())
                    {
                        isCutting = true;
                        weapon.SetActive(true);
                        Interact(wheatCutPoint.transform);
                    }
                }
            }
        }
    }

    public void MoveCharacter(Vector3 moveDirection)
    {
        moveDirection *= moveSpeed;
        characterController.Move(moveDirection * Time.deltaTime);
    }

    public void RotateCharacter(Vector3 moveDirection)
    {
        if (Vector3.Angle(transform.forward, moveDirection) > 0)
        {
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, moveDirection, rotateSpeed, 0);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }

    private void Interact(Transform cutPoint)
    {
        transform.position = cutPoint.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward);
    }

    public void AddWheat(WheatBlock wheatBlock)
    {
        wheatBlocksList.Add(wheatBlock);
        currentWheatAmount++;
        wheatCounterText.text = currentWheatAmount + "/" + maxWheatAmount.ToString();
    }

    public int CurrentWheatAmount()
    {
        return currentWheatAmount;
    }

    public int MaxWheatAmount()
    {
        return maxWheatAmount;
    }

    public void SellAllWheat()
    {
        currentWheatAmount = 0;
        StartCoroutine(SellAllWheatEnumerator());
    }

    private IEnumerator SellAllWheatEnumerator()
    {
        float flyTimer = 0.05f;
        HandlePoint.Instance.ResetWheatBlocks();

        float waitTillClearList = 2f;

        foreach (WheatBlock wheatBlock in wheatBlocksList)
        {
            wheatBlock.SellOut();
            yield return new WaitForSeconds(flyTimer);
        }

        yield return new WaitForSeconds(waitTillClearList);

        wheatBlocksList.Clear();
        wheatCounterText.text = currentWheatAmount + "/" + maxWheatAmount.ToString();
    }

    public Transform HandlePointTransform()
    {
        return handlePointTransform;
    }

    public bool IsMoving()
    {
        return isMoving;
    }

    public bool IsCutting()
    {
        return isCutting;
    }

    public void Cut()
    {
        selectedWheat.Cut();
    }

    public void SetCutBool(bool isCutting)
    {
        this.isCutting = isCutting;
    }

    public Wheat PlayerSelectedWheat()
    {
        return selectedWheat;
    }
}
