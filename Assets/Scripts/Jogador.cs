using UnityEngine;

public class Jogador : MonoBehaviour
{
    public static Jogador Instance;

    [SerializeField] private int vidaMaxima = 100;
    private int vidaAtual;
    private bool estaMorto;

    [SerializeField] private int pontos;

    private MovimentoJogador movimentoJogador;
    private GerenciadorArmas gerenciadorArmas;
    [SerializeField] private GameObject cinemachine;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        vidaAtual = vidaMaxima;
        AtualizarBarraVida();
        InterfaceUsuario.Instance.AtualizarPontos(0, pontos);

        movimentoJogador = GetComponent<MovimentoJogador>();
        gerenciadorArmas = GetComponent<GerenciadorArmas>();
    }

    public void ReduzirVida(int valor)
    {
        if(estaMorto) return;

        vidaAtual -= valor;
        AtualizarBarraVida();

        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    private void Morrer()
    {
        estaMorto = true;
        Time.timeScale = 0;
    }

    private void AtualizarBarraVida()
    {
        InterfaceUsuario.Instance.AtualizarBarraVida(vidaAtual,vidaMaxima);
    }

    public void AdicionarPontos(int valor)
    {
        pontos += valor;
        InterfaceUsuario.Instance.AtualizarPontos(valor, pontos);
    }

    public void ReduzirPontos(int valor)
    {
        pontos = Mathf.Max(0, pontos - valor);
        InterfaceUsuario.Instance.AtualizarPontos(-valor , pontos);
    }

    public void PausarJogador()
    {
        movimentoJogador.enabled = false;
        gerenciadorArmas.enabled = false;
        cinemachine.SetActive(false);
    }

    public void RetomarJogador()
    {
        movimentoJogador.enabled = true;
        gerenciadorArmas.enabled = true;
        cinemachine.SetActive(true);
    }

    public int GetPontos()
    {
        return pontos;
    }
}
