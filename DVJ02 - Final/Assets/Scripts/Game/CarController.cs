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

    public LayerMask rayMask;

    float yHeight;

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        shallMove = false;
        yHeight = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit raycastHit;

            Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.yellow);

            if (Physics.Raycast(ray.origin, ray.direction, out raycastHit, rayDistance, rayMask))
            {
                nextPosition = raycastHit.point;
                target.transform.position = nextPosition;
                target.transform.rotation = Quaternion.identity;
                //nextPosition.y = yHeight;
                
                
                shallMove = true;
            }
        }

        //Debug.Log(nextPosition);
        Vector3 direction = (nextPosition - transform.position).normalized;

        Vector3 upVec = td.GetInterpolatedNormal(transform.position.x / 500, transform.position.z / 500);

        RaycastHit rh;
        if(Physics.Raycast(floorCheckerOrigin.position, -transform.up, out rh, 100f, rayMask))
        {
            if (rh.transform)
            {
                Debug.Log(rh.normal);
                upVec = rh.normal;
                Debug.DrawRay(floorCheckerOrigin.position, -transform.up * 100f);
            }

        }

        

        Quaternion finalRot = Quaternion.LookRotation(direction,upVec);

        if (shallMove)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, finalRot, rotationSpeed * Time.deltaTime);
            transform.position += transform.forward * speed * Time.deltaTime;
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
