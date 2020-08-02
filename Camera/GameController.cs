using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public enum STATE {
    Rotate_left,
    Rotate_right,
    Move_along,
    Move_back,
    Shoot
}

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject shooterObject;
    private CPlayer player;
    private CShooter shooter;
    private Vector3 posCamera;
    private Vector3 rotCamera;
    private float distance;
    private float yPosition;

    void Awake()
    {
        // Iniciamos datos de player
        Assert.IsNotNull( playerObject );
        playerObject.transform.position = Vector3.zero;
        playerObject.transform.eulerAngles = Vector3.zero;
        player = playerObject.GetComponent<CPlayer>();
        player.Direction = Vector3.forward;
        player.Speed = 10f;
        player.RotateSpeed = 60f;

        Assert.IsNotNull( shooterObject );
        shooter = shooterObject.GetComponent<CShooter>();

        // Iniciamos la cámara
        distance = 8f;
        yPosition = 6f;
        transform.eulerAngles = new Vector3( 20f, 0, 0 );
        PositionCamera();
    }

    void Update() {
        if( Input.GetKey( KeyCode.W ) ) {
            player.MakeAction( STATE.Move_along );
            PositionCamera();
        } else if( Input.GetKey( KeyCode.S ) ) {
            player.MakeAction( STATE.Move_back );
            PositionCamera();
        } 
        if( Input.GetKey( KeyCode.A ) ) {
            player.MakeAction( STATE.Rotate_left );
            PositionCamera();
        } else if ( Input.GetKey( KeyCode.D ) ) {
            player.MakeAction( STATE.Rotate_right );
            PositionCamera();
        }
        if( Input.GetKey( KeyCode.Space ) ) {
            shooter.MakeAction( STATE.Shoot );
        }

        if( Input.GetKey( KeyCode.UpArrow ) ) transform.Rotate( new Vector3( -0.5f, 0, 0) * 10f * Time.deltaTime );
        if( Input.GetKey( KeyCode.DownArrow ) ) transform.Rotate( new Vector3( 0.5f, 0, 0) * 10f * Time.deltaTime );
    }

    private void PositionCamera() {
        posCamera = playerObject.transform.position - ( player.Direction * distance );
        posCamera.y = yPosition;
        transform.position = posCamera;

        rotCamera = playerObject.transform.eulerAngles;
        rotCamera.x = transform.eulerAngles.x;
        transform.eulerAngles = rotCamera;
    }
}
