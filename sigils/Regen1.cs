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
    private NewAbility AddRegen1()
    {
      AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
      info.powerLevel = 1;
      info.rulebookName = "Regen 1";
      //      if(Localization.CurrentLanguage == Language.ChineseSimplified)
      //          info.rulebookName = "1级回复";
      info.rulebookDescription = "At the end of the owner's turn, [creature] will regen 1 health.";
            if(Localization.CurrentLanguage == Language.ChineseSimplified)
                info.rulebookDescription = "[creature]，会在持牌人回合结束时，恢复1点生命值。";
      info.metaCategories = new List<AbilityMetaCategory> {AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part1Modular};

      List<DialogueEvent.Line> lines = new List<DialogueEvent.Line>();
      DialogueEvent.Line line = new DialogueEvent.Line();
      line.text = "This creature will heal 1 Health at the end of it's owner's turn.";
            if(Localization.CurrentLanguage == Language.ChineseSimplified)
                line.text = "这个造物阵亡时，它会毒害那个消灭它的造物。";
      lines.Add(line);
      info.abilityLearnedDialogue = new DialogueEvent.LineSet(lines);

      byte[] imgBytes = System.IO.File.ReadAllBytes(Path.Combine(this.Info.Location.Replace("SigilADay.dll",""),"Artwork/ability_regen_1.png"));
      Texture2D tex = new Texture2D(2,2);
      tex.LoadImage(imgBytes);

      NewAbility ability = new NewAbility(info,typeof(Regen1),tex,AbilityIdentifier.GetAbilityIdentifier(PluginGuid, info.rulebookName));
      Regen1.ability = ability.ability;
      return ability;
    }
  }

  public class Regen1 : CustomAbilityBehaviour
  {
    public override bool RespondsToUpkeep(bool playerUpkeep)
    {
      return base.Card.OpponentCard != playerUpkeep;
    }

    public override IEnumerator OnUpkeep(bool playerUpkeep)
    {
      yield return base.PreSuccessfulTriggerSequence();
      if (base.Card.Status.damageTaken > 0)
      {
        base.Card.HealDamage(1);
      }
      yield return base.LearnAbility(0.25f);
      yield break;
    }
  }
}
