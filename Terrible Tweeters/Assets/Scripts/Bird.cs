using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] float _launchForce = 400; //de�eri unity �zerinden de�i�tirebilmemizi de sa�lar
    [SerializeField] float _maxDragDistance = 4;

    Vector2 _startPosition;
    Rigidbody2D _rigidbody2D;
    SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _startPosition = _rigidbody2D.position;
        _rigidbody2D.isKinematic = true;
    }
    void OnMouseDown()
    {      //mause nesnenin �zerinde iken rengi k�rm�z� olur
        _spriteRenderer.color = Color.red;
    }

    void OnMouseUp()
    {
        Vector2 currentPosition = _rigidbody2D.position;
        Vector2 direction = _startPosition - currentPosition;
        direction.Normalize();

        _rigidbody2D.isKinematic = false;
        _rigidbody2D.AddForce(direction * _launchForce);

        //mause nesnenin �zerinde de�ilken rengi beyaz olur
        _spriteRenderer.color = Color.white;
    }
    void OnMouseDrag()
    {    
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 desiredPosition = mousePosition;
       
        //at�� halindeyken �ekme i�lemini daire haline getirdik
        float distance = Vector2.Distance(desiredPosition, _startPosition);
        if (distance > _maxDragDistance )
        {
            Vector2 direction = desiredPosition - _startPosition;
            direction.Normalize();
            desiredPosition = _startPosition + (direction * _maxDragDistance);
        }
        //ku�un �st�nde bas�l� tutularak ilerletilmesini �nledik ve ku� geriye at�lamaz art�k
        if (desiredPosition.x > _startPosition.x)
        {
            desiredPosition.x = _startPosition.x;
        }
        _rigidbody2D.position = desiredPosition;

        // ku�un konumunu mause ile de�i�tirirken z sabit kald� x ve y yi de�i�tirdik
       // transform.position = new Vector3( mousePosition.x,mousePosition.y,transform.position.z);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(ResetAfterDelay());
    }

    IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(3);
        _rigidbody2D.position = _startPosition;
        _rigidbody2D.isKinematic = true;
        _rigidbody2D.velocity = Vector2.zero;
    }
}
