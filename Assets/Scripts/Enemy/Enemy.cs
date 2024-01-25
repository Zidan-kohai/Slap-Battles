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
        GetRandomTarget();
    }

    private void Update()
    {
        if(enemy == null && (target - transform.position).magnitude < 1f)
        {
            GetRandomTarget();
        }

        Vector3 direction = (target - transform.position).normalized;
        Move(direction);
        transform.forward = new Vector3(direction.x, 0f, direction.z);
    }


    private void Move(Vector3 direction)
    {
        characterController.Move(direction * speed * Time.deltaTime);
    }

    private void GetRandomTarget()
    {
        target = new Vector3(Random.Range(transform.position.x - 10, transform.position.x + 10),
            transform.position.y,
            Random.Range(transform.position.x - 10, transform.position.x + 10));
    }


}
