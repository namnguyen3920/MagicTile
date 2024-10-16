using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    private Animator animator;
    private bool visible;
    [SerializeField]
    protected float noteSpeed = 5f;

    [SerializeField]
    public bool isVisible
    {
        get => visible;
        set
        {
            visible = value;
            //if (!visible) { animator.Play("Invisible"); }
        }
    }

    private void Awake()
    {
        //animator = GetComponent<Animator>();
    }

    private void Update ()
    {
        transform.Translate(Vector2.down * noteSpeed * Time.deltaTime);
    }

    public bool Played { get; private set; }
    public int ID { get; set; }
}
