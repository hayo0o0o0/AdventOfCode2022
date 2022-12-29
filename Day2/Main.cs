namespace Day2;

public class Main
{
    private const string OpponentRock = "A";
    private const string OpponentPaper = "B";
    private const string OpponentScissors = "C";
    private const string OwnRock = "X";
    private const string OwnPaper = "Y";
    private const string OwnScissors = "Z";
    private const string MatchLose = "X";
    private const string MatchDraw = "Y";
    private const string MatchWin = "Z";

    private static readonly Dictionary<string, int> Scores = new()
    {
        { OwnRock, 1 }, // Rock
        { OwnPaper, 2 }, // Paper
        { OwnScissors, 3 } // Scissors
    };

    public static void Part1()
    {
        var rounds = ReadInput();
        var totalScore = 0;
        foreach (var roundInput in rounds)
        {
            var inputs = roundInput.Split(" ");
            var round = new Round(inputs.First(), inputs.Last());
            var roundPoints = CalculateRoundPoints(round);
            totalScore += roundPoints;
        }

        Console.WriteLine("Part 1: Total score: {0}", totalScore);
    }

    public static void Part2()
    {
        var rounds = ReadInput();
        var totalScore = 0;
        foreach (var roundInput in rounds)
        {
            var inputs = roundInput.Split(" ");
            var round = new Round
            {
                Opponent = inputs.First(),
                MatchDetermination = inputs.Last()
            };
            round.DetermineOwn();
            var roundPoints = CalculateRoundPoints(round);
            totalScore += roundPoints;
        }

        Console.WriteLine("Part 2: Total score: {0}", totalScore);
    }

    private static int CalculateRoundPoints(Round round)
    {
        var roundPoints = 0;
        switch (round)
        {
            case { Opponent: OpponentRock, Own: OwnRock }:
            case { Opponent: OpponentPaper, Own: OwnPaper }:
            case { Opponent: OpponentScissors, Own: OwnScissors }:
                roundPoints += 3;
                break;
            case { Opponent: OpponentRock, Own: OwnPaper }:
            case { Opponent: OpponentPaper, Own: OwnScissors }:
            case { Opponent: OpponentScissors, Own: OwnRock }:
                roundPoints += 6;
                break;
        }

        roundPoints += Scores[round.Own];
        return roundPoints;
    }

    private static IEnumerable<string> ReadInput()
    {
        var rounds = File.ReadAllLines("input.txt");
        return rounds;
    }

    private class Round
    {
        public Round(string opponent, string own)
        {
            Opponent = opponent;
            Own = own;
        }

        public Round()
        {
        }

        public string Opponent { get; set; }
        public string Own { get; set; }

        public string MatchDetermination { get; set; }

        public void DetermineOwn()
        {
            switch (this)
            {
                case { MatchDetermination: MatchLose, Opponent: OpponentPaper }:
                case { MatchDetermination: MatchDraw, Opponent: OpponentRock }:
                case { MatchDetermination: MatchWin, Opponent: OpponentScissors }:
                    Own = OwnRock;
                    break;
                case { MatchDetermination: MatchLose, Opponent: OpponentScissors }:
                case { MatchDetermination: MatchDraw, Opponent: OpponentPaper }:
                case { MatchDetermination: MatchWin, Opponent: OpponentRock }:
                    Own = OwnPaper;
                    break;
                case { MatchDetermination: MatchLose, Opponent: OpponentRock }:
                case { MatchDetermination: MatchDraw, Opponent: OpponentScissors }:
                case { MatchDetermination: MatchWin, Opponent: OpponentPaper }:
                    Own = OwnScissors;
                    break;
            }
        }
    }
}