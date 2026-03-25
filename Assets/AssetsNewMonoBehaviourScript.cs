using UnityEngine;

public class DroneAI : MonoBehaviour
{
    public Transform player;

    [Header("Follow")]
    public float followSpeed = 5f;
    public float distanceBehindPlayer = 6f;
    public float fixedHeight = 5f;

    [Header("Shoot")]
    public GameObject rocketPrefab;
    public Transform shootPoint;
    public float fireRate = 5f;
    public float shootDistance = 15f; // alleen schieten als drone dichtbij is

    private float shootTimer;

    void Update()
    {
        if (player == null) return;

        FollowPlayer();
        ShootRocket();
    }

    void FollowPlayer()
    {
        Vector3 targetPosition = player.position - player.forward * distanceBehindPlayer;
        targetPosition.y = fixedHeight;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            followSpeed * Time.deltaTime
        );

        Vector3 lookTarget = player.position;
        lookTarget.y = transform.position.y;

        transform.LookAt(lookTarget);
    }

    void ShootRocket()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= shootDistance)
        {
            shootTimer += Time.deltaTime;

            if (shootTimer >= fireRate)
            {
                GameObject rocket = Instantiate(rocketPrefab, shootPoint.position, shootPoint.rotation);

                RocketHoming1 rocketScript = rocket.GetComponent<RocketHoming1>();
                if (rocketScript != null)
                {
                    rocketScript.SetTarget(player);
                }

                shootTimer = 0f;
            }
        }
    }
}