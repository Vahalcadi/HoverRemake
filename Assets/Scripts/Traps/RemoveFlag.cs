using System.Linq;
using UnityEngine;

public class RemoveFlag : Trap
{
    protected override void Update()
    {
        base.Update();
    }

    protected override bool CanActivateTrap(Collider other)
    {
        return base.CanActivateTrap(other);
    }

    protected override void ActivateTrap(Collider other)
    {
        base.ActivateTrap(other);
        TriggerTrap(other);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    private void TriggerTrap(Collider other)
    {

        if (other.gameObject.CompareTag("Player") && !player.GetComponent<Player>().isShielded && player.GetComponent<Player>().pickedUpFlags > 0)
        {
            player.GetComponent<Player>().pickedUpFlags--;
            GameManager.Instance.DestroyLastPlayerPickedUpFlag();

            Debug.Log($"Removed one flag from player\nflags: {player.GetComponent<Player>().pickedUpFlags}");
        }
        else Debug.Log($"No flags to remove from player\nflags: {player.GetComponent<Player>().pickedUpFlags}");

        if (other.gameObject.CompareTag("EnemyFlagChaser") && enemyFlagChaser.GetComponent<NavigationFlagChaser>().flagsLeft < 3)
        {
            enemyFlagChaser.GetComponent<NavigationFlagChaser>().flagsLeft--;
            Debug.Log($"Removed one flag from enemy\nflags: {enemyFlagChaser.GetComponent<NavigationFlagChaser>().flagsLeft}");
        }
        else Debug.Log($"No flags to remove from enemy\nflags: {enemyFlagChaser.GetComponent<NavigationFlagChaser>().flagsLeft}");

    }
}
