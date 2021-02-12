using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    private Vector2 touchPosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);
    private Vector2 bowPosition => transform.position;
    private Vector2 direction => (touchPosition - bowPosition) * (-1);
    private Transform _transformArrow => transform.GetChild(0).GetChild(0).transform;
//physics
    public static bool isArrow = true;
    public static bool isMouseDown = true;

    private Rigidbody2D _rigidbody;
//creating direction pointss
    private GameObject[] _arrayTrajectoryPoints;
    public GameObject trajectoryPoint;
    public GameObject ArrowPrefab;
//pulling the bow
    public float forceRepulsion;

    public int pointCount;
    //audio 
    public AudioSource BowEffect;
    private void Awake() => InstantiateArrow(transform.GetChild(0).transform);

    private void Start()
    {
        _rigidbody = transform.GetChild(0).GetChild(0).GetComponent<Rigidbody2D>();

        _arrayTrajectoryPoints = new GameObject[pointCount];
      
        
    }
    
    private void Update()
    //its all about physics and what will happen if we ht some thing
    {
        if (GameController.isGame)
        {
            if (!isMouseDown)
            {
                isMouseDown = true;
                InstantiateArrow(transform.GetChild(0).transform);
            }

            if (!isArrow)
            {
                _rigidbody = transform.GetChild(0).GetChild(0).GetComponent<Rigidbody2D>();
                Vector2 directionRigidbody = _rigidbody.velocity;

                float angle = Mathf.Atan2(directionRigidbody.x, directionRigidbody.y) * Mathf.Rad2Deg * (-1);
                _transformArrow.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }

    private void OnMouseDown()
    {
        if(GameController.isGame)
        {
            transform.right = direction;
            for (int i = 0; i < pointCount; i++) _arrayTrajectoryPoints[i] = Instantiate(trajectoryPoint, transform);
        }
    }

    private void OnMouseDrag()
    {
        if (GameController.isGame)
        {
            transform.right = direction;
            for (int i = 0; i < pointCount; i++) _arrayTrajectoryPoints[i].transform.position = CalculatePointPosition(i * 0.08f);
        }
    }

    private void OnMouseUp()
    {
        if (GameController.isGame)
        {
            isArrow = false;

            _rigidbody = transform.GetChild(0).GetChild(0).GetComponent<Rigidbody2D>();
            _rigidbody.gravityScale = 1;
            _rigidbody.velocity = new Vector2(direction.x * forceRepulsion, direction.y * forceRepulsion);
            _transformArrow.GetComponent<PolygonCollider2D>().enabled = true;
//destruction points after hit
            for (int i = 0; i < pointCount; i++) Destroy(_arrayTrajectoryPoints[i]);
              //audio 
        BowEffect.Play();
        }
    }

//the arro lives the bo then we turn off the box colider
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Arrow")) PolygonTriggerActive();
    }

    private Vector2 CalculatePointPosition(float elapsedTime)
    {
        return new Vector2(touchPosition.x, touchPosition.y) + new Vector2(direction.x * forceRepulsion, direction.y * forceRepulsion) * elapsedTime + 0.5f * Physics2D.gravity * elapsedTime * elapsedTime * elapsedTime * elapsedTime;
    }

    private void PolygonTriggerActive() => _transformArrow.GetComponent<PolygonCollider2D>().isTrigger = false;

    private void InstantiateArrow(Transform transform) => Instantiate(ArrowPrefab, transform);
}
