namespace AdventOfCode2022.Runner;

public static class Runner {
    public static void RunDay(string day) {
        var dayType = Type.GetType($"AdventOfCode2022.Solutions.{day}") ??
                      throw new NullReferenceException($"Unknown day {day}");
        ((ISolution) Activator.CreateInstance(dayType)!).Run();
    }
}