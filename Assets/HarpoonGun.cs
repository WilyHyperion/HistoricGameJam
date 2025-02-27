using UnityEngine;
public enum FireState
{
    Ready,
    Out,
    DrawBack,
}
public class HarpoonGun : Holdable
{
    public Vector3 original;
    public Quaternion orgrot;
    public float FireSpeed = 5f;
    public float FireDelay = 2f;
    public HarpoonHead head;
    public float cur = 0f;
    public override void ChildStart()
    {
        original = head.rb.position;
        orgrot = head.rb.rotation;
    }
    public override void ChildUpdate()
    {
        if (state == FireState.Ready)
        {
            head.rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        cur -= Time.deltaTime;
        if(state == FireState.DrawBack)
        {
            DrawBackProgress += Time.deltaTime;
            head.rb.position = Vector3.Lerp(DrawBackStart, original, DrawBackProgress / DrawBackTime);
            if(DrawBackProgress/DrawBackTime > 1f)
            {
                Debug.Log("ran");
                head.rb.position = original;
                head.rb.rotation = orgrot;
                Physics.SyncTransforms();
                state = FireState.Ready;
                head.rb.constraints = RigidbodyConstraints.FreezeAll;
            }
        }
    }
    public FireState state = FireState.Ready;
    public float DrawBackProgress = 0f;
    public float DrawBackTime = 1f;
    public Vector3 DrawBackStart;
    public override void Use()
    {
        if (state == FireState.Out)
        {
            state = FireState.DrawBack;
            DrawBackProgress = 0f;
            DrawBackStart = head.rb.position;
        }
        else
        {
            if (cur < 0f)
            {
                state = FireState.Out;
                head.parent = this;
                head.rb.constraints = RigidbodyConstraints.None;
                head.rb.AddForce(PlayerMovement.instance.playerCamera.transform.forward * FireSpeed);
                cur = FireDelay;
            }
        }
    }
}
