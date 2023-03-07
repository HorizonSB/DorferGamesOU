using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Player player;
    private Animator animator;

    private const string MOVING_ANIMATION_BOOL = "IsRunning";
    private const string CUTTING_ANIMATION_BOOL = "IsCutting";

    private void Start()
    {
        player = Player.Instance;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (player.IsMoving())
        {
            animator.SetBool(MOVING_ANIMATION_BOOL, true);
        }
        else
        {
            animator.SetBool(MOVING_ANIMATION_BOOL, false);
        }

        if (player.IsCutting())
        {
            animator.SetBool(CUTTING_ANIMATION_BOOL, true);
        }
        else
        {
            animator.SetBool(CUTTING_ANIMATION_BOOL, false);
        }
    }

    public void PlayerCut()
    {
        player.Cut();
    }
}
