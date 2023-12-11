using UnityEngine;
using UnityEngine.Playables;

public class TriggerEvent : MonoBehaviour
{
    [SerializeField] private PlayableDirector enemyCutscene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<PlayerController>() != null)
        {
            Debug.Log("Triggered!");
            enemyCutscene.Play();
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
