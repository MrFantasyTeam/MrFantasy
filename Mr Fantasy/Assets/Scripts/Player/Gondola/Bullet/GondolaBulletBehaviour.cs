using UnityEngine;

public class GondolaBulletBehaviour : MonoBehaviour
{
    public GameObject player;
    public Animator anim;
    public float speed;
    public float recharge;
    public int num = 0;

    public bool catched;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!catched) Moving();
        else GoBackToPlayer();
    }

    public void Moving()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag.Equals("Enemy"))
        {
            if (!catched)
            {
                catched = true;
                recharge = other.GetComponent<EnemiesGeneralBehaviour>().damage;
                player = GameObject.FindGameObjectWithTag("Player");
                Destroy(other.transform.parent.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag.Equals("MainCamera"))
        {
            gameObject.SetActive(false);
        }
    }

    public void GoBackToPlayer()
    {
        anim.SetBool("Catch", true);
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed/100);
        if(Mathf.Abs(transform.position.x-player.transform.position.x) < 0.05f)
        {
            player.GetComponent<GondolaMovement>().ChangeTransparency(recharge / 100);
            catched = false;
            anim.SetBool("Catch", false);
            gameObject.SetActive(false);
        }
    }
}
