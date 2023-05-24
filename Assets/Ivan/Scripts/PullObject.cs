using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullObject : MonoBehaviour
{

    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public LayerMask whatIsNotGrappleable;
    public Transform gunTip, player;
    [SerializeField] private float maxDistance = 100f;
    private SpringJoint joint;

    [SerializeField] private float pullSpeed;
    private GameObject hookedObject;
    private GameObject hookedWall;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        RaycastTeST();
        if (Input.GetMouseButtonDown(0))
        {
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopGrapple();
        }

        HookingBitches();
        
    }

    void HookingBitches()
    {
        if (hookedObject != null&&hookedObject.CompareTag("PullableObjects"))
        {
            var step = pullSpeed * Time.deltaTime;
            hookedObject.transform.position = Vector3.MoveTowards(hookedObject.transform.position, gunTip.transform.position, step);
        }
        else if (hookedWall != null&&hookedObject.CompareTag("Heavy"))
        {
            var step = pullSpeed * Time.deltaTime;
            player.transform.position = Vector3.MoveTowards(player.transform.position, hookedObject.transform.position, step);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="PullableObjects")
        {
            Debug.Log("Yeah");
            //hookedObject = null;
            StopGrapple();
        }
    }

    private void LateUpdate()
    {
        DrawRope();
    }

    void StartGrapple()
    {
        RaycastHit hit;
        Vector3 forward = gunTip.TransformDirection(Vector3.forward) * 10;
        if (Physics.Raycast(gunTip.position, forward, out hit, maxDistance, whatIsGrappleable))
        {          
            Debug.DrawRay(transform.position, forward, Color.green);
            hookedObject = hit.transform.gameObject;
            
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;


            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            //Distancia que el grapple tendrá del jugador con el origen del grapple
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            lr.positionCount = 2;
            currentGrapplePosition = grapplePoint;
        }     
    }

    void RaycastTeST()
    {
        RaycastHit hit;
        Vector3 forward = gunTip.TransformDirection(Vector3.forward) * 10;
        if (Physics.Raycast(gunTip.position, forward, out hit, maxDistance, whatIsGrappleable))
        {
           // Debug.Log("Le di");
            
            Debug.DrawRay(transform.position, forward, Color.green);
        }
        else
        {
          
            Debug.DrawRay(transform.position, forward, Color.red);
        }
    }

    private Vector3 currentGrapplePosition;

    //Stop right there
    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
        hookedObject = null;
    }


    void DrawRope()
    {
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);

        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, grapplePoint);
    }

    public bool IsGrappling()
    {
        return joint != null;
    }

    public Vector3 GrapplePoint()
    {
        return grapplePoint;
    }
}
