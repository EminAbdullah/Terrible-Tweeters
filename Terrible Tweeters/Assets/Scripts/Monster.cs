using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase] // üstünde efekt olan karakterlere daha rahat týklamamýzý saðlar
public class Monster : MonoBehaviour
{
    [SerializeField] Sprite _deadSprite;
    [SerializeField] ParticleSystem _particleSystem;
    bool _hasDied;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (ShouldDieFromCollision(collision))
        {
            StartCoroutine( Die()); // IEnumerator Die() fonksiyonunu bu þekilde çalýþtýrdýk

        }
        
    }
    private bool ShouldDieFromCollision(Collision2D collision)
    {
        if (_hasDied)
        {
            return false;
        }

        Bird bird = collision.gameObject.GetComponent<Bird>();
        if (bird != null) // kuþ yaratýða deðerse yaratýk ölür
        {
            return true;
        }
        if (collision.contacts[0].normal.y <-0.5) //üzerine 0.5 lik mesafeden daha uzaktan bir cisim düþerse ölür
        {
            return true;
        }
        return false;
    }

     
    IEnumerator Die()
    {
        _hasDied = true;
        GetComponent<SpriteRenderer>().sprite = _deadSprite;// karaktere deðince resmini deðiþtirdik(unityden)
        _particleSystem.Play();
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false); // karakter kaybolur
    }
}