using UnityEngine;
using UnityEngine.UI;

public class StarDisplay : MonoBehaviour
{
    public Image[] stars;       // Assign S1, S2, S3 in the Inspector (ONLY these)
    public Sprite starOn;       // Bright star
    public Sprite starOff;      // Dimmed star

    public void ShowStars(int cowsRemaining)
    {
        int starCount = CalculateStarCount(cowsRemaining);

        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].sprite = i < starCount ? starOn : starOff;
        }
    }

    private int CalculateStarCount(int cowsRemaining)
    {
        if (cowsRemaining >= 3)
            return 3;
        else if (cowsRemaining == 2)
            return 2;
        else if (cowsRemaining == 1)
            return 1;
        else
            return 0;
    }
}
