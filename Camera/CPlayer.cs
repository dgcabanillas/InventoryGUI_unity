using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayer : MonoBehaviour {

    private float _speed;
    private float _rotateSpeed;
    public Vector3 _direction;

    private Animator anim;
    private BoxCollider[] swordColliders;

    private void Awake() {
        anim = GetComponent<Animator>();
        swordColliders = GetComponentsInChildren<BoxCollider>();
        EndAttack();
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

    public void MakeAction( PLAYER_STATE state ) {
        if( !GameManager.instance.GameOver ) { 
            switch( state ) {
                case PLAYER_STATE.Idle:
                    anim.SetBool("IsWalking", false);
                    resetDirection( PLAYER_STATE.Forward );
                    break;
                case PLAYER_STATE.Forward:
                    anim.SetBool("IsWalking", true);
                    _direction = transform.position;
                    transform.Translate( Vector3.forward * _speed * Time.deltaTime );
                    _direction = Vector3.Normalize( transform.position - _direction );
                    resetDirection( PLAYER_STATE.Forward );
                    break;
                case PLAYER_STATE.Back:
                    anim.SetBool("IsWalking", true);
                    _direction = transform.position;
                    transform.Translate( Vector3.forward * -_speed * Time.deltaTime );
                    _direction = Vector3.Normalize( _direction - transform.position );
                    resetDirection( PLAYER_STATE.Back );
                    break;
                case PLAYER_STATE.Left:
                    transform.Rotate( new Vector3(0, -_rotateSpeed, 0) * Time.deltaTime );
                    rotateDirection( _rotateSpeed * Time.deltaTime ); 
                    break;
                case PLAYER_STATE.Right:
                    transform.Rotate( new Vector3(0, _rotateSpeed, 0) * Time.deltaTime );
                    rotateDirection( -_rotateSpeed * Time.deltaTime ); 
                    break;
                case PLAYER_STATE.Attack_1:
                    anim.Play("DoubleChop");
                    break;
                case PLAYER_STATE.Attack_2:
                    anim.Play("SpinAttack");
                    break;
            }
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

    private void resetDirection( PLAYER_STATE state ) {
        switch( state ) {
            case PLAYER_STATE.Forward:
                _rotateSpeed *= ( _rotateSpeed > 0 ? 1 : -1 );
                break;
            case PLAYER_STATE.Back:
                _rotateSpeed *= ( _rotateSpeed < 0 ? 1 : -1 );
                break;
        }
    }

    public void BeginAttack() {
        foreach (var weapon in swordColliders ) {
            weapon.enabled = true;
        }
    }
    public void EndAttack() {
        foreach (var weapon in swordColliders ) {
            weapon.enabled = false;
        }
    }
}
