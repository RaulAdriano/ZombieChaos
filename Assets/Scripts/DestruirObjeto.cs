using UnityEngine;

public class DestruirObjeto : MonoBehaviour
{
    [SerializeField] private float tempoVida;

    void Start()
    {
        Destroy(gameObject,tempoVida);    
    }

}
