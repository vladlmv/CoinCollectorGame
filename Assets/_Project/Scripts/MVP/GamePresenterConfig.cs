using UnityEngine;

namespace _Project.Scripts.MVP
{
    [CreateAssetMenu(fileName = nameof(GamePresenterConfig), menuName = "Config/MVP/Game Presenter")]
    public class GamePresenterConfig : ScriptableObject
    {
        [field: SerializeField] public GameView GameView  { get; private set; }
    }
}