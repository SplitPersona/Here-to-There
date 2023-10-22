using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Throwable : MonoBehaviour
{
    Vector3 throwVector;
    Rigidbody2D _rb;
    LineRenderer _lr;
    private Animator anim; 

    void Awake()
    {
        _rb = this.GetComponent<Rigidbody2D>();
        _lr = this.GetComponent<LineRenderer>();
        anim = GetComponent<Animator>();
    }
    //onmouse events possible thanks to monobehaviour + collider2d
    void OnMouseDown()
    {
        CalculateThrowVector();
        SetArrow();
    }
    void OnMouseDrag()
    {
        CalculateThrowVector();
        SetArrow();        
    }
    void CalculateThrowVector()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //doing vector2 math to ignore the z values in our distance.
        Vector2 distance = mousePos - this.transform.position;
        //dont normalize the ditance if you want the throw strength to vary
        throwVector = -distance*200;     
    }
    void SetArrow()
    {
        _lr.positionCount = 2;
        _lr.SetPosition(0,Vector3.zero);
        _lr.SetPosition(1,throwVector.normalized/2);
        _lr.enabled = true;
    }
    void OnMouseUp()
    {
        RemoveArrow();
        Throw();
    }
    void RemoveArrow()
    {
        _lr.enabled = false;
    }
    public void Throw()
    {
        _rb.AddForce(throwVector);
    }
}