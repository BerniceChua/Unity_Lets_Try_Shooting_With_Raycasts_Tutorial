using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayViewerForDebugging : MonoBehaviour {
    public float m_weaponRange = 50.0f;

    private Camera m_fpsCam;

    // Use this for initialization
    void Start () {
        m_fpsCam = GetComponentInParent<Camera>();
    }

    // Update is called once per frame
    void Update () {
        // origin point for the ray
        // .ViewportToWorldPoint() makes sure that the raycast is ALWAYS at the center of the screen/ first person camera view.
        Vector3 rayOrigin = m_fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

        Debug.DrawRay(rayOrigin, m_fpsCam.transform.forward * m_weaponRange, Color.green);
    }
}
