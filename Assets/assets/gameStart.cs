using UnityEngine;
using UnityEngine.UI;

public class gameStart : MonoBehaviour
{
    public GameObject prefab;
    public float speed = 3f;
    public Button startGameButton;
    public float deviationLimit;
    public float randomMargin = 0.5f;
    private GameObject obj1, obj2;
    private bool gameStarted = false;

    void Start()
    {
        obj1 = Instantiate(prefab, new Vector3(-10, 2, 0), Quaternion.identity);
        obj2 = Instantiate(prefab, new Vector3(-10, -2, 0), Quaternion.identity);

        disMove(obj1);
        disMove(obj2);

        startGameButton.onClick.AddListener(OnStartGame);
    }

    void Update()
    {
        if (gameStarted) return;

        moveObj(obj1);
        moveObj(obj2);

        if(obj1.transform.position.x > 5)
        {
        loopBack(obj1);
        }
        if (obj2.transform.position.x > 5)
        {
            loopBack(obj2);
        }
    }

    void moveObj(GameObject obj)
    {
        obj.transform.Translate(Vector3.right * speed * Time.deltaTime);
       
    }

    void loopBack(GameObject obj)
    {
        obj.transform.position = new Vector3(-10, obj.transform.position.y, obj.transform.position.z);
    }

    void disMove(GameObject obj)
    {
        var comp = obj.GetComponent<move>();
        if (comp != null) comp.enabled = false;
    }

    void enMove(GameObject obj)
    {
        var comp = obj.GetComponent<move>();
        if (comp != null) comp.enabled = true;
    }

    void OnStartGame()
    {
        gameStarted = true;
        startGameButton.gameObject.SetActive(false);
        enMove(obj1);
        enMove(obj2);
    }
}
