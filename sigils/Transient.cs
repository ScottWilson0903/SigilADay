using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using APIPlugin;
using DiskCardGame;

namespace SigilADay
{
  public partial class Plugin
  {
    private NewAbility AddTransient()
    {
      AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
      info.powerLevel = 3;
      info.rulebookName = "Transient";
      //      if(Localization.CurrentLanguage == Language.ChineseSimplified)
      //          info.rulebookName = "暂时";
      info.rulebookDescription = "[creature] will return to your hand at the end of the turn.";
            if(Localization.CurrentLanguage == Language.ChineseSimplified)
                info.rulebookDescription = "[creature]会在回合结束时返回到你的手牌中。";
      info.metaCategories = new List<AbilityMetaCategory> {AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part1Modular};

      List<DialogueEvent.Line> lines = new List<DialogueEvent.Line>();
      DialogueEvent.Line line = new DialogueEvent.Line();
      line.text = "The creature blinks back into the owner's hand at the end of their turn.";
            if(Localization.CurrentLanguage == Language.ChineseSimplified)
                line.text = "造物会在它的回合结束后返回到持牌人的手中。";
      lines.Add(line);
      info.abilityLearnedDialogue = new DialogueEvent.LineSet(lines);

      byte[] imgBytes = System.IO.File.ReadAllBytes(Path.Combine(this.Info.Location.Replace("SigilADay.dll",""),"Artwork/ability_transient.png"));
      Texture2D tex = new Texture2D(2,2);
      tex.LoadImage(imgBytes);

      NewAbility ability = new NewAbility(info,typeof(Transient),tex,AbilityIdentifier.GetAbilityIdentifier(PluginGuid, info.rulebookName));
      Transient.ability = ability.ability;
      return ability;
    }
  }

  public class Transient : DrawCreatedCard
  {
    public override Ability Ability
    {
      get
      {
        return ability;
      }
    }

    public static Ability ability;

    private void Start()
    {
      this.copy = CardLoader.Clone(base.Card.Info);
    }

    public override CardInfo CardToDraw
		{
			get
			{
				return CardLoader.Clone(this.copy);
			}
		}

    public override bool RespondsToTurnEnd(bool playerTurnEnd)
    {
      return playerTurnEnd;
    }

    public override IEnumerator OnTurnEnd(bool playerTurnEnd)
    {
      yield return base.PreSuccessfulTriggerSequence();
			yield return base.CreateDrawnCard();
      base.Card.Anim.PlayDeathAnimation(false);
      base.Card.UnassignFromSlot();
			base.Card.StartCoroutine(base.Card.DestroyWhenStackIsClear());
      base.Card.Slot = null;
      yield break;
    }

    private CardInfo copy;
  }
}
