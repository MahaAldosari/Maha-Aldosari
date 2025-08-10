using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text fishCounterText;

    public void UpdateFishCount(int count)
    {
        fishCounterText.text = "Fish Collected: " + count;
    }
}