using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject collisionVisualFx;
    [SerializeField] Sprite[] hitSprites;

    [SerializeField] int timesHit; // serialised for debug purposes
    Level level;

    private void IfBreakable(Action action)
    {
        if (tag == "Breakable")
            action();
    }

    public void Start()
    {
        level = FindObjectOfType<Level>();
        IfBreakable(() => level.IncrementBreakableBlocks());
    }
    private void OnCollisionEnter2D(Collision2D collision) =>
        IfBreakable(() =>
        {
            timesHit++;
            if(timesHit >= hitSprites.Length)
                DestroyBlock();
            else 
                ShowNextHitSprite();
        });

    private void ShowNextHitSprite()
    {
        GetComponent<SpriteRenderer>().sprite = hitSprites[Math.Min(timesHit - 1, hitSprites.Length)];
    }

    private void DestroyBlock()
    {
        FindObjectOfType<GameSession>().AddToScore();
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        Destroy(gameObject);
        level.DecrementBreakableBlocks();
        TriggerBlockDamageVfx();
    }

    private void TriggerBlockDamageVfx()
    {
        GameObject collisionFx = Instantiate(collisionVisualFx, transform.position, transform.rotation);
        Destroy(collisionFx, 1f);
    }
}
