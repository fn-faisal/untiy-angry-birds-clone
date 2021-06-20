using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    Vector2 _initialPosition;

    [SerializeField] private int birdSpeed = 100;

    private bool _launched;
    private float _timeSittingAround;

    private void Awake()
    {
        _initialPosition = transform.position;
        
    }

    private void OnMouseDown(){
        GetComponent<LineRenderer>().enabled = true;
        var spriteRenderer = GetComponent<SpriteRenderer>(); 
        spriteRenderer.color = Color.red;   
    }

    private void Update()
    {
        GetComponent<LineRenderer>().SetPosition(1, _initialPosition);
        GetComponent<LineRenderer>().SetPosition(0, transform.position);

        if ( _launched && GetComponent<Rigidbody2D>().velocity.magnitude <= 0)
        {
            _timeSittingAround += Time.deltaTime;
        }

        // TODO replace with var
        if (transform.position.y > 10 ||
            transform.position.y < -10 ||
            transform.position.x > 10 ||
            transform.position.x < -10 ||
            _timeSittingAround >= 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnMouseUp() {
        GetComponent<LineRenderer>().enabled = false;

        var spriteRenderer = GetComponent<SpriteRenderer>(); 
        spriteRenderer.color = Color.white;
        var force = Vector2.Distance(this._initialPosition, new Vector2(transform.position.x, transform.position.y));
        Vector2 directionToInitialPosition = this._initialPosition - new Vector2(transform.position.x, transform.position.y);
        GetComponent<Rigidbody2D>().AddForce(directionToInitialPosition * (birdSpeed * (force / 2)));
        GetComponent<Rigidbody2D>().gravityScale = 1;
        _launched = true;
    }

    private void OnMouseDrag() {
        Vector2 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = newPosition;
    }
}
