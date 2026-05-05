using UnityEngine;
using UnityEngine.UI;

public class AbilityUIManager : MonoBehaviour
{
    [SerializeField] private PlaceAbility ability;
    [SerializeField] private Text countText;

    private void Awake()
    {
        ability = GetComponent<PlaceAbility>();
        ability.change += UpdateUI;
    }

    private void UpdateUI(int count)
    {
        countText.text = count.ToString();
    }
}
