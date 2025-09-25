using UnityEngine;
using UnityEngine.AI;

public class Inimigo : MonoBehaviour
{
    private Transform jogador;
    private NavMeshAgent navMeshAgent;
    private Animator animator;

    [SerializeField] private float distanciaAtaque;
    private float tempoProximoAtaque;
    [SerializeField] private float intervaloEntreAtaques;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jogador = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanciaParaJogador = Vector3.Distance(jogador.position, transform.position);
        if(distanciaParaJogador < distanciaAtaque)
        {
            navMeshAgent.velocity = Vector3.zero;

            if (Time.time > tempoProximoAtaque)
            {
                Atacar();
            }
        }
        else
        {
            navMeshAgent.SetDestination(jogador.position);
        }
    }

    private void Atacar()
    {
        Vector3 direcaoParaJogador = (jogador.position - transform.position).normalized;
        Quaternion rotacaoParaJogador = Quaternion.LookRotation(direcaoParaJogador);

        transform.rotation = rotacaoParaJogador;
        animator.SetTrigger("Atacar");
        tempoProximoAtaque = Time.time + intervaloEntreAtaques;
    }
}
