using UnityEngine;

public class Snapper : Interactable
{
    public Transform SnapPoint;

    public PlayerMovement Player = PlayerMovement.instance;
    public override void OnInteract()
    {
        Player.GetComponent<CharacterController>().enabled = false;
        Player.transform.position = SnapPoint.transform.position;
        Player.GetComponent<CharacterController>().enabled = true;
    }
}
