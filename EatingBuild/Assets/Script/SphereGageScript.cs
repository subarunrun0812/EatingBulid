using UnityEngine;
using UnityEngine.UI;
public class SphereGageScript : MonoBehaviour
{
    [SerializeField] private Image image;//spharegageをアタッチする
    [SerializeField] private GameManager gameManager;
    void Start()
    {
        image.fillAmount = 0;//初期値のゲージを０にする
    }
    void Update()
    {
        float points = (float)gameManager.point / 1000f;
        Debug.Log(points);
        image.fillAmount = points;
    }
}
