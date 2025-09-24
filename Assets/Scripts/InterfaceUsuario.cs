using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceUsuario : MonoBehaviour
{

    public static InterfaceUsuario Instance;

    [SerializeField] private Slider staminaSlider;
    [SerializeField] private TMP_Text municaoText;
    [SerializeField] private Image miraImage;

    private void Awake()
    {
       if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void AtualizarStamina(float stamina)
    {
        staminaSlider.value = stamina;
        staminaSlider.gameObject.SetActive(stamina < 0.99f);
    }

    public void AtualizarMunicao(int municaoAtual, int municaoNoInventario)
    {
        municaoText.text = municaoAtual + "/" + municaoNoInventario;
    }

    public void ExibirMira(bool exibirMira)
    {
        miraImage.enabled = exibirMira; 
    }

}
