using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameOverScreen : MonoBehaviour
{

    public Text pointsText;
    public void Setup(int score)
    {
        gameObject.SetActive(true);
        
    }
}
