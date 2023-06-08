//<<<<<<< Updated upstream
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullObject : MonoBehaviour
{
    [Header("References")]
    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public LayerMask whatIsNotGrappleable;
    public Transform orientation, player;
    public GameObject Player;
    // private PlayerController2 pc;
    private NewSuperPlayerM tp;
   


    [Header("Grappling")]
    [SerializeField] private float maxDistance = 100f;
    [SerializeField] private float pullSpeed;
    [SerializeField] private float overShootYAxis;
    

    [Header("Grappled")]
    private SpringJoint hookJoint;
    private GameObject hookedObject;

    [Header("Cooldown")]
    public float grapplingCd;
    private float grapplingCdTimer;
    [SerializeField] private float grappleDelayTime;

    [Header("ListOfEffects")]
    public ParticleSystem[] Effects;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        Player = GameObject.FindGameObjectWithTag("Player");
        tp = Player.GetComponent<NewSuperPlayerM>();

    }

    private void Update()
    {
        //RaycastTeST();
        
        if (Input.GetMouseButtonDown(0)) StartGrapple();
        else if (Input.GetMouseButtonUp(0)) StopGrapple();

        HookingBitches();

        //Esto sirve para darle un Timer al jugador para que pueda volver a lanzar el GrappleHook
        if (grapplingCdTimer > 0)
            grapplingCdTimer -= Time.deltaTime;
        
          
    }

    private void LateUpdate()
    {
        DrawRope();

        if (hookedObject != null)
            lr.SetPosition(0, orientation.position);
    }


    void HookingBitches()
    {
        if (hookedObject != null&&hookedObject.CompareTag("PullableObjects"))
        {
            var step = pullSpeed * Time.deltaTime;
            hookedObject.transform.position = Vector3.MoveTowards(hookedObject.transform.position, orientation.transform.position, step);
        }

        else if(hookedObject!=null&&hookedObject.CompareTag("Heavy"))
        {
            Invoke(nameof(ExecuteGrapple), grappleDelayTime);
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="PullableObjects")
        {
            Debug.Log("Yeah");
            StopGrapple();
        }

        if(other.gameObject.tag=="Meat")
        {
            Effects[0].Play();
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag=="Meat2")
        {
            Effects[1].Play();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Meat3")
        {
            Effects[2].Play();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Meat4")
        {
            Effects[3].Play();
            Destroy(other.gameObject);
        }
    }

    

    void StartGrapple()
    {
        if (grapplingCdTimer > 0) return; //Si el cooldown esta activo no se puede hacer Grappler 

        tp.Freeze = true;
    

        RaycastHit hit;
        Vector3 forward = orientation.TransformDirection(Vector3.forward) * 10;
        if (Physics.Raycast(orientation.position, orientation.forward, out hit, maxDistance, whatIsGrappleable))
        {
            // Debug.DrawRay(transform.position, forward, Color.green);
            hookedObject = hit.transform.gameObject;

            grapplePoint = hit.point;
            hookJoint = player.gameObject.AddComponent<SpringJoint>();
            hookJoint.autoConfigureConnectedAnchor = false;
            hookJoint.connectedAnchor = grapplePoint;




            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            //Distancia que el grapple tendrá del jugador con el origen del grapple
            hookJoint.maxDistance = distanceFromPoint * 0.8f;
            hookJoint.minDistance = distanceFromPoint * 0.25f;

            lr.positionCount = 2;
            currentGrapplePosition = grapplePoint;
        }
        /*else
        {
            grapplePoint = gunTip.position + gunTip.forward * maxDistance;

            Invoke(nameof(StopGrapple), grappleDelayTime);
        } */

    }

    void ExecuteGrapple()
    {
        tp.Freeze = false;

        Vector3 lowestPoint = new Vector3(Player.transform.position.x, transform.position.y - 1f, transform.position.z);

        float grapplePointRelativeYPos = grapplePoint.y - lowestPoint.y;
        float highestPointOnArc = grapplePointRelativeYPos + overShootYAxis;

        if (grapplePointRelativeYPos < 0) highestPointOnArc = overShootYAxis;

        tp.JumpToBitches(grapplePoint, highestPointOnArc);

        Invoke(nameof(StopGrapple), 1f);
    }

   
    /*
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
    }*/

    

    private Vector3 currentGrapplePosition;

    //Stop right there
    public void StopGrapple()
    {

        lr.positionCount = 0;
        Destroy(hookJoint);
        hookedObject = null;
        
        tp.Freeze = false;
        
        grapplingCdTimer = grapplingCd;
    }

    void DrawRope()
    {
        if (!hookJoint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);

        lr.SetPosition(0, orientation.position);
        lr.SetPosition(1, grapplePoint);
    }

    public Vector3 GrapplePoint()
    {
        return grapplePoint;
    }
}

