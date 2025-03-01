using UnityEngine;
public enum FireState
{
    Ready,
    Out,
    DrawBack,
}
public class HarpoonGun : Holdable
{
    public Vector3 headOffset;
    public float FireSpeed = 5f;
    public float FireDelay = 2f;
    public HarpoonHead head;
    public float cur = 0f;
    public override void ChildStart()
    {
        head.parent = this;
        Physics.IgnoreCollision(col, head.GetComponent<Collider>());
        Physics.IgnoreCollision(PlayerMovement.instance.characterController, head.GetComponent<Collider>());
    }
    public override void ChildUpdate()
    {
        if (!OnBoat && !held)
        {
            //snap back on
            transform.position = Boat.Instance.transform.position + new Vector3(0, 10, 0);
        }
        if (state == FireState.Ready)
        {
            head.transform.position = this.transform.position + headOffset;
            head.transform.forward = this.transform.forward;
            head.rb.rotation = this.transform.rotation;
            head.rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        cur -= Time.deltaTime;
        if(state == FireState.DrawBack)
        {
            DrawBackProgress += Time.deltaTime;
            head.transform.position = Vector3.Lerp(DrawBackStart, this.transform.position + headOffset, DrawBackProgress / DrawBackTime);
            if(DrawBackProgress/DrawBackTime > 1f)
            {
                SetHeadVisable(false);
                state = FireState.Ready;
            }
        }
    }
    public FireState state = FireState.Ready;
    public float DrawBackProgress = 0f;
    public float DrawBackTime = 1f;
    public Vector3 DrawBackStart;
    Collider lasthit;
    public override void Use()
    {
        if (state == FireState.Out)
        {
            head.CanGrab = false;
            state = FireState.DrawBack;
            DrawBackProgress = 0f;
            DrawBackStart = head.transform.position;
        }
        else
        {
            if (head.hit != null)
            {
                head.hit.GetComponent<Collider>().enabled = true;
                lasthit = head.hit.GetComponent<Collider>();
                Physics.IgnoreCollision(lasthit, head.GetComponent<Collider>());
                head.hit = null;
                return;
            }

            SetHeadVisable(true);
            if (cur < 0f)
            {
                if (lasthit != null)
                {
                    Physics.IgnoreCollision(lasthit, head.GetComponent<Collider>(), false);
                }
                head.CanGrab = true;
                state = FireState.Out;
                head.rb.constraints = RigidbodyConstraints.None;
                head.transform.position = PlayerMovement.instance.transform.position;
                head.rb.AddForce(PlayerMovement.instance.playerCamera.transform.forward* FireSpeed, ForceMode.Impulse);
                cur = FireDelay;
            }
        }
    }
    public void SetHeadVisable(bool val)
    {
        foreach(Renderer r in head.GetComponentsInChildren<Renderer>())
        {
            r.forceRenderingOff = !val;
        }
    }
}
