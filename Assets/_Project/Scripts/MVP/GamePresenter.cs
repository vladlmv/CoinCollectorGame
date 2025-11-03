using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.MVP
{
    public interface IGamePresenter : IInitializable
    {
    }
    
    public class GamePresenter : IGamePresenter
    {
        private readonly IGameModel _model;
        private readonly GamePresenterConfig _config;
        private GameView _view;

        [Inject]
        public GamePresenter(IGameModel model, GamePresenterConfig config)
        {
            _model = model;
            _config = config;
        }

        void IInitializable.Initialize()
        {
            _view = Object.Instantiate(_config.GameView, null);
        }
    }
}