using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARSubsystems;


public class TapObject : MonoBehaviour
{
    // Start is called before the first frame update
    public ARRaycastManager arRayCastMng;
    public bool canPlace;
    private Pose placementPose;
	public GameObject male;
    public GameObject Roses;
    public bool state = true;
    public static GameObject Rose1;


    
    

    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        UpdatePlacementPose();
        var Touch = Input.touchCount;

        //UpdateTargetIndicator();
        if (canPlace && Touch == 1 && Input.GetTouch(0).phase == TouchPhase.Began && state)
        {
            Quaternion rotation = Quaternion.Euler(0, 180, 0);
            Instantiate(male,placementPose.position, rotation);
            Rose1 = Instantiate(Roses, new Vector3(Random.Range(-0.1f, 0.1f), placementPose.position.y, placementPose.position.z), placementPose.rotation);
            state = false;

        }

    }


    private void UpdatePlacementPose()
    {
        Vector3 screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        arRayCastMng.Raycast(screenCenter, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

        canPlace = hits.Count > 0;

        if (canPlace)
        {
            placementPose = hits[0].pose;

            Vector3 cameraForward = Camera.current.transform.forward;
            Vector3 cameraBearing = new Vector3(cameraForward.x, 0f, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);


        }

    }

	//public void activate()
	//{
	//	Roses.SetActive(false);
	//}



	//private void OnTriggerEnter(Collider other)
	//{
	//    if (other.gameObject.tag == "roses")
	//    {
	//        moveSpeed = 0;
	//        anim.Play("pickup");
	//        Roses.SetActive(false);

	//    }
	//}
}
