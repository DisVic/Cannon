using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public int currentScore = 0;

    private void OnEnable()
    {
        EventManager.OnAddPoints += HandleAddPoints;
    }

    private void OnDisable()
    {
        EventManager.OnAddPoints -= HandleAddPoints;
    }

    private void HandleAddPoints(int points)
    {
        currentScore += points;
        Debug.Log(currentScore);
    }
}
