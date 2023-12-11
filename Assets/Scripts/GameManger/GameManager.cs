using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    public void DisablePlayerController()
    {
        playerController.anim.Play("Player_Idle");
        playerController.canMove = false;
        //playerController.enabled = false;
    }
    public void EnablePlayerController()
    {
        playerController.canMove = true;
        //playerController.enabled = true;
    }
}
