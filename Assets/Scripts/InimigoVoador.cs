using UnityEngine;
using UnityEngine.AI;

public class InimigoVoador : MonoBehaviour
{
    private Transform jogador;
    private NavMeshAgent navMeshAgente;
    private Animator animator;

    [SerializeField] private float distanciaDeAtaque;
    [SerializeField] private float intervaloEntreAtaques;

    [SerializeField] private GameObject bolaAcida;
    [SerializeField] private Transform pontoLancamento;

    private float tempoProximoAtaque;

    [SerializeField] private AudioSource atacarAudioSource;

    private void Start()
    {
        jogador = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgente = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float distanciaParaJogador = Vector3.Distance(jogador.position, transform.position);

        if(distanciaParaJogador <= distanciaDeAtaque)
        {
            navMeshAgente.velocity = Vector3.zero;

            if(Time.time > tempoProximoAtaque)
            {
                Atacar();
            }
        }
        else
        {
            navMeshAgente.SetDestination(jogador.position);
        }
    }

    private void Atacar()
    {
        atacarAudioSource.Play();
        tempoProximoAtaque = Time.time + intervaloEntreAtaques;

        pontoLancamento.LookAt(jogador);
        Instantiate(bolaAcida, pontoLancamento.position, pontoLancamento.rotation);
    }

    public void Morrer()
    {
        enabled = false;
        animator.SetTrigger("Morrer");
        Destroy(gameObject,0.4f);
    }

}

