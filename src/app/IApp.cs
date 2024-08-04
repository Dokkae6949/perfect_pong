using Chickensoft.AutoInject;
using Chickensoft.GodotNodeInterfaces;
using test.app.data;

namespace test.app;

public interface IApp : ICanvasLayer, IProvide<IAppRepo>
{
    
}