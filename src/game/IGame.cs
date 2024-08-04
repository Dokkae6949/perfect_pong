using Chickensoft.AutoInject;
using Chickensoft.GodotNodeInterfaces;
using test.game.data;

namespace test.game;

public interface IGame : INode, IProvide<IGameRepo>
{
    
}