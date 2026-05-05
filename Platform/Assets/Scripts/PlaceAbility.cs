using UnityEngine;

public abstract class PlaceAbility : MonoBehaviour
{
    [SerializeField] protected PlayerInput input;
    [SerializeField] protected SpriteRenderer sr;
    [SerializeField] protected int usesLeft;
    protected int maxUses;

    public delegate void ChangeUse(int use);
    public ChangeUse change;

    protected virtual void Start()
    {
        input = GetComponent<PlayerInput>();
        sr = GetComponentInChildren<SpriteRenderer>();
        maxUses = usesLeft;
        GameController.Instance.resetLevel += ResetCount;
        change?.Invoke(usesLeft);
    }
    private void Update()
    {
        if (input.usePressed && usesLeft > 0)
        {
            Do();
            usesLeft--;
            change?.Invoke(usesLeft);
        }
    }
    private void ResetCount()
    {
        usesLeft = maxUses;
        change?.Invoke(usesLeft);
    }
    protected abstract void Do();
}
