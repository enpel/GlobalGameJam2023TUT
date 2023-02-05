using TMPro;
using UnityEngine;

namespace Term.Example
{
    public class TermView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private TextMeshProUGUI text2;
        // Start is called before the first frame update
        async void Start()
        {
            text.text = await TermLoader.Instance.GetTermAsync(TermType.Penetration);
            text2.text = TermLoader.Instance.GetTerm(TermType.Beauty);
        }

    }
}
