using System;

namespace CraftEnd
{
  public static class Program
  {
    [STAThread]
    static void Main()
    {
      using (var engine = new Engine.PenguinEngine(typeof(CraftEnd), CoreGame.Content.Content.Texture2DCoordinateAxis))
        engine.Run();
    }
  }
}
