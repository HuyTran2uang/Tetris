using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private string _name;

    public string Name => _name;

    public void Open() => gameObject.SetActive(true);

    public void Close() => gameObject.SetActive(false);
}
