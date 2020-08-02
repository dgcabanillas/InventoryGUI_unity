using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _Rueda;

    private float _speed;
    private float _rotateSpeed;
    public Vector3 _direction;
    private float _angleRotation;

    private void Awake() {
        _angleRotation = 40f;
    }

    public float Speed {
        get { return _speed; }
        set { _speed = value; }
    }
    public float RotateSpeed {
        get { return _rotateSpeed; }
        set { _rotateSpeed = value; }
    }
    public Vector3 Direction {
        get { return _direction; }
        set { _direction = value; }
    }

    public void MakeAction( STATE state ) {
        switch( state ) {
            case STATE.Move_along:
                _direction = transform.position;
                transform.Translate( Vector3.forward * _speed * Time.deltaTime );
                _direction = Vector3.Normalize( transform.position - _direction );
                // movimiento de la rueda
                _Rueda.transform.Rotate( Vector3.forward * ( _speed * _angleRotation ) * Time.deltaTime );
                break;
            case STATE.Move_back:
                _direction = transform.position;
                transform.Translate( Vector3.forward * -_speed * Time.deltaTime );
                _direction = Vector3.Normalize( _direction - transform.position );
                // movimiento de la rueda
                _Rueda.transform.Rotate( Vector3.forward * ( -_speed * _angleRotation ) * Time.deltaTime );
                break;
            case STATE.Rotate_left:
                transform.Rotate( new Vector3(0, -_rotateSpeed, 0) * Time.deltaTime );
                rotateDirection( _rotateSpeed * Time.deltaTime ); 
                break;
            case STATE.Rotate_right:
                transform.Rotate( new Vector3(0, _rotateSpeed, 0) * Time.deltaTime );
                rotateDirection( -_rotateSpeed * Time.deltaTime ); 
                break;
        }
    } 

    private void rotateDirection( float theta ) {
        float _angle = theta * Mathf.PI / 180f;
        float cosTheta = Mathf.Cos( _angle );
        float sinTheta = Mathf.Sin( _angle );   
        Vector3 newDirection = Vector3.zero;
        newDirection.x = _direction.x * cosTheta - _direction.z * sinTheta;
        newDirection.z = _direction.z * cosTheta + _direction.x * sinTheta;
        _direction = newDirection;
    }
}
