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
    private NewAbility AddLeech()
    {
      AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
      info.powerLevel = 4;
      info.rulebookName = "Leech";
            if(Localization.CurrentLanguage == Language.ChineseSimplified)
                info.rulebookName = "水蛭";
      info.rulebookDescription = "When a creature bearing this sigil deals damage, it heals 1 Health for each damage dealt.";
            if(Localization.CurrentLanguage == Language.ChineseSimplified)
                info.rulebookDescription = "[creature]造成伤害时，每造成一点恢复1点生命值。";
      info.metaCategories = new List<AbilityMetaCategory> {AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part1Modular};

      List<DialogueEvent.Line> lines = new List<DialogueEvent.Line>();
      DialogueEvent.Line line = new DialogueEvent.Line();
      line.text = "This creature will heals 1 Health for each damage it deals to creatures.";
            if(Localization.CurrentLanguage == Language.ChineseSimplified)
                line.text = "这个造物对其他造物每造成一点伤害，就会恢复1点生命值。";
      lines.Add(line);
      info.abilityLearnedDialogue = new DialogueEvent.LineSet(lines);

      // Icon made by Lorc. Available on https://game-icons.net
      byte[] imgBytes = System.IO.File.ReadAllBytes(Path.Combine(this.Info.Location.Replace("SigilADay.dll",""),"Artwork/ability_leech.png"));
      Texture2D tex = new Texture2D(2,2);
      tex.LoadImage(imgBytes);

      NewAbility ability = new NewAbility(info,typeof(Leech),tex,AbilityIdentifier.GetAbilityIdentifier(PluginGuid, info.rulebookName));
      Leech.ability = ability.ability;
      return ability;
    }
  }

  public class Leech : CustomAbilityBehaviour
  {
    public override bool RespondsToDealDamage(int amount, PlayableCard target)
    {
      return amount > 0;
    }

    public override IEnumerator OnDealDamage(int amount, PlayableCard target)
    {
      yield return base.PreSuccessfulTriggerSequence();
      if (base.Card.Status.damageTaken > 0)
      {
        base.Card.HealDamage(Mathf.Clamp(amount, 1, base.Card.Status.damageTaken));
      }
      base.Card.OnStatsChanged();
      base.Card.Anim.StrongNegationEffect();
      yield return new WaitForSeconds(0.25f);
      yield return base.LearnAbility(0.25f);
      yield break;
    }
  }
}
