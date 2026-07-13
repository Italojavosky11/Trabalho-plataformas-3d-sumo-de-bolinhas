using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    [Header("Bolinhas")]
    public BolinhaData[] bolinhas;

    [Header("Player 1")]
    public Image imagemP1;
    public TMP_Text nomeP1;
    public TMP_Text velocidadeP1;
    public TMP_Text forcaP1;
    public TMP_Text pesoP1;
    public TMP_Text tamanhoP1;
    public GameObject readyP1;

    [Header("Player 2")]
    public Image imagemP2;
    public TMP_Text nomeP2;
    public TMP_Text velocidadeP2;
    public TMP_Text forcaP2;
    public TMP_Text pesoP2;
    public TMP_Text tamanhoP2;
    public GameObject readyP2;

    [HideInInspector]
    public int indiceP1 = 0;

    [HideInInspector]
    public int indiceP2 = 0;

    [HideInInspector]
    public bool confirmouP1 = false;

    [HideInInspector]
    public bool confirmouP2 = false;

    private void Start()
    {
        if (readyP1 != null)
            readyP1.SetActive(false);

        if (readyP2 != null)
            readyP2.SetActive(false);

        AtualizarUI();
    }

    private void Update()
    {
        // ==========================
        // PLAYER 1
        // A = Esquerda
        // D = Direita
        // Espaço = Confirmar
        // ==========================

        if (!confirmouP1)
        {
            if (Input.GetKeyDown(KeyCode.A))
                BolinhaAnteriorP1();

            if (Input.GetKeyDown(KeyCode.D))
                ProximaBolinhaP1();

            if (Input.GetKeyDown(KeyCode.Space))
                ConfirmarP1();
        }

        // ==========================
        // PLAYER 2
        // ← = Esquerda
        // → = Direita
        // Enter = Confirmar
        // ==========================

        if (!confirmouP2)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                BolinhaAnteriorP2();

            if (Input.GetKeyDown(KeyCode.RightArrow))
                ProximaBolinhaP2();

            if (Input.GetKeyDown(KeyCode.Return))
                ConfirmarP2();
        }
    }

    public void AtualizarUI()
    {
        AtualizarPlayer1();
        AtualizarPlayer2();
    }

    void AtualizarPlayer1()
    {
        BolinhaData bola = bolinhas[indiceP1];

        imagemP1.sprite = bola.sprite;
        nomeP1.text = bola.nome;

        velocidadeP1.text = "Speed: " + bola.velocidade;
        forcaP1.text = "Force: " + bola.forca;
        pesoP1.text = "Weight: " + bola.peso;
        tamanhoP1.text = "Size: " + bola.tamanho;
    }

    void AtualizarPlayer2()
    {
        BolinhaData bola = bolinhas[indiceP2];

        imagemP2.sprite = bola.sprite;
        nomeP2.text = bola.nome;

        velocidadeP2.text = "Speed: " + bola.velocidade;
        forcaP2.text = "Force: " + bola.forca;
        pesoP2.text = "Weight: " + bola.peso;
        tamanhoP2.text = "Size: " + bola.tamanho;
    }

    public void ProximaBolinhaP1()
    {
        if (confirmouP1)
            return;

        indiceP1++;

        if (indiceP1 >= bolinhas.Length)
            indiceP1 = 0;

        AtualizarPlayer1();
    }

    public void BolinhaAnteriorP1()
    {
        if (confirmouP1)
            return;

        indiceP1--;

        if (indiceP1 < 0)
            indiceP1 = bolinhas.Length - 1;

        AtualizarPlayer1();
    }

    public void ProximaBolinhaP2()
    {
        if (confirmouP2)
            return;

        indiceP2++;

        if (indiceP2 >= bolinhas.Length)
            indiceP2 = 0;

        AtualizarPlayer2();
    }

    public void BolinhaAnteriorP2()
    {
        if (confirmouP2)
            return;

        indiceP2--;

        if (indiceP2 < 0)
            indiceP2 = bolinhas.Length - 1;

        AtualizarPlayer2();
    }

    public void ConfirmarP1()
    {
        confirmouP1 = true;

        if (readyP1 != null)
            readyP1.SetActive(true);

        VerificarInicio();
    }

    public void ConfirmarP2()
    {
        confirmouP2 = true;

        if (readyP2 != null)
            readyP2.SetActive(true);

        VerificarInicio();
    }

    void VerificarInicio()
    {
        if (confirmouP1 && confirmouP2)
        {
            GameManager.Instance.bolinhaP1 = bolinhas[indiceP1];
            GameManager.Instance.bolinhaP2 = bolinhas[indiceP2];

            Debug.Log("Jogador 1: " + GameManager.Instance.bolinhaP1.nome);
            Debug.Log("Jogador 2: " + GameManager.Instance.bolinhaP2.nome);

            // Troque pelo nome exato da sua cena de Gameplay
            SceneManager.LoadScene("Gameplay");
        }
    }
}