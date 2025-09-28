using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class MenuConfiguracoes : MonoBehaviour
{
    [SerializeField] private GameObject menuPause;
    [SerializeField] private CinemachineInputAxisController cinemachineInputAxisController;

    [SerializeField] private Slider sensibilidadeSlider;
    [SerializeField] private Slider audioSlider;
    [SerializeField] private TMP_Dropdown qualidadeDropDown;

    private void Start()
    {
        CarregarConfiguracoes();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && menuPause)
        {
            menuPause.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Jogador.Instance.PausarJogador();
        }
    }

    public void RetomarPartida()
    {
        menuPause.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Jogador.Instance.RetomarJogador();
    }

    public void SalvarSensibilidade()
    {
        float sensibilidade = sensibilidadeSlider.value;

        PlayerPrefs.SetFloat("Sensibilidade", sensibilidade);
        PlayerPrefs.Save();

        if (cinemachineInputAxisController)
        {
            //Eixo Horizontal
            cinemachineInputAxisController.Controllers[0].Input.LegacyGain = 200 * sensibilidade;
            //Eixo Vertical
            cinemachineInputAxisController.Controllers[1].Input.LegacyGain = -200 * sensibilidade;
        }
    }

    public void SalvarAudio()
    {
        float audio = audioSlider.value;

        PlayerPrefs.SetFloat("Audio", audio);
        PlayerPrefs.Save();

        AudioListener.volume = audio;
    }

    public void SalvarQualidade()
    {
        int qualidadeIndex = qualidadeDropDown.value;

        PlayerPrefs.SetInt("Qualidade", qualidadeIndex);
        PlayerPrefs.Save();

        QualitySettings.SetQualityLevel(qualidadeIndex);
    }

    public void SalvarConfiguracoes()
    {
        SalvarSensibilidade();
        SalvarAudio();
        SalvarQualidade();
    }

    public void CarregarConfiguracoes()
    {
        float sensibilidade = PlayerPrefs.GetFloat("Sensibilidade", 1.0f);
        float audio = PlayerPrefs.GetFloat("Audio", 1.0f);
        int qualidade = PlayerPrefs.GetInt("Qualidade", 3);

        audioSlider.value = audio;
        AudioListener.volume = audio;

        if (cinemachineInputAxisController)
        {
            //Eixo Horizontal
            cinemachineInputAxisController.Controllers[0].Input.LegacyGain = 200 * sensibilidade;
            //Eixo Vertical
            cinemachineInputAxisController.Controllers[1].Input.LegacyGain = -200 * sensibilidade;
        }
        sensibilidadeSlider.value = sensibilidade;

        qualidadeDropDown.value = qualidade;
        QualitySettings.SetQualityLevel (qualidade);
    }
}
