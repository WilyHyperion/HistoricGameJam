using UnityEngine;

public  class GameManager : MonoBehaviour
{
    public static Vector3 WindDirection = new Vector3(1, 0, 0);
    public float Timer;
    public float Spawnint = 0.5f;
    public GameObject[] icebergs;
    public void Update()
    {
        Timer += Time.deltaTime;
        if(Timer > Spawnint)
        {
            Timer = 0;
            Instantiate(randomIceberg(), Boat.Instance.transform.position + (Quaternion.AngleAxis(Random.Range(-30f, 30f), Vector3.up) * Boat.Instance.transform.forward  * Random.Range(150f, 200f)), Quaternion.identity);
        }
    }
    public  GameObject randomIceberg()
    {
        return icebergs[Random.Range(0, icebergs.Length - 1)];
    }
}
