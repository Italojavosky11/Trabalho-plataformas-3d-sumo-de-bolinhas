using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    
    public float velocidade = 6f;

    
    public float forcaBase = 8f;
    public float distanciaMaxima = 4f;

    
    public Transform jogadorInimigo;

    private Rigidbody rb;
    private Vector2 movimento;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
            Mathf.Lerp(forcaBase * 2f,
                       forcaBase,
                       distancia / distanciaMaxima);

        rbInimigo.AddForce(
            direcao * intensidade,
            ForceMode.Impulse
        );
    }
}
