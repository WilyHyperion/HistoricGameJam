using UnityEngine;

public class Throwable : Holdable
{
    public float throwStrength = 1f;
    public override void Use()
    {
        Debug.Log("ran");
        this.UnHold();
        Physics.IgnoreCollision(col, PlayerMovement.instance.characterController);
        this.transform.position = PlayerMovement.instance.playerCamera.transform.position;
        this.GetComponent<Rigidbody>().AddForce(PlayerMovement.instance.playerCamera.transform.forward * throwStrength, ForceMode.Impulse);
    }
}
