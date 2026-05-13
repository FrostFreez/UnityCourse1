using UnityEngine;
using UnityEngine.UI;

public class AbilityUIManager : MonoBehaviour
{
    [SerializeField] private PlaceAbility ability;
    [SerializeField] public Text countText;
    [SerializeField] private int nextCount = -1;

    private void Awake()
    {
        ability = GetComponent<PlaceAbility>();
        ability.change += UpdateUI;
    }

    private void Update()
    {
        if (nextCount >= 0)
        {
            UpdateUI(nextCount);
        }
    }

    private void UpdateUI(int count)
    {
        if (countText)
        {
            countText.text = count.ToString();
            nextCount = -1;
        }
        else
        {
            nextCount = count;
        }
    }
}
