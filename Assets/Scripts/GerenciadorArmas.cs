using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorArmas : MonoBehaviour
{

    [SerializeField] private List<Arma> armasDisponiveis;

    [SerializeField] private Arma armaPrimaria;
    [SerializeField] private Arma armaSecundaria;

    [SerializeField] private GameObject efeitoImpactoTiro;
    [SerializeField] private GameObject efeitoSangue;

    [SerializeField] private LayerMask tiroLayerMask;

    private Transform cameraPrincipal;

    private float tempoProximoTiro;

    private MovimentoJogador MovimentoJogador;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameraPrincipal = Camera.main.transform;
        armaPrimaria.gameObject.SetActive(true);    
        armaSecundaria?.gameObject.SetActive(false);
        MovimentoJogador = GetComponent<MovimentoJogador>();    
    }

    // Update is called once per frame
    void Update()
    {   
        Arma armaAtual = GetArmaAtual();
        Atirar(armaAtual);
        
    }

    public Arma GetArmaAtual()
    {
        return armaPrimaria.gameObject.activeSelf ? armaPrimaria : armaSecundaria;
    }

    public void Atirar(Arma armaAtual)
    {
        if (Input.GetButton("Fire1") && Time.time >= tempoProximoTiro && armaAtual.municaoAtual > 0 && !MovimentoJogador.EstaCorrendo())
        {
            tempoProximoTiro = Time.time + 1f/armaAtual.tirosPorSegundo;

            armaAtual.RealizarDisparo();

            RaycastHit hit;
            if(Physics.Raycast(cameraPrincipal.position, cameraPrincipal.forward, out hit, 1000, tiroLayerMask, QueryTriggerInteraction.Ignore))
            {
                Instantiate(efeitoImpactoTiro, hit.point, Quaternion.LookRotation(hit.point));
            }

        }
    }
}
