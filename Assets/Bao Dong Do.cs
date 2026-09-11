using System;
using UnityEngine;

public class BaoDongDo : MonoBehaviour
{
    [SerializeField] public GameObject triangle;
    [SerializeField] float OnTimer;
    float Timer;
    void Awake()
    {
        triangle.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Timer=OnTimer;
        Player player = other.GetComponent<Player>();
        if(player!=null)
        {
            triangle.SetActive(true);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if(player!=null)
        {
            Timer-=Time.deltaTime;
            if(Timer<=0) triangle.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if(player!=null)
        {
            triangle.SetActive(false);
        }
    }
}
