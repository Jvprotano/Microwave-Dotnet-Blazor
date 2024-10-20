namespace Microwave.Core.Models;

public class PredefinedPrograms : EntityBase
{
    public PredefinedPrograms(string name, string food, int timeSeconds, int power, char labelHarm, string? instructions = null)
    {
        Name = name;
        Food = food;
        TimeSeconds = timeSeconds;
        Power = power;
        Instructions = instructions;
        LabelHarm = labelHarm;
        IsPredefined = false;
    }

    public string Name { get; private set; }
    public string Food { get; private set; }
    public int TimeSeconds { get; private set; }
    public int Power { get; private set; }
    public string? Instructions { get; private set; }
    public char LabelHarm { get; private set; }
    public bool IsPredefined { get; private set; }

    public static IList<PredefinedPrograms> GetPredefinedPrograms()
    {
        var predefinedPrograms = new List<PredefinedPrograms>
        {
            new ("Pipoca", "Pipoca (de micro-ondas)", 180, 7, 'P', @"Observar o barulho de estouros do milho, caso houver um intervalo de mais de 10 segundos entre um 
                estouro e outro, interrompa o aquecimento."),
            new ("Leite", "Leite", 300, 5, 'L', @"Cuidado com aquecimento de líquidos, o choque térmico aliado ao movimento do recipiente pode 
                causar fervura imediata causando risco de queimaduras."),
            new ("Carnes de boi", "Carne em pedaço ou fatias", 840, 4, 'C', @"Interrompa o processo na metade e vire o conteúdo com a parte de baixo para cima para o 
                descongelamento uniforme."),
            new ("Frango", "Frango (qualquer corte)", 480, 7, 'F', @"Interrompa o processo na metade e vire o conteúdo com a parte de baixo para cima para o 
                descongelamento uniforme"),
            new ("Feijão", "Feijão congelado", 480, 9, 'B', @"Deixe o recipiente destampado e em casos de plástico, cuidado ao retirar o recipiente pois o mesmo 
                pode perder resistência em altas temperaturas."),
        };

        predefinedPrograms.ForEach(program => program.IsPredefined = true);
        return predefinedPrograms;
    }
}
