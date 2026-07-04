using UnityEngine;

[CreateAssetMenu(menuName="Bolinhas/Bolinha")]
public class BolinhaData : ScriptableObject
{
    public string nome;

    public Sprite sprite;

    public float velocidade;

    public float forca;

    public float peso;

    public float tamanho;
}