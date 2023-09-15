using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerEnergy : MonoBehaviour
{
        Animator anim;

        [SerializeField] private float _maxEnergy = 100;
        [SerializeField] public float _energy = 100;
        [SerializeField] private float energyRegenSpeed;
        private bool _hasEnergy;
        private bool _isAlive;

        public bool IsAlive
        {
                get
                {
                        return anim.GetBool("isAlive");
                }
        }

        public float Energy
        {
                get
                {
                        return _energy;
                }
                set
                {
                        _energy = value;
                        anim.SetFloat("energy", value);
                        if (_energy  <= 0)
                        {
                                _hasEnergy = false;
                        }
                }
        }

        public float MaxEnergy
        {
                get => _maxEnergy;
                set => _maxEnergy=value;
        }

        public bool HasEnergy
        {
                get
                {
                        return _hasEnergy;
                }
                set
                {
                        _hasEnergy = value;
                        anim.SetBool("hasEnergy", value);
                }
        }

        // Start is called before the first frame update
        void Awake()
        {
                anim = GetComponent<Animator>();
        }

        private void Update()
        {
                if (Energy < 100)
                {
                        Energy += energyRegenSpeed * Time.deltaTime;
                }
        }

        public bool EnergyAttack(float energy)
        {
                if (IsAlive)
                {
                        if (Energy > 20)
                        {
                                Energy -= energy;
                                return true;
                        }
                }
                return false;
        }
}
