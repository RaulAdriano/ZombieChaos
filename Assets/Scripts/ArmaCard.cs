using UnityEngine;
using UnityEngine.UI;

public class ArmaCard : MonoBehaviour
{
    [SerializeField] private Button comprarArmaButton;
    [SerializeField] private Button comprarMunicaoButton;

    [SerializeField] private int valorArma;
    [SerializeField] private int valorMunicao;

    [SerializeField] private ModeloArma modeloArma;
    [SerializeField] private GerenciadorArmas gerenciadorArmas;
    [SerializeField] private GerenciadorLoja gerenciadorLoja;


    private void OnEnable()
    {
        comprarArmaButton.interactable = Jogador.Instance.GetPontos() >= valorArma;   
        comprarMunicaoButton.interactable = Jogador.Instance.GetPontos() >= valorMunicao;   
    }

    public void ComprarArma()
    {
        if (Jogador.Instance.GetPontos() >= valorArma)
        {
            Jogador.Instance.ReduzirPontos(valorArma);
            gerenciadorArmas.EquiparNovaArma(modeloArma);
            gerenciadorLoja.FecharLoja();
        }
    }

    public void ComprarMunicao()
    {
        if (Jogador.Instance.GetPontos() >= valorMunicao)
        {
            Jogador.Instance.ReduzirPontos(valorMunicao);
            gerenciadorArmas.EquiparMunicao(modeloArma);
            gerenciadorLoja.FecharLoja();
        }
    }
}
