using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private InGamePanel _inaGamePanel;
    [SerializeField] private WinPanel _winPanel;
    [SerializeField] private LosePanel _losePanel;
    [SerializeField] private Player _player;

    private IEnumerator Start()
    {
        yield return _inaGamePanel.Show();

        yield return _player.Play();
        if (_player.CurrentState == Player.State.Win)
        {
            yield return _winPanel.Show();
            LevelLoader.Instance.LoadNextLevel();
        }
        else if (_player.CurrentState == Player.State.Lose)
        {
            yield return _losePanel.Show();
            LevelLoader.Instance.RestartLevel();
        }
    }
}