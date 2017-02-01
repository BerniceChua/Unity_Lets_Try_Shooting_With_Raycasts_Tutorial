using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShoot : MonoBehaviour {
    public int m_gunDamage = 1;
    public float m_fireRate = 0.25f;
    public float m_weaponRange = 50.0f;
    public float m_hitForce = 100.0f;
    public Transform m_gunEnd;

    private Camera m_fpsCam;
    private WaitForSeconds m_shotDuration = new WaitForSeconds(0.7f);
    private AudioSource m_gunAudio;
    private LineRenderer m_laserLine;
    private float m_nextFire;

	// Use this for initialization
	void Start () {
        m_laserLine = GetComponent<LineRenderer>();
        m_gunAudio = GetComponent<AudioSource>();
        m_fpsCam = GetComponentInParent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton("Fire1") && Time.time > m_nextFire) {
            m_nextFire = Time.time + m_fireRate;

            StartCoroutine(ShotEffect());

            // origin point for the ray
            // .ViewportToWorldPoint() makes sure that the raycast is ALWAYS at the center of the screen/ first person camera view.
            Vector3 rayOrigin = m_fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

            // holds the info from the ray if it hits a game object w/ collider.
            RaycastHit hit;

            // start & end positions of laser line when player fires.
            m_laserLine.SetPosition(0, m_gunEnd.position); // gives current position in worldspace of the gun object.

            // Physics.Raycast() returns a bool "true" I.I.F. it hits something.
            if (Physics.Raycast(rayOrigin, m_fpsCam.transform.forward, out hit, m_weaponRange)) {
                m_laserLine.SetPosition(1, hit.point);
            } else {
                m_laserLine.SetPosition(0, rayOrigin + (m_fpsCam.transform.forward * m_weaponRange));
            }
        }
    }

    private IEnumerator ShotEffect() {
        m_gunAudio.Play();

        m_laserLine.enabled = true;
        yield return m_shotDuration;
        m_laserLine.enabled = false;
    }

    


}