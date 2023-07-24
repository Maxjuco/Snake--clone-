using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manageSprit : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeSprite(Sprite spriteToDisplay)
    {
        this.spriteRenderer.sprite = spriteToDisplay;
    }
}
