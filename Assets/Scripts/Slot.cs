using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        GameManager.instance.OnPlay += Event_OnPlay;
    }

    private void Event_OnPlay(object sender, EventArgs e)
    {
        spriteRenderer.sprite = sprites[GameManager.instance.nums[transform.GetSiblingIndex()]];
    }
}
