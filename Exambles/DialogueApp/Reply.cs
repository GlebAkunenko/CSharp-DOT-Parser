using DotParser.Graphs;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace Exambles;

public class Reply : Node
{
    private string _text;

    public Effect? Effect { get; init; }
    public Condition? Condition { get; init; }

    public Reply(DotParser.DOT.Node source) : base(source)
    {
        _text = source.Name;
        if (Effect.TryParse(source.Attributes, out Effect? effect))
            Effect = effect;
        if (Condition.TryParse(source.Attributes, out Condition? condition))
            Condition = condition;
    }

    public string GetText()
    {
        string result = _text;

        if (Condition != null) {
            List<string> conditions = new List<string>();
            if (Condition.CoinCondition)
                conditions.Add(Condition.Coins.ToString() + "$");
            if (Condition.CharismaCondition)
                conditions.Add(Condition.Charisma.ToString() + " хар.");

            if (conditions.Count == 0)
                throw new Exception();
            result += $" [{string.Join(" ", conditions)}] ";
        }

        if (Effect != null) {
            List<string> effects = new List<string>();
            if (Effect.CharismaBoost != 0)
                effects.Add("(" + (Effect.CharismaBoost > 0 ? "+" : "") + Effect.CharismaBoost.ToString() + " хар.)");
            if (Effect.CoinsBoost != 0)
                effects.Add("(" + (Effect.CoinsBoost > 0 ? "+" : "") + Effect.CoinsBoost.ToString() + "$)");

            if (effects.Count == 0)
                throw new Exception();
            result += $" {string.Join(" ", effects)} ";

        }

        return result;
    }
}

/// <summary>
/// Some choices have effects that offer to some player stats
/// </summary>
public class Effect
{
    const string BOOST_ATTRIBUTE_NAME = "ch_";

    public int CharismaBoost { get; private set; }
    public int CoinsBoost { get; private set; }

    public static bool TryParse(DotParser.DOT.Attribute[] attributes, out Effect? result)
    {
        result = new Effect();
        foreach (DotParser.DOT.Attribute attribute in attributes) {
            if (attribute.Name.ToLower().StartsWith(BOOST_ATTRIBUTE_NAME)) {
                switch (attribute.Name.ToLower()) {
                    case BOOST_ATTRIBUTE_NAME + "charisma":
                        result.CharismaBoost = int.Parse(attribute.Value.Trim());
                        break;
                    case BOOST_ATTRIBUTE_NAME + "coins":
                        result.CoinsBoost = int.Parse(attribute.Value.Trim());
                        break;
                    default:
                        throw new Exception("Unsupported property");
                }
            }
        }
        return result.CoinsBoost != 0 || result.CharismaBoost != 0;
    }
}

/// <summary>
/// Some choices have necessary conditions
/// </summary>
public class Condition
{
    const string CONDITION_ATTRIBUTES_NAME = "need_";

    public int Coins { get; private set; }
    public int Charisma { get; private set; }

    public bool CoinCondition => Coins > int.MinValue;

    public bool CharismaCondition => Charisma > int.MinValue;

    public Condition()
    {
        Charisma = Coins = int.MinValue;
    }

    public bool Check(Player player) => player.Coins >= Coins && player.Charisma >= Charisma;

    public static bool TryParse(DotParser.DOT.Attribute[] attributes, out Condition result)
    {
        result = new Condition();
        foreach(DotParser.DOT.Attribute attribute in attributes) {
            if (attribute.Name.ToLower().StartsWith(CONDITION_ATTRIBUTES_NAME)) {
                int value = int.Parse(attribute.Value.Trim());

                switch(attribute.Name.ToLower()) {
                    case CONDITION_ATTRIBUTES_NAME + "coins":
                        result.Coins = value;
                        break;
                    case CONDITION_ATTRIBUTES_NAME + "charisma":
                        result.Charisma = value;
                        break;
                    default:
                        throw new Exception("Unsupported property");
                }
            }
        }
        return result.Charisma != int.MinValue || result.Coins != int.MinValue;
    }
}

