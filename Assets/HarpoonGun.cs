using UnityEngine;

public class HarpoonGun : Holdable
{
    public float FireSpeed = 5f;
    public float FireDelay = 2f;
    public HarpoonHead head;
    public float cur = 0f;
    public override void ChildUpdate()
    {
        if (!InFire)
        {
            head.rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        cur -= Time.deltaTime;
    }
    bool InFire = false;
    public override void Use()
    {
        if(cur > 0f)
        {
            InFire = true;
            head.parent = this;
            head.rb.constraints = RigidbodyConstraints.None;
            head.rb.AddForce(PlayerMovement.instance.playerCamera.transform.forward * FireSpeed);
            cur = FireDelay;
        }
    }
}
