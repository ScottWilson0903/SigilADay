using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using APIPlugin;
using DiskCardGame;
using HarmonyLib;

namespace SigilADay
{
  public partial class Plugin
  {
    private NewAbility AddSilence()
    {
      AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
      info.powerLevel = 5;
      info.rulebookName = "Silence";
      //      if(Localization.CurrentLanguage == Language.ChineseSimplified)
      //          info.rulebookName = "沉默";
      info.rulebookDescription = "Creatures opposing [creature] have all their sigils silenced.";
            if(Localization.CurrentLanguage == Language.ChineseSimplified)
                info.rulebookDescription = "使[creature]对面造物的印记无效。";
            info.metaCategories = new List<AbilityMetaCategory> {AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part1Modular};

      List<DialogueEvent.Line> lines = new List<DialogueEvent.Line>();
      DialogueEvent.Line line = new DialogueEvent.Line();
      line.text = "The sigils opposing your creature are silenced!";
            if(Localization.CurrentLanguage == Language.ChineseSimplified)
                line.text = "你的造物对面的印记被沉默了!";
      lines.Add(line);
      info.abilityLearnedDialogue = new DialogueEvent.LineSet(lines);

      byte[] imgBytes = System.IO.File.ReadAllBytes(Path.Combine(this.Info.Location.Replace("SigilADay.dll",""),"Artwork/ability_silence.png"));
      Texture2D tex = new Texture2D(2,2);
      tex.LoadImage(imgBytes);

      NewAbility ability = new NewAbility(info,typeof(Silence),tex,AbilityIdentifier.GetAbilityIdentifier(PluginGuid, info.rulebookName));
      Silence.ability = ability.ability;
      return ability;
    }
  }

  public class Silence : CustomAbilityBehaviour
  {
    public override bool RespondsToPlayFromHand()
    {
      return true;
    }

    public override IEnumerator OnPlayFromHand()
    {
      yield return base.PreSuccessfulTriggerSequence();
      yield return base.LearnAbility(0.25f);
      yield break;
    }
  }
}
