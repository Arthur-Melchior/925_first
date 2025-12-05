using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.VFX;

public class TowerScript : MonoBehaviour
{
    [SerializeField] private float detectionRange;
    [SerializeField] private float fireTime;
    [SerializeField] private float waitTime;
    [SerializeField] private float dps;
    [SerializeField] private Transform playerPosition;
    [SerializeField] private Transform barrel;
    [SerializeField] private float lerpCompensation;

    [SerializeField] private LayerMask layers;
    [SerializeField] private LineRenderer laserLine;
    [SerializeField] private float blinkSpeed = 0.1f;
    [SerializeField] private GameObject implosion;
    [SerializeField] private GameObject bullet;


    private bool _doShoot = false;
    private float time = 0;
    private bool _lookAtPlayer = true;
    private bool _shooting = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!playerPosition)
        {
            playerPosition = GameObject.FindWithTag("Player").transform;
        }

        if (laserLine) laserLine.enabled = false;
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (playerPosition)
        {
            var playerDirection = playerPosition.position - barrel.position;

            if (playerDirection.magnitude < detectionRange)
            {
                if (_lookAtPlayer)
                {
                    barrel.LookAt(playerPosition);
                }
                
                if (laserLine)
                {
                    laserLine.enabled = true;
                    laserLine.SetPosition(0, barrel.position);
                    laserLine.SetPosition(1, barrel.position + barrel.forward * 50);
                }

                time += Time.deltaTime;


                if (!(time > 3f) || _shooting) return;
                
                _shooting = true;

                if (laserLine) laserLine.enabled = false;

                _lookAtPlayer = false;
                
                var fx = Instantiate(implosion, barrel.position, barrel.rotation);
                Destroy(fx, 1f);

                StartCoroutine(ShootSequence_co());
            }
            else
            {
                StopAllCoroutines();
                _shooting = false;
                time = 0f;
                _lookAtPlayer = true;

                if (laserLine) laserLine.enabled = false;

                // Retour orientation par défaut
                barrel.rotation = Quaternion.Lerp(
                    barrel.rotation,
                    Quaternion.LookRotation(Vector3.forward),
                    lerpCompensation * Time.deltaTime
                );
            }
        }
        else
        {
            StopAllCoroutines();
        }
    }

    private IEnumerator ShootSequence_co()
    {
        yield return new WaitForSeconds(1f);
        
        var bu = Instantiate(bullet, barrel.position, barrel.rotation);
        Destroy(bu,5f);
        
        time = 0;
        _lookAtPlayer = true;
        _shooting = false;
        
        yield return new WaitForSeconds(1f);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(barrel.position, detectionRange);
    }

    private void DoLaserShoot()
    {
        if (Physics.Raycast(barrel.position, barrel.forward, out var hit, Mathf.Infinity, layers))
        {
            Debug.Log("Hit something !!!! " + hit.collider.gameObject.name);
            // if(hit.collider.CompareTag("Tank"))
            // GameObject damageTaker;
            // if (!hit.collider.gameObject.TryGetComponent(out damageTaker)) return;
            // damageTaker.TakeDamages(_dps * Time.deltaTime);
            Debug.DrawRay(barrel.position, barrel.forward * 100, Color.green, 0.25f);
        }
        else
        {
            Debug.DrawRay(barrel.position, barrel.forward * 100, Color.red, 0.25f);
        }

        _lookAtPlayer = true;
    }


    private IEnumerator BlinkWarning()
    {
        float elapsed = 0f;

        while (elapsed < 1f)
        {
            elapsed += blinkSpeed;

            if (laserLine)
            {
                laserLine.enabled = !laserLine.enabled;
                laserLine.SetPosition(0, barrel.position);
                laserLine.SetPosition(1, barrel.position + barrel.forward * 50);
            }

            yield return new WaitForSeconds(blinkSpeed);
        }

        if (laserLine) laserLine.enabled = false;
    }
}