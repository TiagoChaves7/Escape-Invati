using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : MonoBehaviour
{
    public float invincibilityDuration = 2f;

    private bool isInvincible = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ActivateInvincibility(other.gameObject));
            Destroy(gameObject);
        }
    }
    public IEnumerator ActivateInvincibility(GameObject player)
    {
        if (!isInvincible)
        {
            isInvincible = true;
            SetInvincible(player, true);
            yield return new WaitForSeconds(invincibilityDuration);
            SetInvincible(player, false);
            isInvincible = false;
        }
    }

    private void SetInvincible(GameObject player, bool invincible)
    {
        // Implemente a lógica para tornar o jogador invencível ou vulnerável.
        // Isso pode incluir desativar colisões com inimigos ou ativar algum efeito visual.
        // Exemplo:
        player.GetComponent<Player>().isInvincible = invincible;
    }

    public void ActivateInvincibility(GameObject player, float duration)
    {
        player.GetComponent<Player>().isInvincible = true; // Ativa a invencibilidade

        StartCoroutine(DeactivateInvincibility(player, duration));
    }

    private IEnumerator DeactivateInvincibility(GameObject player, float duration)
    {
        yield return new WaitForSeconds(duration);

        player.GetComponent<Player>().isInvincible = false; // Desativa a invencibilidade
    }

}
