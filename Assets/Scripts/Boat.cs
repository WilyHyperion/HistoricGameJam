using UnityEngine;

public class Boat : MonoBehaviour
{
    public AudioSource hitsoud;
    public float MaxHP = 100;
    public float HP;
    public static Boat Instance;
    public float Speed = 1f;
    void Start()
    {
        HP = MaxHP;
        Instance = this;
        Last = transform.position;
    }
    Vector3 Last;
    public static Vector3 Delta = new Vector3();
    void Update()
    {
        if(HP <= 0)
        {

            Speed = 0f;
            GameManager.DisplayGameOver();
        }
        {
            transform.position += this.transform.forward * Time.smoothDeltaTime * Speed;
            Delta = transform.position - Last;
            Last = transform.position;
        }
    }

}
