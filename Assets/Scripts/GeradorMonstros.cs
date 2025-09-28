using System.Collections;
using UnityEngine;

public class GeradorMonstros : MonoBehaviour
{
    [Tooltip("Array de Prefabs de Monstros ordenados por dificuldade")]
    [SerializeField] private GameObject[] monstros;

    [SerializeField] private Transform[] pontosDeSpawn;

    [SerializeField] private int monstrosIniciaisPorOnda;

    private int ondaAtual = 1;
    private float tempoRestanteProximaOnda;

    private Transform jogador;


    private void Start()
    {
        jogador = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(GerarOndas());

        InterfaceUsuario.Instance.AtualizarTempoRestante(tempoRestanteProximaOnda);
        InterfaceUsuario.Instance.AtualizarOndaAtual(ondaAtual);

    }
    private void Update()
    {
        tempoRestanteProximaOnda -= Time.deltaTime;
        InterfaceUsuario.Instance.AtualizarTempoRestante(tempoRestanteProximaOnda);

    }

    private IEnumerator GerarOndas()
    {
        while (true)
        {
            Jogador.Instance.RestaurarVida();

            tempoRestanteProximaOnda = 30 + 5 * ondaAtual;
            int totalMonstros = Mathf.CeilToInt(monstrosIniciaisPorOnda * Mathf.Log(ondaAtual + 1));

            int subOndas = Mathf.CeilToInt(tempoRestanteProximaOnda / 20);
            float intervaloSubOnda = tempoRestanteProximaOnda / subOndas;

            for (int i = 0; i < subOndas; i++)
            {
                GerarMonstros(Mathf.CeilToInt(totalMonstros/subOndas));
                yield return new WaitForSeconds(intervaloSubOnda);
            }

            ondaAtual++;

            InterfaceUsuario.Instance.AtualizarOndaAtual(ondaAtual);

        }

    }

    private void GerarMonstros(int quantidade)
    {
        for (int i = 0; i < quantidade; i++)
        {
            int maxIndiceMonstro = Mathf.Min(ondaAtual / 2, monstros.Length);
            int indiceTipoMonstro = Random.Range(0, maxIndiceMonstro);

            int indiceSpawn;

            do
            {

                indiceSpawn = Random.Range(0, pontosDeSpawn.Length);

            } while (Vector3.Distance(pontosDeSpawn[indiceSpawn].position, jogador.position) < 15f);

            Transform pontoDeSpawn = pontosDeSpawn[indiceSpawn];
            Instantiate(monstros[indiceTipoMonstro], pontoDeSpawn.position, pontoDeSpawn.rotation);

        }
    }
}
