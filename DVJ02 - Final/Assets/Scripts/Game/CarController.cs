using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CarController : MonoBehaviour
{
    public delegate void OnItemCollected();
    public static OnItemCollected CollectItem;

    public delegate void OnCarDeath();
    public static OnCarDeath CarDie;

    public float speed;
    public float rotationSpeed;
    public int rayDistance;
    public bool shallMove;

    public Transform limit;

    Rigidbody rig;
    public Transform floorCheckerOrigin;

    public GameObject target;

    Vector3 nextPosition;

    public TerrainData td;

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        shallMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit raycastHit;

            Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.yellow);

            if (Physics.Raycast(ray.origin, ray.direction, out raycastHit, rayDistance, 1))
            {
                nextPosition = raycastHit.point;
                target.transform.position = nextPosition;
                target.transform.rotation = Quaternion.identity;
                shallMove = true;
            }
        }

        if(transform.position.x == nextPosition.x && transform.position.z == nextPosition.z)
        {
            shallMove = false;
        }

        Vector3 direction = (nextPosition - transform.position).normalized;

        Quaternion finalRot = Quaternion.LookRotation(direction, td.GetInterpolatedNormal(transform.position.x / 500, transform.position.z / 500));

        RaycastHit floorHit;

        if (shallMove)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, finalRot, rotationSpeed / 100 * Time.deltaTime);
            transform.position += transform.forward * speed * Time.deltaTime;
            Debug.DrawRay(floorCheckerOrigin.position, Vector3.down*5f);
            
        }

        if(transform.position.y<limit.position.y)
        {
            if(CarDie!=null)
                CarDie();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.transform.tag == target.tag)
        {
            shallMove = false;
        }
        else if(collider.transform.tag == "item")
        {
            if(CollectItem != null)
                CollectItem();
        }
    }
}
