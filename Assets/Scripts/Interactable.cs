using System;
using TMPro;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public abstract class Interactable : MonoBehaviour
{
    public string Text;
    TextMeshPro t;
    GameObject obj;
    private void Start()
    {
        obj  = new GameObject("Text");
        obj.transform.parent = transform;
        t = obj.AddComponent<TextMeshPro>();
        t.text = Text;
        t.color = new Color(0, 0, 0, 1);
        t.transform.position = new Vector3();
            t.fontSize = 5;
    }
    public bool playerInside = false;
    public abstract void OnInteract();  
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            t.color = new Color(0, 0, 0, 0);
            playerInside = true;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            t.color = new Color(0, 0, 0, 1);
            playerInside = false;
        }

    }

    public void Update()
    {

        ChildUpdate();
        if(playerInside )
        {
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                OnInteract();
            }
        }
    }

    public virtual void ChildUpdate()
    {
    }
}
