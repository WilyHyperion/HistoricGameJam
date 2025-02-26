using UnityEngine;

public class Boat : MonoBehaviour
{
    public float HP = 100;
    public static Boat Instance;
    public float Speed = 1f;
    void Start()
    {
        Instance = this;
        Last = transform.position;
    }
    Vector3 Last;
    public static Vector3 Delta = new Vector3();
    void Update()
    {
        transform.position += this.transform.forward* Time.smoothDeltaTime * Speed;
        Delta = transform.position - Last;
        Last = transform.position;
    }

}
