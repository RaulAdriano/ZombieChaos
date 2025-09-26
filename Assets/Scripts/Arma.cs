using UnityEngine;

public class Arma : MonoBehaviour
{

    public int tirosPorSegundo;
    public int capacidadePente;
    public int municaoNoInventario;
    public int quantidadeMaximaMunicaoInventario;
    public int municaoAtual;

    public ParticleSystem efeitoDisparo;

    [SerializeField] private Vector2[] padraoRecoil;
    private int indiceRecoil;

    public int danoBaixo;
    public int danoMedio;
    public int danoAlto;
    public int distanciaParaDanoMaximo;

    [Range(0f, 1f)]
    public float multiplicadorDanoReduzido;

    public ModeloArma ModeloArma;
    public Animator animator;

    public float tempoDelayRecarregar;

    private void Awake()
    {
        municaoAtual = capacidadePente;
        animator = GetComponent<Animator>();
    }


    public Vector2 ObterRecoilAtual()
    {
        return padraoRecoil[indiceRecoil];
    }

    public void ProximoRecoil()
    {
        indiceRecoil++;

        if(indiceRecoil >= padraoRecoil.Length)
        {
            indiceRecoil = 0;
        }
    }

    public void RealizarDisparo()
    {
        municaoAtual--;
        animator.SetTrigger("Atirar");
        efeitoDisparo.Play();
    }

    public void RecarregarArma(int quantidade)
    {
        municaoAtual += quantidade;
        municaoNoInventario -= quantidade;
    }

    public void AlterarMira()
    {
        bool miraAtiva = animator.GetBool("Mirar");
        animator.SetBool("Mirar", !miraAtiva);

        InterfaceUsuario.Instance.ExibirMira(miraAtiva);
    }

    public void CarregarInventario()
    {
        municaoNoInventario = quantidadeMaximaMunicaoInventario;
    }

    public int GetDano(float distancia, NivelDano nivelDano)
    {
        int dano = 0;

        switch (nivelDano)
        {
            case NivelDano.BAIXO:
                dano = danoBaixo;
                break;
            case NivelDano.MEDIO:
                dano = danoMedio;
                break;
            case NivelDano.ALTO:
                dano = danoAlto;
                break;
        }

        if(distancia > distanciaParaDanoMaximo)
        {
            dano = (int)(dano * multiplicadorDanoReduzido);
        }

        return dano;

    }
}


public enum ModeloArma
{
    PISTOLA,
    SHOTGUN,
    M4A1,
    SMG45,
    AK47,
    LMG,
}
