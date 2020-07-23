using System;
using System.Threading.Tasks;

namespace TranslatorConsoleExample {
 
  class Program {
  static void Main(string[] args) {
      MainAsync(args).GetAwaiter().GetResult();

      Console.ReadKey();    
      Console.WriteLine("press anykey to exit");
    }

    static async Task MainAsync(string[] args) {
      string traslateText = await Translate.TranslateToSpanish("Hello World, how are you today?");
      Console.Write("Translate Text: ");
      Console.WriteLine(traslateText);
      Console.ReadLine();
    }

   

  }
}
