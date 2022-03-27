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
    private NewAbility AddBloodGuzzler()
    {
      AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
      info.powerLevel = 5;
      info.rulebookName = "BloodGuzzler";
      //      if(Localization.CurrentLanguage == Language.ChineseSimplified)
      //          info.rulebookName = "嗜血者";
      info.rulebookDescription = "When a creature bearing this sigil deals damage, it gains 1 Health for each damage dealt.";
            if (Localization.CurrentLanguage == Language.ChineseSimplified)
                info.rulebookDescription = "[creature]造成伤害时，每造成一点获得1点生命值。";
      info.metaCategories = new List<AbilityMetaCategory> {AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part1Modular};

      List<DialogueEvent.Line> lines = new List<DialogueEvent.Line>();
      DialogueEvent.Line line = new DialogueEvent.Line();
      line.text = "This creature will gains 1 Health for each damage it deals to creatures.";
            if (Localization.CurrentLanguage == Language.ChineseSimplified)
                line.text = "这个造物对其他造物每造成一点伤害，就会获得1点生命值。";
      lines.Add(line);
      info.abilityLearnedDialogue = new DialogueEvent.LineSet(lines);

      byte[] imgBytes = System.IO.File.ReadAllBytes(Path.Combine(this.Info.Location.Replace("SigilADay.dll",""),"Artwork/ability_bloodguzzler.png"));
      Texture2D tex = new Texture2D(2,2);
      tex.LoadImage(imgBytes);

      NewAbility ability = new NewAbility(info,typeof(BloodGuzzler),tex,AbilityIdentifier.GetAbilityIdentifier(PluginGuid, info.rulebookName));
      BloodGuzzler.ability = ability.ability;
      return ability;
    }
  }

  public class BloodGuzzler : CustomAbilityBehaviour
  {
    private void Start()
    {
      int health = base.Card.Info.Health;
      this.mod = new CardModificationInfo();
      this.mod.nonCopyable = true;
      this.mod.singletonId = "increaseHP";
      this.mod.healthAdjustment = 0;
      base.Card.AddTemporaryMod(this.mod);
    }

    public override bool RespondsToDealDamage(int amount, PlayableCard target)
    {
       return amount > 0;
    }

    public override IEnumerator OnDealDamage(int amount, PlayableCard target)
    {
      yield return base.PreSuccessfulTriggerSequence();
      this.mod.healthAdjustment += amount;
      base.Card.OnStatsChanged();
      base.Card.Anim.StrongNegationEffect();
      yield return new WaitForSeconds(0.25f);
      yield return base.LearnAbility(0.25f);
      yield break;
    }

    private CardModificationInfo mod;
  }
}
