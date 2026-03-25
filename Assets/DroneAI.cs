using UnityEngine;

public class DroneAI1 : MonoBehaviour
{
    [Header("Player")]
    public Transform player;

    [Header("Follow Settings")]
    public float followSpeed = 5f;
    public float distanceBehindPlayer = 6f;
    public float fixedHeight = 5f;

    [Header("Shoot Settings")]
    public GameObject rocketPrefab;
    public Transform shootPoint;
    public float fireRate = 5f;

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
        shootTimer += Time.deltaTime;

        if (shootTimer >= fireRate)
        {
            GameObject newRocket = Instantiate(rocketPrefab, shootPoint.position, shootPoint.rotation);

            RocketHoming1 rocketScript = newRocket.GetComponent<RocketHoming1>();
            if (rocketScript != null)
            {
                rocketScript.SetTarget(player);
            }

            shootTimer = 0f;
        }
    }
}