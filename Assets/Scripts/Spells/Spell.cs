using UnityEngine;
using UnityEngine.UI;

public class Spell : MonoBehaviour, ICommand
{
    public string spellName;
    public float maxCooldown;
    public float velocity;
    public int damage;
    private const string BOUNDS_TRIGGER_TAG = "BoundsTrigger";
    private Rigidbody rb;
    internal static PoolingManager poolingManager;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        poolingManager = FindObjectOfType<PoolingManager>();
    }
    private void OnEnable()
    {
        rb.velocity = transform.forward * velocity;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(BOUNDS_TRIGGER_TAG))
            LeftBounds();
    }
    protected virtual void LeftBounds()
    {
        gameObject.SetActive(false);
    }


    public virtual void CastSpell(Transform caster)
    {

    }

}
