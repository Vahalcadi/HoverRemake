public class Player : EntityHover
{
    public bool isShielded;
    public bool isSpedUp;
    public int invisibilityUses;
    public int jumpUses;
    public bool isSlowedDown;
    public int wallUses;

    public static Player Instance { get; private set; }


    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance.gameObject);
        else
            Instance = this;
    }

}
