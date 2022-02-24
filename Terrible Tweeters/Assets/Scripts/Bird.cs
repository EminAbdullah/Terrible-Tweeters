using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] float _launchForce = 400; //deðeri unity üzerinden deðiþtirebilmemizi de saðlar
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
    {      //mause nesnenin üzerinde iken rengi kýrmýzý olur
        _spriteRenderer.color = Color.red;
    }

    void OnMouseUp()
    {
        Vector2 currentPosition = _rigidbody2D.position;
        Vector2 direction = _startPosition - currentPosition;
        direction.Normalize();

        _rigidbody2D.isKinematic = false;
        _rigidbody2D.AddForce(direction * _launchForce);

        //mause nesnenin üzerinde deðilken rengi beyaz olur
        _spriteRenderer.color = Color.white;
    }
    void OnMouseDrag()
    {    
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 desiredPosition = mousePosition;
       
        //atýþ halindeyken çekme iþlemini daire haline getirdik
        float distance = Vector2.Distance(desiredPosition, _startPosition);
        if (distance > _maxDragDistance )
        {
            Vector2 direction = desiredPosition - _startPosition;
            direction.Normalize();
            desiredPosition = _startPosition + (direction * _maxDragDistance);
        }
        //kuþun üstünde basýlý tutularak ilerletilmesini önledik ve kuþ geriye atýlamaz artýk
        if (desiredPosition.x > _startPosition.x)
        {
            desiredPosition.x = _startPosition.x;
        }
        _rigidbody2D.position = desiredPosition;

        // kuþun konumunu mause ile deðiþtirirken z sabit kaldý x ve y yi deðiþtirdik
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
