using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
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

    private Coroutine recarregarCoroutine;
    private bool recarregando;

    [SerializeField] private CinemachinePanTilt panTilt;
    private float tempoRecoil;
    [SerializeField] private CinemachineImpulseSource impulseSource;    

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
        if (recarregando) return;

        Arma armaAtual = GetArmaAtual();
        Recarregar(armaAtual);
        Atirar(armaAtual);
        AplicarRecoil(armaAtual);
        Mirar(armaAtual);
        
    }

    public Arma GetArmaAtual()
    {
        return armaPrimaria.gameObject.activeSelf ? armaPrimaria : armaSecundaria;
    }

    public void Atirar(Arma armaAtual)
    {
        if (Input.GetButton("Fire1") && Time.time >= tempoProximoTiro && armaAtual.municaoAtual > 0 && !MovimentoJogador.EstaCorrendo())
        {
            impulseSource.GenerateImpulse();
            tempoProximoTiro = Time.time + 1f/armaAtual.tirosPorSegundo;

            armaAtual.RealizarDisparo();

            RaycastHit hit;
            if(Physics.Raycast(cameraPrincipal.position, cameraPrincipal.forward, out hit, 1000, tiroLayerMask, QueryTriggerInteraction.Ignore))
            {
                Instantiate(efeitoImpactoTiro, hit.point, Quaternion.LookRotation(hit.point));
            }

            tempoRecoil = 0.2f;
            armaAtual.ProximoRecoil();

        }
    }

    public void Recarregar(Arma armaAtual)
    {

        if ((Input.GetKeyDown(KeyCode.R) || armaAtual.municaoAtual <= 0) && (armaAtual.municaoNoInventario >0) && armaAtual.municaoAtual != armaAtual.capacidadePente)
        {
            CancelarRecarga();
            recarregarCoroutine = StartCoroutine(ExecutarRecarga(armaAtual));    
        }

    }

    private void CancelarRecarga()
    {
        if(recarregarCoroutine != null)
        {
            StopCoroutine(recarregarCoroutine);
        }
        recarregando = false;
    }

    private IEnumerator ExecutarRecarga(Arma armaAtual)
    {
        recarregando = true;
        armaAtual.animator.SetTrigger("Recarregar");

        yield return new WaitForSeconds(armaAtual.tempoDelayRecarregar);

        int balasRecarregar = Mathf.Min(armaAtual.capacidadePente, armaAtual.municaoNoInventario) - armaAtual.municaoAtual;

        armaAtual.RecarregarArma(balasRecarregar);
        recarregando = false;
    }

    private void AplicarRecoil(Arma armaAtual)
    {
        if (tempoRecoil <= 0f) return;

        tempoRecoil -= Time.deltaTime;
        Vector2 recoilAtual = armaAtual.ObterRecoilAtual();
        panTilt.PanAxis.Value += recoilAtual.x * Time.deltaTime;
        panTilt.TiltAxis.Value += recoilAtual.y * Time.deltaTime;
    }

    private void Mirar(Arma armaAtual)
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            armaAtual.AlterarMira();
        }
    }
}
