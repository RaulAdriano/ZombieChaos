using UnityEngine;

public class MovimentoJogador : MonoBehaviour
{
    [SerializeField] private float velocidadeMovimento = 2.6f;
    private Transform cameraPrincipal;
    private CharacterController characterController;
    [SerializeField] private Transform pontoVerificacao;
    [SerializeField] private LayerMask camadaColisao;
    private bool estaNoChao;
    private float velocidadeVertical;

    private bool estaCorrendo;
    private float nivelStamina;

    private GerenciadorArmas gerenciadorArmas;

    void Start()
    {
        cameraPrincipal = Camera.main.transform;
        characterController = GetComponent<CharacterController>();
        gerenciadorArmas = GetComponent<GerenciadorArmas>();
    }

    // Update is called once per frame
    void Update()
    {
        AplicarGravidade();
        ProcessarMovimento();
        AtualizarStamina();
    }

    private void AplicarGravidade()
    {
        estaNoChao = Physics.CheckSphere(pontoVerificacao.position,0.3f,camadaColisao);

        if (Input.GetKeyDown(KeyCode.Space) && estaNoChao)
        {
            velocidadeVertical = 4.5f;
        }

        if (!estaNoChao || velocidadeVertical > Physics.gravity.y)
        {
            velocidadeVertical += Physics.gravity.y * Time.deltaTime;
        }

        characterController.Move(new Vector3(0,velocidadeVertical,0) * Time.deltaTime);
    }


    private void ProcessarMovimento()
    {
        float movimentoHorizontal = Input.GetAxis("Horizontal");
        float movimentoVertical = Input.GetAxis("Vertical");

        estaCorrendo = Input.GetKey(KeyCode.LeftShift);

        Vector3 direcaoMovimento = new Vector3(movimentoHorizontal, 0, movimentoVertical);
        direcaoMovimento = cameraPrincipal.TransformDirection(direcaoMovimento).normalized;
        direcaoMovimento.y = 0;

        float velocidadeAtual = estaCorrendo && nivelStamina > 0 ? velocidadeMovimento * 2f : velocidadeMovimento;

        if(estaCorrendo && nivelStamina > 0)
        {
            nivelStamina -= Time.deltaTime;
            nivelStamina = Mathf.Max(0f,nivelStamina);
        }

        gerenciadorArmas.GetArmaAtual().animator.SetBool("Mover", direcaoMovimento != Vector3.zero);
        gerenciadorArmas.GetArmaAtual().animator.SetBool("Correr", estaCorrendo && nivelStamina > 0f);

        characterController.Move(direcaoMovimento * Time.deltaTime * velocidadeAtual);
    }

    private void AtualizarStamina()
    {
        if(!estaCorrendo && nivelStamina < 2f)
        {
            nivelStamina += Time.deltaTime;
        }
    }

    public bool EstaCorrendo()
    {
        return estaCorrendo && nivelStamina > 0f;
    }
}
