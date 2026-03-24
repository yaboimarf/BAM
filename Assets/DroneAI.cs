using UnityEngine;

public class DroneAI : MonoBehaviour
{
    public Transform player;

    [Header("Follow")]
    public float followSpeed = 5f;
    public float distance = 6f;
    public float fixedHeight = 5f;

    [Header("Shoot")]
    public GameObject rocketPrefab;
    public Transform shootPoint;
    public float fireRate = 5f;

    private float timer;

    void Update()
    {
        if (player == null) return;

        FollowPlayer();
        Shoot();
    }

    void FollowPlayer()
    {
        Vector3 targetPos = player.position - player.forward * distance;
        targetPos.y = fixedHeight;

        transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);

        Vector3 lookTarget = player.position;
        lookTarget.y = transform.position.y;

        transform.LookAt(lookTarget);
    }

    void Shoot()
    {
        timer += Time.deltaTime;

        if (timer >= fireRate)
        {
            GameObject rocket = Instantiate(rocketPrefab, shootPoint.position, shootPoint.rotation);

            // geef target door aan rocket
            RocketHoming homing = rocket.GetComponent<RocketHoming>();
            if (homing != null)
            {
                homing.SetTarget(player);
            }

            timer = 0f;
        }
    }
}