using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(ParticleSystem))]
public class SoundEffects : MonoBehaviour
{
    private ParticleSystem _parentParticleSystem;
    private IDictionary<uint, ParticleSystem.Particle> _trackedParticles = new Dictionary<uint, ParticleSystem.Particle>();
    [SerializeField] private AudioSource AudioSource;
    
    void Start()
    {
        _parentParticleSystem = this.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        var liveParticles       = new ParticleSystem.Particle[_parentParticleSystem.particleCount];
        _parentParticleSystem.GetParticles(liveParticles);
        var particleDelta       = GetParticleDelta(liveParticles);
        Debug.Log(particleDelta.Added + " added");

        foreach (var particleAdded in particleDelta.Added)
        {
            AudioSource.Play();
        }
    }

    private ParticleDelta GetParticleDelta(ParticleSystem.Particle[] liveParticles)
    {
        var deltaResult = new ParticleDelta();

        foreach (var activeParticle in liveParticles)
        {
            ParticleSystem.Particle foundParticle;
            if(_trackedParticles.TryGetValue(activeParticle.randomSeed, out foundParticle))
            {
                _trackedParticles[activeParticle.randomSeed] = activeParticle;
            }
            else
            {
                deltaResult.Added.Add(activeParticle);
                _trackedParticles.Add(activeParticle.randomSeed, activeParticle);
            }
        }

        var updatedParticleAsDictionary = liveParticles.ToDictionary(x => x.randomSeed, x => x);
        var dictionaryKeysAsList = _trackedParticles.Keys.ToList();

        foreach (var dictionaryKey in dictionaryKeysAsList)
        {
            if (updatedParticleAsDictionary.ContainsKey(dictionaryKey) == false)
            {
                deltaResult.Removed.Add(_trackedParticles[dictionaryKey]);
                _trackedParticles.Remove(dictionaryKey);
            }
        }
        
        return deltaResult;
    }


    private class ParticleDelta
    {
        public IList<ParticleSystem.Particle> Added     { get; set; }   = new List<ParticleSystem.Particle>();
        public IList<ParticleSystem.Particle> Removed   { get; set; }   = new List<ParticleSystem.Particle>();
    }
}
