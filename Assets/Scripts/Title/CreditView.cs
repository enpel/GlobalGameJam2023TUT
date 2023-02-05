using Gekkou;
using UnityEngine;

public class CreditView : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    public void OnClose()
    {
        UISoundManager.Instance.PlayFailure();
        this.panel.SetActive(false);
    }
    public void OnOpen()
    {
        UISoundManager.Instance.PlaySuccess();
        this.panel.SetActive(true);
    }
}
