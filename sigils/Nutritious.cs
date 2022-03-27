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
    private NewAbility AddNutritious()
    {
      AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
      info.powerLevel = 3;
      info.rulebookName = "Nutritious";
      //      if(Localization.CurrentLanguage == Language.ChineseSimplified)
      //          info.rulebookName = "富营养化";
      info.rulebookDescription = "A creature gain 1 power and 2 health when summoned using [creature] as a sacrifice.";
            if(Localization.CurrentLanguage == Language.ChineseSimplified)
                info.rulebookDescription = "献祭[creature]，召唤出来的造物会获得1点攻击力和2点生命值。";
      info.metaCategories = new List<AbilityMetaCategory> {AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part1Modular};

      List<DialogueEvent.Line> lines = new List<DialogueEvent.Line>();
      DialogueEvent.Line line = new DialogueEvent.Line();
      line.text = "That creature is so full of nutrients, the creature you play comes in stronger!";
            if(Localization.CurrentLanguage == Language.ChineseSimplified)
                line.text = "那个生物营养丰富，你召唤出来的造物会更强！";
      lines.Add(line);
      info.abilityLearnedDialogue = new DialogueEvent.LineSet(lines);

      byte[] imgBytes = System.IO.File.ReadAllBytes(Path.Combine(this.Info.Location.Replace("SigilADay.dll",""),"Artwork/ability_nutritious.png"));
      Texture2D tex = new Texture2D(2,2);
      tex.LoadImage(imgBytes);

      NewAbility ability = new NewAbility(info,typeof(Nutritious),tex,AbilityIdentifier.GetAbilityIdentifier(PluginGuid, info.rulebookName));
      Nutritious.ability = ability.ability;
      return ability;
    }
  }

  public class Nutritious : CustomAbilityBehaviour
  {
      private void Start()
      {
        this.mod = new CardModificationInfo();
        this.mod.healthAdjustment = 2;
        this.mod.attackAdjustment = 1;
      }

      public override bool RespondsToSacrifice()
      {
         return true;
      }

      public override IEnumerator OnSacrifice()
      {
        yield return base.PreSuccessfulTriggerSequence();
        Singleton<BoardManager>.Instance.currentSacrificeDemandingCard.AddTemporaryMod(this.mod);
        Singleton<BoardManager>.Instance.currentSacrificeDemandingCard.OnStatsChanged();
        yield return new WaitForSeconds(0.25f);
        yield return base.LearnAbility(0.25f);
        yield break;
      }

      private CardModificationInfo mod;
  }
}
