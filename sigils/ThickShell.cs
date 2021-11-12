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
    private NewAbility AddThickShell()
    {
      AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
      info.powerLevel = 3;
      info.rulebookName = "Thick Shell";
      info.rulebookDescription = "When attacked, [creature] takes one less damage.";
      info.metaCategories = new List<AbilityMetaCategory> {AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part1Modular};

      List<DialogueEvent.Line> lines = new List<DialogueEvent.Line>();
      DialogueEvent.Line line = new DialogueEvent.Line();
      line.text = "The thick shell on that creature protected it from one damage!";
      lines.Add(line);
      info.abilityLearnedDialogue = new DialogueEvent.LineSet(lines);

      byte[] imgBytes = System.IO.File.ReadAllBytes(Path.Combine(this.Info.Location.Replace("SigilADay.dll",""),"Artwork/ability_thickshell.png"));
      Texture2D tex = new Texture2D(2,2);
      tex.LoadImage(imgBytes);

      NewAbility ability = new NewAbility(info,typeof(ThickShell),tex,AbilityIdentifier.GetAbilityIdentifier(PluginGuid, info.rulebookName));
      ThickShell.ability = ability.ability;
      return ability;
    }
  }

  public class ThickShell : CustomAbilityBehaviour
  {
    private void Start()
    {
      int health = base.Card.Info.Health;
      this.mod = new CardModificationInfo();
      this.mod.nonCopyable = true;
      this.mod.singletonId = "ShellHP";
      this.mod.healthAdjustment = 0;
      base.Card.AddTemporaryMod(this.mod);
    }

    public override bool RespondsToCardGettingAttacked(PlayableCard source)
    {
      return source == base.Card;
    }

    public override bool RespondsToAttackEnded()
    {
      return this.attacked;
    }

    public override IEnumerator OnCardGettingAttacked(PlayableCard source)
    {
      this.attacked = true;
      yield return base.PreSuccessfulTriggerSequence();
      this.mod.healthAdjustment = 1;
      yield break;
    }

    public override IEnumerator OnAttackEnded()
    {
      this.attacked = false;
      yield return new WaitForSeconds(0.1f);
      this.mod.healthAdjustment = 0;
      base.Card.HealDamage(1);
      base.Card.Anim.LightNegationEffect();
      yield return new WaitForSeconds(0.1f);
      yield return base.LearnAbility(0.25f);
      yield break;
    }

    private bool attacked;
    private CardModificationInfo mod;
  }
}
