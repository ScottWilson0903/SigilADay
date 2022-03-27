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
    private NewAbility AddRegenFull()
    {
      AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
      info.powerLevel = 4;
      info.rulebookName = "Regen";
      //      if(Localization.CurrentLanguage == Language.ChineseSimplified)
      //          info.rulebookName = "满级回复";
      info.rulebookDescription = "At the end of the owner's turn, [creature] will regen to full health.";
            if(Localization.CurrentLanguage == Language.ChineseSimplified)
                info.rulebookDescription = "[creature]，会在持牌人回合结束时，完全恢复生命值。";
      info.metaCategories = new List<AbilityMetaCategory> {AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part1Modular};

      List<DialogueEvent.Line> lines = new List<DialogueEvent.Line>();
      DialogueEvent.Line line = new DialogueEvent.Line();
      line.text = "This creature will heal to full Health at the end of it's owner's turn.";
            if(Localization.CurrentLanguage == Language.ChineseSimplified)
                line.text = "这个造物会在持牌人回合结束时，完全恢复生命值。";
      lines.Add(line);
      info.abilityLearnedDialogue = new DialogueEvent.LineSet(lines);

      byte[] imgBytes = System.IO.File.ReadAllBytes(Path.Combine(this.Info.Location.Replace("SigilADay.dll",""),"Artwork/ability_regen_full.png"));
      Texture2D tex = new Texture2D(2,2);
      tex.LoadImage(imgBytes);

      NewAbility ability = new NewAbility(info,typeof(RegenFull),tex,AbilityIdentifier.GetAbilityIdentifier(PluginGuid, info.rulebookName));
      RegenFull.ability = ability.ability;
      return ability;
    }
  }

  public class RegenFull : CustomAbilityBehaviour
  {
    public override bool RespondsToUpkeep(bool playerUpkeep)
    {
      return base.Card.OpponentCard != playerUpkeep;
    }

    public override IEnumerator OnUpkeep(bool playerUpkeep)
    {
      yield return base.PreSuccessfulTriggerSequence();
      base.Card.HealDamage(base.Card.Status.damageTaken);
      yield return base.LearnAbility(0.25f);
      yield break;
    }
  }
}
