using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public  class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject crate;
    public static Vector3 WindDirection = new Vector3(1, 0, 0);
    public float Timer;
    public float Spawnint = 0.5f;
    public GameObject[] icebergs;
    public CanvasGroup GOPanel;
    public void Update()
    {
        if(Gameover && Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        Timer += Time.deltaTime;
        if(Timer > Spawnint)
        {
            if(Random.Range(0,5) == 0)
            {
                Instantiate(crate, Boat.Instance.transform.position + (Quaternion.AngleAxis(Random.Range(-30f, 30f), Vector3.up) * Boat.Instance.transform.forward * Random.Range(150f, 200f)), Quaternion.identity);
            }
            Timer = 0;
            Instantiate(randomIceberg(), Boat.Instance.transform.position + (Quaternion.AngleAxis(Random.Range(-30f, 30f), Vector3.up) * Boat.Instance.transform.forward  * Random.Range(150f, 200f)), Quaternion.identity);
        }
    }
    public  GameObject randomIceberg()
    {
        return icebergs[Random.Range(0, icebergs.Length - 1)];
    }
    public static bool Gameover = false;
    public static void DisplayGameOver()
    {
        Gameover = true;
        
        instance.GOPanel.alpha = 1;
        instance.GOPanel.GetComponentInChildren<TextMeshProUGUI>().text = $"Game Over!\nDistance Traveled:{Mathf.Round(Boat.Instance.transform.position.magnitude)}\nPress R to restart";
    }
    private void Start()
    {
        instance = this;
    }
}
