using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    
    public BolinhaData bolinhaP1;
    public BolinhaData bolinhaP2;

    
    public int vitoriasP1 = 0;
    public int vitoriasP2 = 0;

    
    public int vencedor = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ReiniciarPartida()
    {
        vitoriasP1 = 0;
        vitoriasP2 = 0;
        vencedor = 0;

        bolinhaP1 = null;
        bolinhaP2 = null;
    }
}