using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject startButton;

    [SerializeField]
    private GameObject changeSkinbutton;

    [SerializeField]
    private GameObject scrollviewbutton;


    void Start()
    {
        changeSkinbutton.SetActive(true);
        startButton.SetActive(true);
        scrollviewbutton.SetActive(false);
    }
    public void StartButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ActiveChangeSkinButton()//buttonが押されたらscrollviewを表示する
    {
        changeSkinbutton.SetActive(false);
        startButton.SetActive(true);
        scrollviewbutton.SetActive(true);
    }

    public void CloseChangeSkinButton() //buttonが押されたらscrollviewを非表示にする
    {
        changeSkinbutton.SetActive(true);
        startButton.SetActive(true);

        scrollviewbutton.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("StartScene");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            DeleteSave();
        }
    }
    public void DeleteSave()//test用
    {
        PlayerPrefs.DeleteAll();
        Debug.LogError("セーブデータを全て削除した");
        SceneManager.LoadScene("StartScene");
    }

}
