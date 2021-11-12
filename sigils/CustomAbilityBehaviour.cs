using DiskCardGame;

public abstract class CustomAbilityBehaviour : AbilityBehaviour
{
  public override Ability Ability
  {
    get
    {
      return ability;
    }
  }

  public static Ability ability;
}
