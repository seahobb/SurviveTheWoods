using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SurviveTheWoods.ParticleSystem
{
    public class BloodParticleSystem : ParticleSystemClass
    {
        public BloodParticleSystem(Game game, int maxBlood) : base(game, maxBlood * 25) { }

        protected override void InitializeConstants()
        {
            textureFilename = "blood";

            minNumParticles = 20;
            maxNumParticles = 25;

            //blendState = BlendState.Additive;
            DrawOrder = AdditiveBlendDrawOrder;
        }

        protected override void InitializeParticle(ref Particle p, Vector2 where)
        {
            var velocity = RandomHelper.NextDirection() * RandomHelper.NextFloat(40, 200);

            var lifetime = RandomHelper.NextFloat(0.5f, 1.0f);

            var acceleration = -velocity / lifetime;

            var rotation = RandomHelper.NextFloat(0, MathHelper.TwoPi);

            var angularVelocity = RandomHelper.NextFloat(-MathHelper.PiOver4, MathHelper.PiOver4);

            p.Initialize(where, velocity, acceleration, lifetime: lifetime, rotation: rotation, angularVelocity: angularVelocity);
        }

        protected override void UpdateParticle(ref Particle particle, float dt)
        {
            base.UpdateParticle(ref particle, dt);

            float normalizeLifetime = particle.TimeSinceStart / particle.Lifetime;

            float alpha = 4 * normalizeLifetime * (1 - normalizeLifetime);
            particle.Color = Color.White * alpha;

            particle.Scale = 0.1f + 0.25f * normalizeLifetime;
        }

        public void PlaceBlood(Vector2 where) => AddParticles(where);
    }
}
