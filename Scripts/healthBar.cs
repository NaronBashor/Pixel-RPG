using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
        public Image HealthBar;
        public Image EnergyBar;
        damageable playerHealth;
        playerEnergy playerEnergy;
        public float maxHealthAmount;
        public float healthAmount;
        public float maxEnergyAmount;
        public float energyAmount;
        private int damage;
        private float energy;

        private void Awake()
        {
                playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<damageable>();
                playerEnergy = GameObject.FindGameObjectWithTag("Player").GetComponent<playerEnergy>();
        }

        private void Update()
        {
                maxHealthAmount = playerHealth.MaxHealth;
                maxEnergyAmount = playerEnergy.MaxEnergy;

                healthAmount = playerHealth.Health;
                energyAmount = playerEnergy.Energy;

                if (playerEnergy)
                {
                        LoseEnergy(energy);
                }

                if (playerHealth)
                {
                        TakeDamage(damage);
                }
        }

        public void TakeDamage(int damage)
        {
                healthAmount -= damage;
                HealthBar.fillAmount = healthAmount / maxHealthAmount;
        }

        public void LoseEnergy(float energy)
        {
                energyAmount -= energy;
                EnergyBar.fillAmount = energyAmount / maxEnergyAmount;
        }
}
