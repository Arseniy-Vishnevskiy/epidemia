using UnityEngine;

public class SavePoint : MonoBehaviour
{
    [SerializeField]
    public Transform player;
    bool isActive = false;
    bool outSP = false;

    public Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(player != null)
           player.GetComponent<PlayerController>().SetLastSavePoint(transform);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && !isActive)
        {
            animator.SetBool("isActive", !isActive);
            Debug.Log("Open Capsule");
            outSP = true;
            isActive = true;
            player.position = new Vector3(player.position.x, player.position.y, 0.02014181f);
        }else if(Input.GetKeyUp(KeyCode.E) && isActive)
        {
            player.position = new Vector3(player.position.x, player.position.y, transform.position.z);
            isActive = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RespawnManager.Instance.SetRespawnPoint(transform.position);
        }
        animator.SetTrigger("intoSP");
        animator.SetBool("isActive", isActive);
    }
    //{
    //            player.transform.position = new Vector3(player.position.x, player.position.y, player.position.z + 0.02014181f);
    //        }
}
