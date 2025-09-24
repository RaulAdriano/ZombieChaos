using UnityEngine;

public class GerenciadorLoja : MonoBehaviour
{
    private bool estaNaAreaCompra;
    [SerializeField] private GameObject tooltipAbrirLoja;
    [SerializeField] private GameObject lojaUI;
    private bool lojaAberta;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Loja"))
        {
            estaNaAreaCompra = true;
            tooltipAbrirLoja.SetActive(true);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Loja"))
        {
            estaNaAreaCompra = false;
            tooltipAbrirLoja.SetActive(false);
            FecharLoja();
        }
    }

    private void Update()
    {
        if (estaNaAreaCompra && Input.GetKeyDown(KeyCode.F))
        {
            lojaAberta = !lojaAberta;

            if (lojaAberta) 
            {
                AbrirLoja();
            }
            else
            {
                FecharLoja();
            }

        }
    }

    public void AbrirLoja()
    {
        Cursor.lockState = CursorLockMode.None;
        lojaUI.SetActive(true);
        tooltipAbrirLoja.SetActive(false);

        Jogador.Instance.PausarJogador();
    }

    public void FecharLoja()
    {
        Cursor.lockState = CursorLockMode.Locked;
        lojaUI.SetActive(false);

        Jogador.Instance.RetomarJogador();
    }
}
