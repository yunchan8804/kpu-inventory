using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 2f;

    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        transform.Translate(horizontal * Time.deltaTime * speed, 
            vertical * Time.deltaTime * speed, transform.position.z);
        _animator.SetFloat("faceX", horizontal);
        _animator.SetFloat("faceY", vertical);

        if (horizontal > 0) transform.localScale = new Vector3(-1, 1, 1);
        else transform.localScale = new Vector3(1, 1, 1);
    }
}
