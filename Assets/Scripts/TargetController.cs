using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField] private int points;
    [SerializeField] private GameObject parrent;

    private void OnTriggerEnter()
    {
        EventManager.RaiseAddPoints(points);
        Destroy(parrent);
    }
}