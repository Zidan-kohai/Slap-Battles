using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Enemy : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField] private float speed;
    [SerializeField] private int attackPower;

    [SerializeField] private GameObject enemy;
    [SerializeField] private Vector3 target;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if(enemy == null && (target - transform.position).magnitude < 1f)
        {
            target = new Vector3(Random.Range(transform.position.x - 10, transform.position.x + 10),
                transform.position.y,
                Random.Range(transform.position.x - 10, transform.position.x + 10));
        }

        Vector3 direction = (target - transform.position).normalized;

        characterController.Move(direction * speed * Time.deltaTime);
        transform.forward = new Vector3(direction.x, 0f, direction.z);
    }



}
