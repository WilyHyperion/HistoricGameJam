using UnityEngine;

public class Throwable : Holdable
{
    public float throwStrength = 1f;
    public override void Use()
    {
        Debug.Log("ran");
        this.UnHold();

        this.GetComponent<Rigidbody>().AddForce(PlayerMovement.instance.playerCamera.transform.forward * throwStrength, ForceMode.Impulse);
    }
}
