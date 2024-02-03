namespace CaterpillarControlSystem.App;
using CaterpillarControlSystem.App.Controllers;
using CaterpillarControlSystem.App.Constants;

class Program
{
    static void Main(string[] args)
    {

        GecaController gecaController = new GecaController(AppConstants.START_X, AppConstants.START_Y, AppConstants.AREA_SIZE);

        gecaController.Run();
    }
}
