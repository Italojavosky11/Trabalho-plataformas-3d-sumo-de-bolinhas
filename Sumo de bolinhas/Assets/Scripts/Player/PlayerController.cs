using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Configuração do Jogador")]
    public bool jogador1; // Marque para o Player 1 e desmarque para o Player 2

    private BolinhaData bolinha;

    [Header("Atributos")]
    public float velocidade = 6f;
    public float forcaBase = 8f;
    public float distanciaMaxima = 4f;

    [Header("Referências")]
    public Transform jogadorInimigo;

    private Rigidbody rb;
    private Vector2 movimento;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (GameManager.Instance != null)
        {
            bolinha = jogador1
                ? GameManager.Instance.bolinhaP1
                : GameManager.Instance.bolinhaP2;

            if (bolinha != null)
            {
                velocidade = bolinha.velocidade;
                forcaBase = bolinha.forca;

                rb.mass = bolinha.peso;

                transform.localScale = Vector3.one * bolinha.tamanho;

                Renderer renderer = GetComponent<Renderer>();

                if (renderer != null)
                    renderer.material = bolinha.material;
            }
        }
    }

    public void OnMove(InputValue value)
    {
        movimento = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector3 direcao = new Vector3(movimento.x, 0, movimento.y);

        rb.MovePosition(
            rb.position +
            direcao * velocidade * Time.fixedDeltaTime
        );
    }

    public void OnPush()
    {
        if (jogadorInimigo == null)
            return;

        Rigidbody rbInimigo = jogadorInimigo.GetComponent<Rigidbody>();

        Vector3 direcao =
            (jogadorInimigo.position - transform.position).normalized;

        float distancia =
            Vector3.Distance(transform.position, jogadorInimigo.position);

        if (distancia > distanciaMaxima)
            return;

        float intensidade =
            Mathf.Lerp(
                forcaBase * 2f,
                forcaBase,
                distancia / distanciaMaxima
            );

        rbInimigo.AddForce(
            direcao * intensidade,
            ForceMode.Impulse
        );
    }
}