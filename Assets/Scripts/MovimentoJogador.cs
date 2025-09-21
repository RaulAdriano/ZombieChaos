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

    void Start()
    {
        cameraPrincipal = Camera.main.transform;
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float movimentoHorizontal = Input.GetAxis("Horizontal");
        float movimentoVertical = Input.GetAxis("Vertical");

        Vector3 direcaoMovimento = new Vector3(movimentoHorizontal, 0, movimentoVertical);
        direcaoMovimento = cameraPrincipal.TransformDirection(direcaoMovimento).normalized;
        direcaoMovimento.y = 0;

        characterController.Move(direcaoMovimento * velocidadeMovimento * Time.deltaTime);
        AplicarGravidade();
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
}
