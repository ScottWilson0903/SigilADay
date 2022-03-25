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
    private NewAbility AddBonePicker()
    {
      AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
      info.powerLevel = 1;
      info.rulebookName = "Bone Picker";
            if(Localization.CurrentLanguage == Language.ChineseSimplified)
                info.rulebookName = "拾骨者";
      info.rulebookDescription = "When [creature] kills a creature, it will generate 1 Bone.";
            if (Localization.CurrentLanguage == Language.ChineseSimplified)
                info.rulebookDescription = "[creature]消灭一个造物，会产生1根骨头。";
      info.metaCategories = new List<AbilityMetaCategory> {AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part1Modular};

      List<DialogueEvent.Line> lines = new List<DialogueEvent.Line>();
      DialogueEvent.Line line = new DialogueEvent.Line();
      line.text = "Your creature licks the corpse clean, and takes a bone!";
            if(Localization.CurrentLanguage == Language.ChineseSimplified)
                line.text = "你的造物将尸体舔干净，并取走了根骨头！";
      lines.Add(line);
      info.abilityLearnedDialogue = new DialogueEvent.LineSet(lines);

      byte[] imgBytes = System.IO.File.ReadAllBytes(Path.Combine(this.Info.Location.Replace("SigilADay.dll",""),"Artwork/ability_bonepicker.png"));
      Texture2D tex = new Texture2D(2,2);
      tex.LoadImage(imgBytes);

      NewAbility ability = new NewAbility(info,typeof(BonePicker),tex,AbilityIdentifier.GetAbilityIdentifier(PluginGuid, info.rulebookName));
      BonePicker.ability = ability.ability;
      return ability;
    }
  }

  public class BonePicker : CustomAbilityBehaviour
  {
    public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
    {
       return fromCombat && base.Card == killer;
    }

    public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
    {
      Singleton<ViewManager>.Instance.SwitchToView(View.Board, false, true);
      yield return new WaitForSeconds(0.1f);
      base.Card.Anim.LightNegationEffect();
      yield return base.PreSuccessfulTriggerSequence();
      yield return Singleton<ResourcesManager>.Instance.AddBones(1, base.Card.Slot);
      yield return new WaitForSeconds(0.1f);
      yield return base.LearnAbility(0.1f);
      Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
      yield break;
    }
  }
}
