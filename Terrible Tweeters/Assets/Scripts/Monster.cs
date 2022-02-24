using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase] // �st�nde efekt olan karakterlere daha rahat t�klamam�z� sa�lar
public class Monster : MonoBehaviour
{
    [SerializeField] Sprite _deadSprite;
    [SerializeField] ParticleSystem _particleSystem;
    bool _hasDied;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (ShouldDieFromCollision(collision))
        {
            StartCoroutine( Die()); // IEnumerator Die() fonksiyonunu bu �ekilde �al��t�rd�k

        }
        
    }
    private bool ShouldDieFromCollision(Collision2D collision)
    {
        if (_hasDied)
        {
            return false;
        }

        Bird bird = collision.gameObject.GetComponent<Bird>();
        if (bird != null) // ku� yarat��a de�erse yarat�k �l�r
        {
            return true;
        }
        if (collision.contacts[0].normal.y <-0.5) //�zerine 0.5 lik mesafeden daha uzaktan bir cisim d��erse �l�r
        {
            return true;
        }
        return false;
    }

     
    IEnumerator Die()
    {
        _hasDied = true;
        GetComponent<SpriteRenderer>().sprite = _deadSprite;// karaktere de�ince resmini de�i�tirdik(unityden)
        _particleSystem.Play();
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false); // karakter kaybolur
    }
}