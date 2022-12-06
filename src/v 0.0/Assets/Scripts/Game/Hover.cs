using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : Singleton<Hover>
{
    // a reference to the icons spriterenderer
    private SpriteRenderer spriteRenderer;

    // a reference to the rangedcheck on the tower
    private SpriteRenderer rangeSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.rangeSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        FollowMouse();
    }

    private void FollowMouse()
    {
        if(spriteRenderer.enabled)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }

    }
    //Activated the hover icon
    public void Activate(Sprite sprite)
    {   
        this.spriteRenderer.sprite = sprite;
        spriteRenderer.enabled = true;
        rangeSpriteRenderer.enabled = true;
    }
    //Deactivates the hover icon
    public void Deactivate()
    {
        spriteRenderer.enabled = false;
        GameManager.Instance.ClickedBtn = null;
        rangeSpriteRenderer.enabled = false;
    }
}
