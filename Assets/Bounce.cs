using UnityEngine;

public class Bounce : MonoBehaviour
{
    public float Height;
    public float current = 0f;
    public float Speed = 2f;
    bool dir = false;
    void Update()
    {
        float inc = Time.deltaTime * (dir ? 1 : -1) * Speed;
        current += inc;
        transform.position = transform.position + new Vector3(0,inc, 0);
        if( Mathf.Abs(current) > Height)
        {
            dir = !dir;
        }
    }
}
