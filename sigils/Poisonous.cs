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
    private NewAbility AddPoisonous()
    {
      AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
      info.powerLevel = 3;
      info.rulebookName = "Poisonous";
      info.rulebookDescription = "When [creature] perishes, the creature that killed it perishes as well.";
      info.metaCategories = new List<AbilityMetaCategory> {AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part1Modular};

      List<DialogueEvent.Line> lines = new List<DialogueEvent.Line>();
      DialogueEvent.Line line = new DialogueEvent.Line();
      line.text = "When this creature perishes, it will kill the creature that killed it.";
      lines.Add(line);
      info.abilityLearnedDialogue = new DialogueEvent.LineSet(lines);

      byte[] imgBytes = System.IO.File.ReadAllBytes(Path.Combine(this.Info.Location.Replace("SigilADay.dll",""),"Artwork/ability_poisonous.png"));
      Texture2D tex = new Texture2D(2,2);
      tex.LoadImage(imgBytes);

      NewAbility ability = new NewAbility(info,typeof(Poisonous),tex,AbilityIdentifier.GetAbilityIdentifier(PluginGuid, info.rulebookName));
      Poisonous.ability = ability.ability;
      return ability;
    }
  }

  public class Poisonous : CustomAbilityBehaviour
  {
    public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer)
    {
      return !wasSacrifice && base.Card.OnBoard;
    }

    public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
    {
      yield return base.PreSuccessfulTriggerSequence();
      yield return new WaitForSeconds(0.25f);
      if (killer != null)
      {
        yield return killer.Die(false, base.Card, true);
        if (Singleton<BoardManager>.Instance is BoardManager3D)
        {
          yield return new WaitForSeconds(0.5f);
          yield return base.LearnAbility(0.5f);
        }
      }
      yield break;
    }
  }
}
