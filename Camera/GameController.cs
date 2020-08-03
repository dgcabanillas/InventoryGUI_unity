using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public enum PLAYER_STATE {
    Idle,
    Left,
    Right,
    Forward,
    Back,
    Rotate,
    Attack_1,
    Attack_2
}

public class GameController : MonoBehaviour {

    [SerializeField] private GameObject playerObject;
    private CPlayer player;
    private Vector3 posCamera;
    private Vector3 rotCamera;
    private float distance;
    private float yPosition;

    void Awake()
    {
        // Iniciamos datos de player
        Assert.IsNotNull( playerObject );
        playerObject.transform.position = new Vector3(-10f, 0, -2);
        playerObject.transform.eulerAngles = Vector3.zero;
        player = playerObject.GetComponent<CPlayer>();
        player.Direction = Vector3.forward;
        player.Speed = 5f;
        player.RotateSpeed = 120f;

        // Iniciamos la cámara
        distance = 8f;
        yPosition = 6f;
        transform.eulerAngles = new Vector3( 20f, 0, 0 );
        PositionCamera();
    }

    void Update() {
        if( Input.GetKey( KeyCode.W ) ) {
            player.MakeAction( PLAYER_STATE.Forward );
            PositionCamera();
        } else if( Input.GetKey( KeyCode.S ) ) {
            player.MakeAction( PLAYER_STATE.Back );
            PositionCamera();
        } else {
            player.MakeAction( PLAYER_STATE.Idle );
        }

        if( Input.GetKey( KeyCode.A ) ) {
            player.MakeAction( PLAYER_STATE.Left );
            PositionCamera();
        } else if ( Input.GetKey( KeyCode.D ) ) {
            player.MakeAction( PLAYER_STATE.Right );
            PositionCamera();
        }

        if( Input.GetKey( KeyCode.J ) ) {
            player.MakeAction( PLAYER_STATE.Attack_1 );
        } else if ( Input.GetKey( KeyCode.L ) ) {
            player.MakeAction( PLAYER_STATE.Attack_2 );
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
