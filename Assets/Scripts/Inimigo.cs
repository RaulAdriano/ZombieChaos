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

    [SerializeField] private HitboxInimigo hitboxInimigo;
    [SerializeField] private int dano;

    [SerializeField] private AudioSource inimigoAudioSource;
    [SerializeField] private AudioClip[] sonsInimigosAudioClips;
    [SerializeField] private AudioSource atacarAudioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jogador = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
        InvokeRepeating(nameof(TocarSomInimigo), Random.Range(0,4), Random.Range(4,8));
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
                PrepararAtaque();
            }
        }
        else
        {
            navMeshAgent.SetDestination(jogador.position);
        }

        animator.SetBool("Mover", navMeshAgent.velocity.magnitude >= 0.1f);
    }

    private void PrepararAtaque()
    {
        Vector3 direcaoParaJogador = (jogador.position - transform.position).normalized;
        Quaternion rotacaoParaJogador = Quaternion.LookRotation(direcaoParaJogador);

        transform.rotation = rotacaoParaJogador;
        animator.SetTrigger("Atacar");
        tempoProximoAtaque = Time.time + intervaloEntreAtaques;
    }

    public void RealizarAtaque()
    {
        atacarAudioSource.Play();

        if (hitboxInimigo.GetJogadorNaHitbox())
        {
            Jogador.Instance.ReduzirVida(dano);
        }
    }

    public void Morrer()
    {
        enabled = false;
        animator.SetTrigger("Morrer");
        Destroy(gameObject, 5f);
    }

    public void TocarSomInimigo()
    {
        inimigoAudioSource.PlayOneShot(sonsInimigosAudioClips[Random.Range(0,sonsInimigosAudioClips.Length)]);

    }
}
