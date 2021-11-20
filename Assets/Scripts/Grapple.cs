using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    public Transform cam;
    public Transform point;

    Vector3 grapplePoint;
    SpringJoint joint;
    LineRenderer lr;
    bool grappling=false;

    void Start(){
        lr=GetComponent<LineRenderer>();
    }

    void Update(){
        if(Input.GetMouseButtonDown(0))StartGrapple();
        else if(Input.GetMouseButtonUp(0))StopGrapple();
        if(!grappling)grapplePoint=point.position;

        lr.SetPosition(0,point.position);
        lr.SetPosition(1,grapplePoint);
    }

    void StartGrapple(){
        grappling=true;
        RaycastHit hit;
        if(Physics.Raycast(cam.position,cam.forward,out hit,500f)){
            grapplePoint=hit.point;
            joint=gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor=false;
            joint.connectedAnchor=grapplePoint;

            float dist=Vector3.Distance(transform.position,grapplePoint);

            joint.maxDistance=dist*.7f;
            joint.minDistance=dist*.3f;

            joint.spring=6.5f;
            joint.damper=7f;
            joint.massScale=500f;
        }
    }

    void StopGrapple(){
        grapplePoint=Vector3.zero;
        Destroy(joint);
        grappling=false;
    }
}
